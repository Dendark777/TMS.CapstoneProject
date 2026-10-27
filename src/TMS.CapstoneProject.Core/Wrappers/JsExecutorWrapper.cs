using OpenQA.Selenium;

namespace TMS.CapstoneProject.Core.Wrappers;

public sealed class JsExecutorWrapper
{
    private readonly IJavaScriptExecutor _js;

    public JsExecutorWrapper(IWebDriver driver)
    {
        _js = (IJavaScriptExecutor)driver;
    }

    public object? Execute(string script, params object[] args) => _js.ExecuteScript(script, args);

    public void Click(IWebElement element) => Execute("arguments[0].click();", element);

    public void ScrollIntoView(IWebElement element) =>
        Execute("arguments[0].scrollIntoView({block:'center', inline:'nearest'});", element);

    public void SetValue(IWebElement element, string value)
    {
        Execute("arguments[0].value = arguments[1]; arguments[0].dispatchEvent(new Event('input', { bubbles: true }));", element, value);
    }

    public string ReadyState => Execute("return document.readyState;")?.ToString() ?? string.Empty;

    public bool IsDocumentReady => string.Equals(ReadyState, "complete", StringComparison.OrdinalIgnoreCase);
}
