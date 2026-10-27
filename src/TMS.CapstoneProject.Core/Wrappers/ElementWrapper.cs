using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace TMS.CapstoneProject.Core.Wrappers;

public sealed class ElementWrapper
{
    private readonly IWebDriver _driver;
    private readonly WaitWrapper _wait;
    private readonly JsExecutorWrapper _js;

    public ElementWrapper(IWebDriver driver)
    {
        _driver = driver;
        _wait = new WaitWrapper(driver);
        _js = new JsExecutorWrapper(driver);
    }

    public IWebElement Find(By locator) => _wait.UntilVisible(locator);

    public IReadOnlyList<IWebElement> FindAll(By locator) => _driver.FindElements(locator);

    public void Click(By locator)
    {
        var element = _wait.UntilClickable(locator);
        TryAction(() =>
        {
            _js.ScrollIntoView(element);
            element.Click();
        }, () => _js.Click(element));
    }

    public void Type(By locator, string text, bool clear = true)
    {
        var element = _wait.UntilVisible(locator);
        TryAction(() =>
        {
            _js.ScrollIntoView(element);
            if (clear)
            {
                element.Clear();
            }

            element.SendKeys(text);
        }, () => _js.SetValue(element, text));
    }

    public string Text(By locator) => Find(locator).Text.Trim();

    public string Attribute(By locator, string name) => Find(locator).GetAttribute(name) ?? string.Empty;

    public bool Displayed(By locator) => _wait.Exists(locator, TimeSpan.FromSeconds(8));

    public int Count(By locator) => FindAll(locator).Count;

    public void Hover(By locator)
    {
        var element = Find(locator);
        new Actions(_driver).MoveToElement(element).Perform();
    }

    private static void TryAction(Action primary, Action fallback)
    {
        try
        {
            primary();
        }
        catch (Exception ex) when (ex is ElementClickInterceptedException or ElementNotInteractableException)
        {
            fallback();
        }
    }
}
