using OpenQA.Selenium;
using TMS.CapstoneProject.UI.Components;

namespace TMS.CapstoneProject.UI.Pages;

public sealed class ProductsPage : BasePage
{
    private static readonly By Content = By.CssSelector(".sbis_ru-content, .bodyContent");
    private static readonly By ProductLinks = By.CssSelector("a[href*='/']");

    public HeaderComponent Header { get; }

    public ProductsPage()
    {
        Header = new HeaderComponent(Driver);
    }

    public ProductsPage Open()
    {
        OpenUrl(Combine("/products"));
        return WaitReady();
    }

    public ProductsPage WaitReady()
    {
        WaitUntilLoaded();
        Wait.UntilUrlContains("/products");
        Wait.UntilVisible(Content);
        return this;
    }

    public bool IsOpened() => Url.Contains("/products", StringComparison.OrdinalIgnoreCase);

    public bool HasProductCards() => Elements.Count(ProductLinks) > 5;
}
