using OpenQA.Selenium;
using TMS.CapstoneProject.Core.Configuration;
using TMS.CapstoneProject.UI.Components;

namespace TMS.CapstoneProject.UI.Pages;

public sealed class HomePage : BasePage
{
    private static readonly By Hero = By.XPath("//*[contains(.,'Управляйте бизнесом') or contains(.,'Соберите свой Saby')]");

    private static By ProductLink(string name) =>
        By.XPath($"//a[contains(.,'{name}')]");

    public HeaderComponent Header { get; }

    public HomePage()
    {
        Header = new HeaderComponent(Driver);
    }

    public HomePage Open()
    {
        OpenUrl(ConfigManager.Settings.UiBaseUrl);
        return WaitReady();
    }

    public HomePage WaitReady()
    {
        WaitUntilLoaded();
        Wait.UntilVisible(By.CssSelector(".sbisru-Header"));
        return this;
    }

    public bool HeroIsDisplayed() => Elements.Displayed(Hero);

    public bool ProductIsDisplayed(string name) => Elements.Displayed(ProductLink(name));

    public ProductsPage OpenProducts()
    {
        Header.OpenLink("Продукты");
        return new ProductsPage().WaitReady();
    }

    public TariffsPage OpenTariffs()
    {
        Header.OpenLink("Тарифы");
        return new TariffsPage().WaitReady();
    }

    public ContactsPage OpenContacts()
    {
        Header.OpenLink("Контакты");
        return new ContactsPage().WaitReady();
    }

    public SupportPage OpenSupport()
    {
        Header.OpenLink("Поддержка");
        return new SupportPage().WaitReady();
    }
}
