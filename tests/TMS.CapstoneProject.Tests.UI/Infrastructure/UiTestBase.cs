using Allure.NUnit;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using TMS.CapstoneProject.Core.Browsers;
using TMS.CapstoneProject.Core.Logging;
using TMS.CapstoneProject.Core.Wrappers;

namespace TMS.CapstoneProject.Tests.UI.Infrastructure;

[AllureNUnit]
[Parallelizable(ParallelScope.Fixtures)]
public abstract class UiTestBase
{
    protected IWebDriver Driver => DriverManager.Driver;

    [SetUp]
    public void SetUp()
    {
        TestLogger.Info($"Start UI test: {TestContext.CurrentContext.Test.FullName}");
        DriverManager.Start();
    }

    [TearDown]
    public void TearDown()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed && DriverManager.HasDriver)
        {
            ScreenshotWrapper.AttachToAllure(Driver, TestContext.CurrentContext.Test.Name);
            ScreenshotWrapper.Capture(Driver, TestContext.CurrentContext.Test.Name);
        }

        DriverManager.Quit();
        TestLogger.Info($"Finished UI test: {TestContext.CurrentContext.Test.FullName}");
    }
}
