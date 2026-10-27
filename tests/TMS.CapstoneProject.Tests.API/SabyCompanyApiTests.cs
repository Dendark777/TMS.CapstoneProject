using Allure.NUnit;
using Allure.NUnit.Attributes;
using NUnit.Framework;
using TMS.CapstoneProject.API.Clients;

namespace TMS.CapstoneProject.Tests.API;

[TestFixture]
[AllureNUnit]
[AllureEpic("Saby")]
[AllureFeature("API")]
[AllureStory("SppAPI")]
[Category("API")]
[Category("Regression")]
public sealed class SabyCompanyApiTests
{
    [Test]
    [AllureName("SppAPI.Requisites без лицензии возвращает 401")]
    public void Requisites_WithoutLicense_ReturnsUnauthorized()
    {
        using var client = new SabyCompanyApiClient();
        var response = client.GetRequisitesUnauthorized("7605016030");

        Assert.That(response.StatusCode, Is.EqualTo(401));
    }
}
