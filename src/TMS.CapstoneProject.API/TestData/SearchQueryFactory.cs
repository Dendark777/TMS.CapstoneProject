using TMS.CapstoneProject.API.Clients;
using TMS.CapstoneProject.API.Models;

namespace TMS.CapstoneProject.API.TestData;

public static class SearchQueryFactory
{
    public static PlaceholderPost CreateHelpSearchQuery(JsonPlaceholderClient client)
    {
        var payload = new PlaceholderPost
        {
            UserId = 1,
            Title = "Электронный документооборот",
            Body = "Тестовые данные для поиска по базе знаний Saby"
        };

        var response = client.CreatePost(payload);
        if (response.Data is null || (int)response.StatusCode is < 200 or >= 300)
        {
            return payload;
        }

        return response.Data;
    }
}
