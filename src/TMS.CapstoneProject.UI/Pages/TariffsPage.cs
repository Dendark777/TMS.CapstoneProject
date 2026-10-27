using OpenQA.Selenium;

namespace TMS.CapstoneProject.UI.Pages;

public sealed class TariffsPage : BasePage
{
    private static readonly By Content = By.CssSelector(".sbis_ru-content, .bodyContent");

    public TariffsPage Open()
    {
        OpenUrl(Combine("/tariffs"));
        return WaitReady();
    }

    public TariffsPage WaitReady()
    {
        WaitUntilLoaded();
        Wait.UntilUrlContains("/tariff");
        Wait.UntilVisible(Content);
        return this;
    }

    public bool IsOpened() => Url.Contains("/tariff", StringComparison.OrdinalIgnoreCase);

    public bool TitleLooksValid() =>
        Title.Contains("тариф", StringComparison.OrdinalIgnoreCase)
        || Title.Contains("Saby", StringComparison.OrdinalIgnoreCase);
}
