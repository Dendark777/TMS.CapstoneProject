using System.Diagnostics;
using RestSharp;
using TMS.CapstoneProject.API.Models;
using TMS.CapstoneProject.Core.Configuration;
using TMS.CapstoneProject.Core.Logging;

namespace TMS.CapstoneProject.API.Clients;

public sealed class SabyWebApiClient : IDisposable
{
    private readonly RestClient _client;

    public SabyWebApiClient()
    {
        _client = RestClientFactory.Create(ConfigManager.Settings.Api.SabyBaseUrl);
    }

    public ApiResponseSnapshot GetPage(string relativeUrl)
    {
        var request = new RestRequest(relativeUrl, Method.Get);
        request.AddHeader("Accept", "text/html,application/xhtml+xml");
        return ExecuteWithRetry(request);
    }

    private ApiResponseSnapshot ExecuteWithRetry(RestRequest request, int attempts = 3)
    {
        ApiResponseSnapshot? last = null;
        for (var i = 1; i <= attempts; i++)
        {
            var sw = Stopwatch.StartNew();
            var response = _client.Execute(request);
            sw.Stop();
            last = ApiResponseSnapshot.From(response, sw.Elapsed);
            TestLogger.Info($"{request.Method} {request.Resource} -> {last.StatusCode} in {last.ElapsedMilliseconds} ms {last.ErrorMessage}");

            if (last.StatusCode is >= 200 and < 500)
            {
                return last;
            }

            Thread.Sleep(TimeSpan.FromMilliseconds(400 * i));
        }

        return last!;
    }

    public void Dispose() => _client.Dispose();
}
