using OpenQA.Selenium;
using TMS.CapstoneProject.Core.Browsers;
using TMS.CapstoneProject.Core.Configuration;
using TMS.CapstoneProject.Core.Logging;
using TMS.CapstoneProject.Core.Wrappers;

namespace TMS.CapstoneProject.UI.Pages;

public abstract class BasePage
{
    protected IWebDriver Driver => DriverManager.Driver;
    protected ElementWrapper Elements { get; }
    protected WaitWrapper Wait { get; }
    protected JsExecutorWrapper Js { get; }

    private static readonly By CookieClose = By.CssSelector(".sbis_ru-CookieAgreement__close");
    private static readonly By PreloadOverlay = By.CssSelector(".preload-overlay");

    protected BasePage()
    {
        Elements = new ElementWrapper(Driver);
        Wait = new WaitWrapper(Driver);
        Js = new JsExecutorWrapper(Driver);
    }

    public string Title => Driver.Title;
    public string Url => Driver.Url;

    protected void OpenUrl(string url)
    {
        TestLogger.Info($"Open {url}");
        Driver.Navigate().GoToUrl(url);
        Wait.Until(_ => Js.IsDocumentReady);
        DismissCookieBanner();
    }

    protected string Combine(string relative)
    {
        return new Uri(new Uri(ConfigManager.Settings.UiBaseUrl.TrimEnd('/') + "/"), relative.TrimStart('/')).ToString();
    }

    public void DismissCookieBanner()
    {
        if (Wait.Exists(CookieClose, TimeSpan.FromSeconds(3)))
        {
            Elements.Click(CookieClose);
            Wait.UntilInvisible(CookieClose);
        }
    }

    protected void WaitUntilLoaded()
    {
        Wait.Until(_ => Js.IsDocumentReady);
        if (Wait.Exists(PreloadOverlay, TimeSpan.FromSeconds(1)))
        {
            Wait.UntilInvisible(PreloadOverlay);
        }
    }
}
