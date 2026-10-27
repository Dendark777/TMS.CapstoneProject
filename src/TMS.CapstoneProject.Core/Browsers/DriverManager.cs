using OpenQA.Selenium;
using TMS.CapstoneProject.Core.Configuration;

namespace TMS.CapstoneProject.Core.Browsers;

public static class DriverManager
{
    private static readonly AsyncLocal<IWebDriver?> Current = new();

    public static IWebDriver Driver =>
        Current.Value ?? throw new InvalidOperationException("WebDriver is not initialized for the current thread.");

    public static bool HasDriver => Current.Value is not null;

    public static IWebDriver Start()
    {
        Quit();
        Current.Value = BrowserFactory.Create(ConfigManager.Settings);
        return Current.Value;
    }

    public static void Quit()
    {
        try
        {
            Current.Value?.Quit();
        }
        catch
        {
            // Driver may already be closed after a browser crash.
        }
        finally
        {
            Current.Value?.Dispose();
            Current.Value = null;
        }
    }
}
