using OpenQA.Selenium;
using TMS.CapstoneProject.Core.Wrappers;
using TMS.CapstoneProject.UI.Pages;

namespace TMS.CapstoneProject.UI.Components;

public sealed class HeaderComponent
{
    private readonly IWebDriver _driver;
    private readonly ElementWrapper _elements;

    private static readonly By Root = By.CssSelector(".sbisru-Header");
    private static readonly By Logo = By.CssSelector(".sbisru-Header__logo, .sbisru-Header__logo-text, .sabyru__Logo");
    private static readonly By LogoText = By.CssSelector(".sbisru-Header__logo-text");
    private static readonly By Burger = By.CssSelector("button.js-header-burger");
    private static readonly Dictionary<string, string> HeaderHrefs = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Тарифы"] = "/tariffs",
        ["Контакты"] = "/contacts",
        ["Поддержка"] = "/support",
        ["Продукты"] = "/products"
    };

    public HeaderComponent(IWebDriver driver)
    {
        _driver = driver;
        _elements = new ElementWrapper(driver);
    }

    public bool IsDisplayed() => _elements.Displayed(Root);

    public bool LogoIsDisplayed() => _elements.Displayed(Logo) || _elements.Displayed(LogoText);

    public HomePage OpenHome()
    {
        _elements.Click(Logo);
        return new HomePage().WaitReady();
    }

    public void OpenLink(string title)
    {
        if (HeaderHrefs.TryGetValue(title, out var href))
        {
            var byHref = By.CssSelector($".sbisru-Header a[href*='{href}']");
            if (_elements.Displayed(byHref))
            {
                _elements.Click(byHref);
                return;
            }
        }

        var byText = By.XPath($"//div[contains(@class,'sbisru-Header')]//a[normalize-space()='{title}']");
        if (_elements.Displayed(byText))
        {
            _elements.Click(byText);
            return;
        }

        if (HeaderHrefs.TryGetValue(title, out var fallbackHref))
        {
            _driver.Navigate().GoToUrl(new Uri(new Uri(_driver.Url), fallbackHref));
            return;
        }

        throw new InvalidOperationException($"Header item '{title}' was not found.");
    }
}
