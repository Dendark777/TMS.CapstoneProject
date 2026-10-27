using OpenQA.Selenium;
using TMS.CapstoneProject.Core.Configuration;

namespace TMS.CapstoneProject.UI.Pages;

public sealed class AuthPage : BasePage
{
    private static readonly By PageRoot = By.CssSelector("[data-entity-id='main-auth-page'], .authPage-Layout");
    private static readonly By LoginInput = By.CssSelector("input.controls-Field:not([type='password']), input[type='text'], input[type='tel']");
    private static readonly By PasswordInput = By.CssSelector("input[type='password']");
    private static readonly By SubmitButton = By.XPath("//button[contains(.,'Войти') or contains(.,'Sign in') or contains(.,'Войти')]");
    private static readonly By DemoLink = By.XPath("//*[self::a or self::button][contains(.,'демо') or contains(.,'Demo') or contains(.,'Try demo')]");
    private static readonly By ErrorOrHint = By.XPath("//*[contains(@class,'error') or contains(@class,'Error') or contains(@class,'auth') or contains(.,'парол') or contains(.,'логин')]");

    public AuthPage Open()
    {
        OpenUrl(ConfigManager.Settings.AuthUrl);
        return WaitReady();
    }

    public AuthPage WaitReady()
    {
        WaitUntilLoaded();
        Wait.Until(driver =>
            driver.Url.Contains("auth", StringComparison.OrdinalIgnoreCase)
            || driver.FindElements(PageRoot).Count > 0);
        return this;
    }

    public bool IsOpened() =>
        Url.Contains("auth", StringComparison.OrdinalIgnoreCase)
        || Elements.Displayed(PageRoot);

    public bool LoginFormIsDisplayed() =>
        Elements.Displayed(LoginInput) || Elements.Displayed(SubmitButton) || Elements.Displayed(PageRoot);

    public AuthPage FillLogin(string login)
    {
        if (Elements.Displayed(LoginInput))
        {
            Elements.Type(LoginInput, login);
        }

        return this;
    }

    public AuthPage FillPassword(string password)
    {
        if (Elements.Displayed(PasswordInput))
        {
            Elements.Type(PasswordInput, password);
        }

        return this;
    }

    public AuthPage Submit()
    {
        if (Elements.Displayed(SubmitButton))
        {
            Elements.Click(SubmitButton);
        }

        WaitUntilLoaded();
        return this;
    }

    public bool StaysOnAuthOrShowsError()
    {
        return IsOpened() || Elements.Displayed(ErrorOrHint);
    }

    public bool DemoIsAvailable() => Elements.Displayed(DemoLink);
}
