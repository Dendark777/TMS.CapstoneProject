using RestSharp;
using TMS.CapstoneProject.Core.Logging;

namespace TMS.CapstoneProject.API.Clients;

public static class RestClientFactory
{
    public const string BrowserUserAgent =
        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36";

    public static RestClient Create(string baseUrl)
    {
        TestLogger.Info($"Create RestClient for {baseUrl}");
        var options = new RestClientOptions(baseUrl)
        {
            ThrowOnAnyError = false,
            Timeout = TimeSpan.FromSeconds(45),
            UserAgent = BrowserUserAgent,
            FollowRedirects = true,
            AutomaticDecompression = System.Net.DecompressionMethods.All
        };

        var client = new RestClient(options);
        client.AddDefaultHeader("Accept-Language", "ru-RU,ru;q=0.9,en;q=0.8");
        return client;
    }
}
