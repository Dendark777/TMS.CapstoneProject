using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TMS.CapstoneProject.Core.Configuration;

namespace TMS.CapstoneProject.Core.Wrappers;

public sealed class WaitWrapper
{
    private readonly IWebDriver _driver;
    private readonly TimeSpan _timeout;

    public WaitWrapper(IWebDriver driver, TimeSpan? timeout = null)
    {
        _driver = driver;
        _timeout = timeout ?? TimeSpan.FromSeconds(ConfigManager.Settings.ExplicitWaitSeconds);
    }

    public IWebElement UntilVisible(By locator) =>
        CreateWait().Until(driver =>
        {
            var element = driver.FindElement(locator);
            return element.Displayed ? element : null;
        }) ?? throw new WebDriverTimeoutException($"Element {locator} was not visible.");

    public IWebElement UntilClickable(By locator) =>
        CreateWait().Until(driver =>
        {
            var element = driver.FindElement(locator);
            return element.Displayed && element.Enabled ? element : null;
        }) ?? throw new WebDriverTimeoutException($"Element {locator} was not clickable.");

    public bool UntilInvisible(By locator)
    {
        return CreateWait().Until(driver =>
        {
            var elements = driver.FindElements(locator);
            return elements.Count == 0 || elements.All(element => !element.Displayed);
        });
    }

    public bool UntilUrlContains(string fragment) =>
        CreateWait().Until(driver => driver.Url.Contains(fragment, StringComparison.OrdinalIgnoreCase));

    public bool UntilTitleContains(string fragment) =>
        CreateWait().Until(driver => driver.Title.Contains(fragment, StringComparison.OrdinalIgnoreCase));

    public T Until<T>(Func<IWebDriver, T> condition) => CreateWait().Until(condition);

    public bool Exists(By locator, TimeSpan? timeout = null)
    {
        try
        {
            new WebDriverWait(_driver, timeout ?? TimeSpan.FromSeconds(3))
            {
                PollingInterval = TimeSpan.FromMilliseconds(200)
            }.Until(driver => driver.FindElements(locator).Any(element => element.Displayed));
            return true;
        }
        catch (WebDriverTimeoutException)
        {
            return false;
        }
    }

    private WebDriverWait CreateWait()
    {
        var wait = new WebDriverWait(_driver, _timeout)
        {
            PollingInterval = TimeSpan.FromMilliseconds(250)
        };
        wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));
        return wait;
    }
}
