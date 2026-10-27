using OpenQA.Selenium;
using TMS.CapstoneProject.UI.Components;

namespace TMS.CapstoneProject.UI.Pages;

public sealed class ContactsPage : BasePage
{
    private static readonly By Phone = By.XPath("//*[contains(.,'+7') and (contains(.,'495') or contains(.,'800') or contains(.,'812') or contains(.,'4852'))]");
    private static readonly By OfficeBlock = By.XPath("//*[contains(.,'офис') or contains(.,'Офис') or contains(.,'филиал') or contains(.,'Партнер')]");
    private static readonly By Address = By.XPath("//*[contains(.,'ул.') or contains(.,'пр-кт') or contains(.,'проспект')]");

    public HeaderComponent Header { get; }

    public ContactsPage()
    {
        Header = new HeaderComponent(Driver);
    }

    public ContactsPage Open()
    {
        OpenUrl(Combine("/contacts"));
        return WaitReady();
    }

    public ContactsPage WaitReady()
    {
        WaitUntilLoaded();
        Wait.UntilUrlContains("/contacts");
        return this;
    }

    public bool IsOpened() => Url.Contains("/contacts", StringComparison.OrdinalIgnoreCase);

    public bool PhoneIsDisplayed() => Elements.Displayed(Phone);

    public bool OfficesAreDisplayed() => Elements.Displayed(OfficeBlock);

    public bool AddressIsDisplayed() => Elements.Displayed(Address);
}
