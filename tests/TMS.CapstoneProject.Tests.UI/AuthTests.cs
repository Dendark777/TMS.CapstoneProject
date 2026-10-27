using Allure.NUnit.Attributes;
using NUnit.Framework;
using TMS.CapstoneProject.Tests.UI.Infrastructure;
using TMS.CapstoneProject.UI.Pages;

namespace TMS.CapstoneProject.Tests.UI;

[TestFixture]
[AllureEpic("Saby")]
[AllureFeature("UI")]
[AllureStory("Авторизация")]
[Category("UI")]
[Category("Regression")]
public sealed class AuthTests : UiTestBase
{
    [Test]
    [AllureName("Форма входа открывается на online.saby.ru/auth")]
    [Category("Smoke")]
    public void AuthPage_Opens_LoginForm()
    {
        var page = new AuthPage().Open();

        Assert.Multiple(() =>
        {
            Assert.That(page.IsOpened(), Is.True);
            Assert.That(page.LoginFormIsDisplayed(), Is.True);
        });
    }

    [Test]
    [AllureName("Невалидные учетные данные не открывают кабинет")]
    public void AuthPage_InvalidCredentials_StayOnAuth()
    {
        var page = new AuthPage()
            .Open()
            .FillLogin("diploma.invalid.user")
            .FillPassword("WrongPassword!123")
            .Submit();

        Assert.That(page.StaysOnAuthOrShowsError(), Is.True);
    }
}
