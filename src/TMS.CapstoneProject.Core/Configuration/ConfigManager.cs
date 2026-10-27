using Microsoft.Extensions.Configuration;

namespace TMS.CapstoneProject.Core.Configuration;

public static class ConfigManager
{
    private static readonly Lazy<TestSettings> SettingsLazy = new(Load);

    public static TestSettings Settings => SettingsLazy.Value;

    private static TestSettings Load()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: false)
            .AddEnvironmentVariables(prefix: "TMS_")
            .Build();

        var settings = new TestSettings();
        configuration.Bind(settings);

        var browser = Environment.GetEnvironmentVariable("BROWSER");
        if (!string.IsNullOrWhiteSpace(browser))
        {
            settings.Browser = browser;
        }

        var headless = Environment.GetEnvironmentVariable("HEADLESS");
        if (bool.TryParse(headless, out var isHeadless))
        {
            settings.Headless = isHeadless;
        }

        return settings;
    }
}
