using Allure.NUnit;
using Allure.NUnit.Attributes;
using NUnit.Framework;
using TMS.CapstoneProject.API.Clients;
using TMS.CapstoneProject.API.Models;

namespace TMS.CapstoneProject.Tests.API;

[TestFixture]
[AllureNUnit]
[Parallelizable(ParallelScope.Fixtures)]
[AllureEpic("Saby")]
[AllureFeature("API")]
[AllureStory("JSONPlaceholder")]
[Category("API")]
[Category("Regression")]
public sealed class JsonPlaceholderApiTests
{
    private JsonPlaceholderClient _client = null!;

    [SetUp]
    public void SetUp() => _client = new JsonPlaceholderClient();

    [TearDown]
    public void TearDown() => _client.Dispose();

    [Test]
    [AllureName("GET /posts возвращает список публикаций")]
    [Category("Smoke")]
    public void GetPosts_ReturnsCollection()
    {
        var response = _client.GetPosts();

        Assert.Multiple(() =>
        {
            Assert.That((int)response.StatusCode, Is.EqualTo(200));
            Assert.That(response.Data, Is.Not.Null);
            Assert.That(response.Data!, Has.Count.GreaterThan(10));
        });
    }

    [Test]
    [AllureName("GET /posts/1 возвращает конкретную публикацию")]
    public void GetPost_ReturnsItem()
    {
        var response = _client.GetPost(1);

        Assert.Multiple(() =>
        {
            Assert.That((int)response.StatusCode, Is.EqualTo(200));
            Assert.That(response.Data, Is.Not.Null);
            Assert.That(response.Data!.Id, Is.EqualTo(1));
            Assert.That(response.Data.Title, Is.Not.Empty);
        });
    }

    [Test]
    [AllureName("POST /posts создаёт тестовые данные")]
    public void CreatePost_ReturnsCreatedResource()
    {
        var payload = new PlaceholderPost
        {
            UserId = 1,
            Title = "Saby diploma search query",
            Body = "Документооборот"
        };

        var response = _client.CreatePost(payload);

        Assert.Multiple(() =>
        {
            Assert.That((int)response.StatusCode, Is.EqualTo(201));
            Assert.That(response.Data, Is.Not.Null);
            Assert.That(response.Data!.Id, Is.GreaterThan(0));
            Assert.That(response.Data.Title, Is.EqualTo(payload.Title));
        });
    }

    [Test]
    [AllureName("PUT /posts/{id} обновляет публикацию")]
    public void UpdatePost_ReturnsUpdatedResource()
    {
        var payload = new PlaceholderPost { UserId = 1, Title = "Updated", Body = "Body" };
        var response = _client.UpdatePost(1, payload);

        Assert.That((int)response.StatusCode, Is.EqualTo(200));
        Assert.That(response.Data, Is.Not.Null);
        Assert.That(response.Data!.Title, Is.EqualTo("Updated"));
    }

    [Test]
    [AllureName("DELETE /posts/{id} удаляет публикацию")]
    public void DeletePost_ReturnsSuccess()
    {
        var response = _client.DeletePost(1);
        Assert.That((int)response.StatusCode, Is.InRange(200, 204));
    }
}
