using RestSharp;

namespace TMS.CapstoneProject.API.Models;

public sealed record ApiResponseSnapshot(
    int StatusCode,
    string ContentType,
    string Body,
    long ElapsedMilliseconds,
    string ErrorMessage)
{
    public static ApiResponseSnapshot From(RestResponse response, TimeSpan elapsed)
    {
        return new ApiResponseSnapshot(
            (int)response.StatusCode,
            response.ContentType ?? string.Empty,
            response.Content ?? string.Empty,
            (long)elapsed.TotalMilliseconds,
            response.ErrorMessage ?? string.Empty);
    }
}
