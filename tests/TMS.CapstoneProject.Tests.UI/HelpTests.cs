using Allure.NUnit.Attributes;
using NUnit.Framework;
using TMS.CapstoneProject.API.Clients;
using TMS.CapstoneProject.API.TestData;
using TMS.CapstoneProject.Tests.UI.Infrastructure;
using TMS.CapstoneProject.UI.Pages;

namespace TMS.CapstoneProject.Tests.UI;

[TestFixture]
[AllureEpic("Saby")]
[AllureFeature("UI")]
[AllureStory("База знаний")]
[Category("UI")]
[Category("Regression")]
public sealed class HelpTests : UiTestBase
{
    [Test]
    [AllureName("База знаний открывается и показывает список сервисов")]
    [Category("Smoke")]
    public void HelpPage_Opens_WithServices()
    {
        var page = new HelpPage().Open();

        Assert.Multiple(() =>
        {
            Assert.That(page.IsOpened(), Is.True);
            Assert.That(page.TitleIsDisplayed(), Is.True);
            Assert.That(page.ServiceCount(), Is.GreaterThan(5));
        });
    }

    [Test]
    [AllureName("Поиск в помощи использует данные, созданные через API")]
    public void HelpPage_Search_UsesDataCreatedViaApi()
    {
        using var api = new JsonPlaceholderClient();
        var query = SearchQueryFactory.CreateHelpSearchQuery(api);

        var page = new HelpPage().Open().Search(query.Title);

        Assert.That(page.HasResultsOrContent(), Is.True);
        Assert.That(page.Url, Does.Contain("help").IgnoreCase);
    }
}
