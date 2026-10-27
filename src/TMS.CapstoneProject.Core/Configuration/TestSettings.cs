namespace TMS.CapstoneProject.Core.Configuration;

public sealed class TestSettings
{
    public string Browser { get; set; } = "Chrome";
    public bool Headless { get; set; } = true;
    public int ImplicitWaitSeconds { get; set; }
    public int ExplicitWaitSeconds { get; set; } = 20;
    public int PageLoadTimeoutSeconds { get; set; } = 60;
    public string UiBaseUrl { get; set; } = "https://saby.ru";
    public string AuthUrl { get; set; } = "https://online.saby.ru/auth/";
    public ApiSettings Api { get; set; } = new();
}

public sealed class ApiSettings
{
    public string SabyBaseUrl { get; set; } = "https://saby.ru";
    public string SppApiUrl { get; set; } = "https://api.saby.ru/spp-rest-api/service/";
    public string JsonPlaceholderUrl { get; set; } = "https://jsonplaceholder.typicode.com";
    public string RestfulBookerUrl { get; set; } = "https://restful-booker.herokuapp.com";
}
