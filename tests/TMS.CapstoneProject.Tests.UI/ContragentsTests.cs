using Allure.NUnit.Attributes;
using NUnit.Framework;
using TMS.CapstoneProject.Tests.UI.Infrastructure;
using TMS.CapstoneProject.UI.Pages;

namespace TMS.CapstoneProject.Tests.UI;

[TestFixture]
[AllureEpic("Saby")]
[AllureFeature("UI")]
[AllureStory("Контрагенты")]
[Category("UI")]
[Category("Regression")]
public sealed class ContragentsTests : UiTestBase
{
    [Test]
    [AllureName("Страница проверки контрагентов доступна без авторизации")]
    public void ContragentsPage_Opens_PublicSearch()
    {
        var page = new ContragentsPage().Open();

        Assert.Multiple(() =>
        {
            Assert.That(page.IsOpened(), Is.True);
            Assert.That(page.HeadingMentionsContragents(), Is.True);
            Assert.That(page.SearchIsDisplayed(), Is.True);
        });
    }
}
