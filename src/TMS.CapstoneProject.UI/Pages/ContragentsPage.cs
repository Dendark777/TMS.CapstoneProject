using OpenQA.Selenium;

namespace TMS.CapstoneProject.UI.Pages;

public sealed class ContragentsPage : BasePage
{
    private static readonly By Heading = By.CssSelector("h1, h2, .sbisru-h1, .sbisru-h2");
    private static readonly By SearchInput = By.CssSelector("input.controls-Field, input[type='text']");
    private static readonly By InnHint = By.XPath("//*[contains(.,'ИНН') or contains(.,'контрагент')]");

    public ContragentsPage Open()
    {
        OpenUrl(Combine("/contragents"));
        return WaitReady();
    }

    public ContragentsPage WaitReady()
    {
        WaitUntilLoaded();
        Wait.Until(_ =>
            Driver.Url.Contains("contragent", StringComparison.OrdinalIgnoreCase)
            || Driver.Url.Contains("profile", StringComparison.OrdinalIgnoreCase)
            || Driver.Title.Contains("контрагент", StringComparison.OrdinalIgnoreCase)
            || Driver.PageSource.Contains("контрагент", StringComparison.OrdinalIgnoreCase));
        return this;
    }

    public bool IsOpened() =>
        Url.Contains("contragent", StringComparison.OrdinalIgnoreCase)
        || Url.Contains("profile", StringComparison.OrdinalIgnoreCase)
        || Title.Contains("контрагент", StringComparison.OrdinalIgnoreCase);

    public bool SearchIsDisplayed() => Elements.Displayed(SearchInput) || Elements.Displayed(InnHint);

    public bool HeadingMentionsContragents() =>
        Title.Contains("контрагент", StringComparison.OrdinalIgnoreCase)
        || Title.Contains("ИНН", StringComparison.OrdinalIgnoreCase)
        || Elements.Displayed(Heading);
}
