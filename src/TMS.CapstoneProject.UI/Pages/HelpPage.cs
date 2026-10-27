using OpenQA.Selenium;

namespace TMS.CapstoneProject.UI.Pages;

public sealed class HelpPage : BasePage
{
    private static readonly By TitleHeading = By.CssSelector("h1.sbis_help-Index-AllServices_title, h1");
    private static readonly By SearchInput = By.CssSelector("input.controls-Field, input[inputmode='search']");
    private static readonly By SearchButton = By.CssSelector(".sbis_help-SearchInput__button");
    private static readonly By ServiceItem = By.CssSelector(".sbis_help-Menu_ServiceList-item, .sbis_help-Index-AllServices_serviceList a");
    private static readonly By SearchResults = By.CssSelector(".sbis_help-content, .Article-container, [data-qa='items-container']");

    public HelpPage Open()
    {
        OpenUrl(Combine("/help"));
        return WaitReady();
    }

    public HelpPage WaitReady()
    {
        WaitUntilLoaded();
        Wait.UntilUrlContains("/help");
        return this;
    }

    public bool IsOpened() => Url.Contains("/help", StringComparison.OrdinalIgnoreCase);

    public bool TitleIsDisplayed() => Elements.Displayed(TitleHeading);

    public int ServiceCount() => Elements.Count(ServiceItem);

    public HelpPage Search(string query)
    {
        Wait.UntilVisible(SearchInput);
        Elements.Type(SearchInput, query);
        if (Elements.Displayed(SearchButton))
        {
            Elements.Click(SearchButton);
        }
        else
        {
            FindSearchInput().SendKeys(Keys.Enter);
        }

        WaitUntilLoaded();
        return this;
    }

    public bool HasResultsOrContent() => Elements.Displayed(SearchResults) || Title.Length > 0;

    public HelpPage OpenService(string name)
    {
        var locator = By.XPath($"//a[contains(@class,'sbis_help-Menu_ServiceList-item-link') and contains(.,'{name}')]");
        Elements.Click(locator);
        WaitUntilLoaded();
        return this;
    }

    private IWebElement FindSearchInput() => Wait.UntilVisible(SearchInput);
}
