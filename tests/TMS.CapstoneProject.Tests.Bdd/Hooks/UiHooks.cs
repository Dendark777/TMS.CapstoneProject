using Allure.NUnit;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Reqnroll;
using TMS.CapstoneProject.Core.Browsers;
using TMS.CapstoneProject.Core.Logging;
using TMS.CapstoneProject.Core.Wrappers;

namespace TMS.CapstoneProject.Tests.Bdd.Hooks;

[Binding]
[AllureNUnit]
public sealed class UiHooks
{
    [BeforeScenario("ui")]
    public void BeforeUiScenario()
    {
        TestLogger.Info($"BDD UI start: {TestContext.CurrentContext.Test.Name}");
        DriverManager.Start();
    }

    [AfterScenario("ui")]
    public void AfterUiScenario()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed && DriverManager.HasDriver)
        {
            ScreenshotWrapper.AttachToAllure(DriverManager.Driver, TestContext.CurrentContext.Test.Name);
        }

        DriverManager.Quit();
    }
}
