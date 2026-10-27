using Allure.Net.Commons;
using OpenQA.Selenium;
using TMS.CapstoneProject.Core.Logging;

namespace TMS.CapstoneProject.Core.Wrappers;

public static class ScreenshotWrapper
{
    public static string Capture(IWebDriver driver, string name)
    {
        var directory = Path.Combine(AppContext.BaseDirectory, "screenshots");
        Directory.CreateDirectory(directory);

        var safeName = string.Concat(name.Select(ch => Path.GetInvalidFileNameChars().Contains(ch) ? '_' : ch));
        var path = Path.Combine(directory, $"{safeName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");

        ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(path);
        TestLogger.Info($"Screenshot saved: {path}");
        return path;
    }

    public static void AttachToAllure(IWebDriver driver, string name)
    {
        try
        {
            var bytes = ((ITakesScreenshot)driver).GetScreenshot().AsByteArray;
            AllureApi.AddAttachment(name, "image/png", bytes, ".png");
        }
        catch (Exception ex)
        {
            TestLogger.Warn($"Failed to attach screenshot: {ex.Message}");
        }
    }
}
