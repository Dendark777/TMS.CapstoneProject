pipeline {
    agent any

    parameters {
        choice(name: 'BROWSER', choices: ['Chrome', 'Firefox', 'Edge'], description: 'Браузер для UI-тестов')
        choice(name: 'SUITE', choices: ['All', 'Smoke', 'UI', 'API', 'BDD'], description: 'Набор тестов')
        booleanParam(name: 'HEADLESS', defaultValue: true, description: 'Headless-режим')
        choice(name: 'RUNSETTINGS', choices: ['runsettings/chrome.runsettings', 'runsettings/parallel.runsettings', 'runsettings/firefox.runsettings', 'runsettings/edge.runsettings'], description: 'План прогона')
    }

    environment {
        BROWSER = "${params.BROWSER}"
        HEADLESS = "${params.HEADLESS}"
        DOTNET_ROOT = "${HOME}/.dotnet"
        DOTNET_CLI_TELEMETRY_OPTOUT = '1'
        DOTNET_SKIP_FIRST_TIME_EXPERIENCE = '1'
        DOTNET_NOLOGO = '1'
        LD_LIBRARY_PATH = "${HOME}/.local-libs/usr/lib/x86_64-linux-gnu:${LD_LIBRARY_PATH}"
        FONTCONFIG_PATH = "${HOME}/.local-libs/etc/fonts"
        PATH = "${HOME}/.dotnet:${HOME}/tools/chrome/chrome-linux64:${PATH}"
    }

    options {
        timestamps()
        timeout(time: 45, unit: 'MINUTES')
    }

    stages {
        stage('Restore') {
            steps {
                script { run('dotnet restore TMS.CapstoneProject.slnx') }
            }
        }

        stage('Build') {
            steps {
                script { run('dotnet build TMS.CapstoneProject.slnx --no-restore -c Release') }
            }
        }

        stage('Test') {
            steps {
                catchError(buildResult: 'UNSTABLE', stageResult: 'FAILURE') {
                    script {
                        def filter = suiteFilter(params.SUITE)
                        def command = "dotnet test TMS.CapstoneProject.slnx --no-build -c Release --settings ${params.RUNSETTINGS} --logger trx --results-directory TestResults"
                        if (filter) {
                            command += " --filter \"${filter}\""
                        }
                        if (isUnix()) {
                            sh command
                        } else {
                            bat command
                        }
                    }
                }
            }
        }

        stage('Collect Allure results') {
            steps {
                script {
                    if (isUnix()) {
                        sh '''
                            rm -rf allure-results
                            mkdir -p allure-results
                            find tests -type d -name allure-results -exec sh -c 'cp -r "$1"/. allure-results/' _ {} \\;
                        '''
                    } else {
                        bat '''
                            if exist allure-results rmdir /s /q allure-results
                            mkdir allure-results
                            for /d /r tests %%d in (allure-results) do xcopy /e /y "%%d\\*" allure-results\\
                        '''
                    }
                }
            }
        }
    }

    post {
        always {
            script {
                if (fileExists('allure-results')) {
                    allure includeProperties: false, jdk: '', results: [[path: 'allure-results']]
                }
            }
            archiveArtifacts artifacts: 'TestResults/**,**/screenshots/**,allure-results/**', allowEmptyArchive: true
        }
    }
}

def run(String command) {
    if (isUnix()) {
        sh command
    } else {
        bat command
    }
}

def suiteFilter(String suite) {
    switch (suite) {
        case 'Smoke': return 'TestCategory=Smoke'
        case 'UI': return 'TestCategory=UI'
        case 'API': return 'TestCategory=API'
        case 'BDD': return 'FullyQualifiedName~TMS.CapstoneProject.Tests.Bdd'
        default: return ''
    }
}
