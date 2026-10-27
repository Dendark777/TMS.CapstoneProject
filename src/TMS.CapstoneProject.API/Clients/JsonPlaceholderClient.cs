using RestSharp;
using TMS.CapstoneProject.API.Models;
using TMS.CapstoneProject.Core.Configuration;
using TMS.CapstoneProject.Core.Logging;

namespace TMS.CapstoneProject.API.Clients;

public sealed class JsonPlaceholderClient : IDisposable
{
    private readonly RestClient _client;

    public JsonPlaceholderClient()
    {
        _client = RestClientFactory.Create(ConfigManager.Settings.Api.JsonPlaceholderUrl);
    }

    public RestResponse<PlaceholderPost> GetPost(int id)
    {
        var request = new RestRequest($"posts/{id}", Method.Get);
        var response = _client.Execute<PlaceholderPost>(request);
        TestLogger.Info($"GET posts/{id} -> {(int)response.StatusCode}");
        return response;
    }

    public RestResponse<List<PlaceholderPost>> GetPosts()
    {
        var request = new RestRequest("posts", Method.Get);
        var response = _client.Execute<List<PlaceholderPost>>(request);
        TestLogger.Info($"GET posts -> {(int)response.StatusCode}, count={response.Data?.Count}");
        return response;
    }

    public RestResponse<PlaceholderPost> CreatePost(PlaceholderPost post)
    {
        var request = new RestRequest("posts", Method.Post);
        request.AddJsonBody(post);
        var response = _client.Execute<PlaceholderPost>(request);
        TestLogger.Info($"POST posts -> {(int)response.StatusCode}, id={response.Data?.Id}");
        return response;
    }

    public RestResponse<PlaceholderPost> UpdatePost(int id, PlaceholderPost post)
    {
        var request = new RestRequest($"posts/{id}", Method.Put);
        request.AddJsonBody(post);
        var response = _client.Execute<PlaceholderPost>(request);
        TestLogger.Info($"PUT posts/{id} -> {(int)response.StatusCode}");
        return response;
    }

    public RestResponse DeletePost(int id)
    {
        var request = new RestRequest($"posts/{id}", Method.Delete);
        var response = _client.Execute(request);
        TestLogger.Info($"DELETE posts/{id} -> {(int)response.StatusCode}");
        return response;
    }

    public void Dispose() => _client.Dispose();
}
