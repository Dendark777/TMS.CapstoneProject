using System.Diagnostics;
using RestSharp;
using TMS.CapstoneProject.API.Models;
using TMS.CapstoneProject.Core.Configuration;
using TMS.CapstoneProject.Core.Logging;

namespace TMS.CapstoneProject.API.Clients;

public sealed class SabyCompanyApiClient : IDisposable
{
    private readonly RestClient _client;

    public SabyCompanyApiClient()
    {
        _client = RestClientFactory.Create(ConfigManager.Settings.Api.SppApiUrl);
    }

    public ApiResponseSnapshot GetRequisitesUnauthorized(string inn)
    {
        var request = new RestRequest(string.Empty, Method.Post);
        request.AddHeader("Content-Type", "application/json; charset=utf-8");
        request.AddJsonBody(new
        {
            jsonrpc = "2.0",
            protocol = 6,
            method = "SppAPI.Requisites",
            @params = new { inn, ogrn = (string?)null },
            id = 1
        });

        var sw = Stopwatch.StartNew();
        var response = _client.Execute(request);
        sw.Stop();
        TestLogger.Info($"SppAPI.Requisites inn={inn} -> {(int)response.StatusCode}");
        return ApiResponseSnapshot.From(response, sw.Elapsed);
    }

    public void Dispose() => _client.Dispose();
}
