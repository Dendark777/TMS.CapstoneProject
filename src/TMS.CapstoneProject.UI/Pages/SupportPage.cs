using OpenQA.Selenium;

namespace TMS.CapstoneProject.UI.Pages;

public sealed class SupportPage : BasePage
{
    private static readonly By Content = By.CssSelector(".sbis_ru-content, .bodyContent, .sbis_help-page");

    public SupportPage Open()
    {
        OpenUrl(Combine("/support"));
        return WaitReady();
    }

    public SupportPage WaitReady()
    {
        WaitUntilLoaded();
        Wait.UntilVisible(Content);
        return this;
    }

    public bool IsOpened() =>
        Url.Contains("/support", StringComparison.OrdinalIgnoreCase)
        || Url.Contains("/help", StringComparison.OrdinalIgnoreCase);

    public bool TitleLooksValid() =>
        Title.Contains("поддерж", StringComparison.OrdinalIgnoreCase)
        || Title.Contains("помо", StringComparison.OrdinalIgnoreCase)
        || Title.Contains("Saby", StringComparison.OrdinalIgnoreCase);
}
