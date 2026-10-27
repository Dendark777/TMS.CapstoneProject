using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using TMS.CapstoneProject.Core.Configuration;

namespace TMS.CapstoneProject.Core.Browsers;

public static class BrowserFactory
{
    public static IWebDriver Create(TestSettings settings)
    {
        var browser = ParseBrowser(settings.Browser);
        IWebDriver driver = browser switch
        {
            BrowserType.Firefox => new FirefoxDriver(CreateFirefoxOptions(settings.Headless)),
            BrowserType.Edge => new EdgeDriver(CreateEdgeOptions(settings.Headless)),
            _ => new ChromeDriver(CreateChromeOptions(settings.Headless))
        };

        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(settings.ImplicitWaitSeconds);
        driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(settings.PageLoadTimeoutSeconds);
        // Maximize() in headless Chrome can shrink the window; set an explicit desktop size instead.
        driver.Manage().Window.Size = new Size(1920, 1080);
        if (!settings.Headless)
        {
            driver.Manage().Window.Maximize();
        }

        return driver;
    }

    public static BrowserType ParseBrowser(string? value)
    {
        return Enum.TryParse<BrowserType>(value, ignoreCase: true, out var browser)
            ? browser
            : BrowserType.Chrome;
    }

    private static ChromeOptions CreateChromeOptions(bool headless)
    {
        var options = new ChromeOptions();
        ApplyCommonArguments(options, headless);
        options.AddArgument("--disable-search-engine-choice-screen");
        options.AddUserProfilePreference("intl.accept_languages", "ru-RU,ru");
        return options;
    }

    private static FirefoxOptions CreateFirefoxOptions(bool headless)
    {
        var options = new FirefoxOptions();
        if (headless)
        {
            options.AddArgument("-headless");
        }

        options.SetPreference("intl.accept_languages", "ru-RU,ru");
        options.SetPreference("app.update.auto", false);
        return options;
    }

    private static EdgeOptions CreateEdgeOptions(bool headless)
    {
        var options = new EdgeOptions();
        ApplyCommonArguments(options, headless);
        return options;
    }

    private static void ApplyCommonArguments(DriverOptions options, bool headless)
    {
        var arguments = new List<string>
        {
            "--disable-gpu",
            "--no-sandbox",
            "--disable-dev-shm-usage",
            "--window-size=1920,1080",
            "--lang=ru-RU",
            "--disable-notifications"
        };

        if (headless)
        {
            arguments.Add("--headless=new");
        }

        switch (options)
        {
            case ChromeOptions chrome:
                chrome.AddArguments(arguments);
                break;
            case EdgeOptions edge:
                edge.AddArguments(arguments);
                break;
        }
    }
}
