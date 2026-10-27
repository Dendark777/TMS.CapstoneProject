using Allure.NUnit.Attributes;
using NUnit.Framework;
using TMS.CapstoneProject.Tests.UI.Infrastructure;
using TMS.CapstoneProject.UI.Pages;

namespace TMS.CapstoneProject.Tests.UI;

[TestFixture]
[AllureEpic("Saby")]
[AllureFeature("UI")]
[AllureStory("Навигация")]
[Category("UI")]
[Category("Smoke")]
public sealed class NavigationTests : UiTestBase
{
    [Test]
    [AllureName("Переход в Тарифы из шапки")]
    public void Header_OpensTariffs()
    {
        var tariffs = new HomePage().Open().OpenTariffs();

        Assert.That(tariffs.IsOpened(), Is.True);
        Assert.That(tariffs.TitleLooksValid(), Is.True);
    }

    [Test]
    [AllureName("Переход в Контакты из шапки")]
    public void Header_OpensContacts()
    {
        var contacts = new HomePage().Open().OpenContacts();

        Assert.That(contacts.IsOpened(), Is.True);
    }

    [Test]
    [AllureName("Переход в Поддержку из шапки")]
    public void Header_OpensSupport()
    {
        var support = new HomePage().Open().OpenSupport();

        Assert.That(support.IsOpened(), Is.True);
        Assert.That(support.TitleLooksValid(), Is.True);
    }
}
