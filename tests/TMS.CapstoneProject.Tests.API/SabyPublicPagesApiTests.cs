using Allure.NUnit;
using Allure.NUnit.Attributes;
using NUnit.Framework;
using RestSharp;
using TMS.CapstoneProject.API.Clients;

namespace TMS.CapstoneProject.Tests.API;

[TestFixture]
[AllureNUnit]
[Parallelizable(ParallelScope.None)]
[AllureEpic("Saby")]
[AllureFeature("API")]
[AllureStory("Публичные страницы")]
[Category("API")]
[Category("Smoke")]
public sealed class SabyPublicPagesApiTests
{
    private SabyWebApiClient _client = null!;

    [SetUp]
    public void SetUp() => _client = new SabyWebApiClient();

    [TearDown]
    public void TearDown() => _client.Dispose();

    [TestCase("/")]
    [TestCase("/products")]
    [TestCase("/contacts")]
    [TestCase("/tariffs")]
    [TestCase("/help")]
    [TestCase("/contragents")]
    [AllureName("Публичная страница Saby отвечает 200")]
    public void PublicPage_ReturnsOk(string path)
    {
        var response = _client.GetPage(path);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(200));
            Assert.That(response.Body, Does.Contain("Saby").IgnoreCase.Or.Contain("saby"));
            Assert.That(response.ElapsedMilliseconds, Is.LessThan(15_000));
        });
    }

    [Test]
    [AllureName("Страница авторизации online.saby.ru доступна")]
    [Category("Regression")]
    public void AuthPage_ReturnsOk()
    {
        using var client = RestClientFactory.Create("https://online.saby.ru");
        var request = new RestRequest("auth/", Method.Get);
        request.AddHeader("Accept", "text/html");
        var response = client.Execute(request);

        Assert.That((int)response.StatusCode, Is.EqualTo(200));
        Assert.That(response.Content, Does.Contain("auth").IgnoreCase.Or.Contain("вход").IgnoreCase.Or.Contain("Saby").IgnoreCase);
    }
}
