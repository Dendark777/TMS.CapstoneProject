using RestSharp;
using TMS.CapstoneProject.API.Models;
using TMS.CapstoneProject.Core.Configuration;
using TMS.CapstoneProject.Core.Logging;

namespace TMS.CapstoneProject.API.Clients;

public sealed class RestfulBookerClient : IDisposable
{
    private readonly RestClient _client;

    public RestfulBookerClient()
    {
        _client = RestClientFactory.Create(ConfigManager.Settings.Api.RestfulBookerUrl);
    }

    public RestResponse<AuthToken> CreateToken(string username = "admin", string password = "password123")
    {
        var request = new RestRequest("auth", Method.Post);
        request.AddJsonBody(new { username, password });
        var response = _client.Execute<AuthToken>(request);
        TestLogger.Info($"POST auth -> {(int)response.StatusCode}");
        return response;
    }

    public RestResponse<List<BookingId>> GetBookingIds()
    {
        var request = new RestRequest("booking", Method.Get);
        var response = _client.Execute<List<BookingId>>(request);
        TestLogger.Info($"GET booking -> {(int)response.StatusCode}, count={response.Data?.Count}");
        return response;
    }

    public RestResponse<CreatedBooking> CreateBooking(Booking booking)
    {
        RestResponse<CreatedBooking>? response = null;
        for (var attempt = 1; attempt <= 3; attempt++)
        {
            var request = new RestRequest("booking", Method.Post);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(booking);
            response = _client.Execute<CreatedBooking>(request);
            TestLogger.Info($"POST booking -> {(int)response.StatusCode}, id={response.Data?.BookingId}");
            if ((int)response.StatusCode is >= 200 and < 300)
            {
                return response;
            }

            Thread.Sleep(500 * attempt);
        }

        return response!;
    }

    public RestResponse<Booking> GetBooking(int id)
    {
        var request = new RestRequest($"booking/{id}", Method.Get);
        request.AddHeader("Accept", "application/json");
        var response = _client.Execute<Booking>(request);
        TestLogger.Info($"GET booking/{id} -> {(int)response.StatusCode}");
        return response;
    }

    public RestResponse<Booking> UpdateBooking(int id, Booking booking, string token)
    {
        var request = new RestRequest($"booking/{id}", Method.Put);
        request.AddHeader("Accept", "application/json");
        request.AddHeader("Cookie", $"token={token}");
        request.AddJsonBody(booking);
        var response = _client.Execute<Booking>(request);
        TestLogger.Info($"PUT booking/{id} -> {(int)response.StatusCode}");
        return response;
    }

    public RestResponse DeleteBooking(int id, string token)
    {
        var request = new RestRequest($"booking/{id}", Method.Delete);
        request.AddHeader("Cookie", $"token={token}");
        var response = _client.Execute(request);
        TestLogger.Info($"DELETE booking/{id} -> {(int)response.StatusCode}");
        return response;
    }

    public void Dispose() => _client.Dispose();
}
