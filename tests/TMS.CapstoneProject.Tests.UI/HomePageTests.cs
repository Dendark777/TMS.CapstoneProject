using Allure.NUnit.Attributes;
using NUnit.Framework;
using TMS.CapstoneProject.Tests.UI.Infrastructure;
using TMS.CapstoneProject.UI.Pages;

namespace TMS.CapstoneProject.Tests.UI;

[TestFixture]
[AllureEpic("Saby")]
[AllureFeature("UI")]
[AllureStory("Главная страница")]
[Category("UI")]
[Category("Smoke")]
public sealed class HomePageTests : UiTestBase
{
    [Test]
    [AllureName("Главная страница открывается и содержит логотип Saby")]
    public void HomePage_Opens_WithLogoAndHero()
    {
        var page = new HomePage().Open();

        Assert.Multiple(() =>
        {
            Assert.That(page.Title, Does.Contain("Saby").IgnoreCase);
            Assert.That(page.Header.IsDisplayed(), Is.True);
            Assert.That(page.Header.LogoIsDisplayed(), Is.True);
            Assert.That(page.HeroIsDisplayed(), Is.True);
        });
    }

    [Test]
    [AllureName("На главной отображаются ключевые продукты")]
    [Category("Regression")]
    public void HomePage_ShowsKeyProducts()
    {
        var page = new HomePage().Open();

        Assert.Multiple(() =>
        {
            Assert.That(page.ProductIsDisplayed("Бухгалтерия и учет"), Is.True);
            Assert.That(page.ProductIsDisplayed("Документооборот"), Is.True);
            Assert.That(page.ProductIsDisplayed("CRM"), Is.True);
        });
    }
}
