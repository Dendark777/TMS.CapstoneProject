using NUnit.Framework;
using Reqnroll;
using TMS.CapstoneProject.UI.Pages;

namespace TMS.CapstoneProject.Tests.Bdd.Steps;

[Binding]
public sealed class SabyUiSteps
{
    private HomePage? _home;
    private ContactsPage? _contacts;
    private AuthPage? _auth;

    [When(@"я открываю главную страницу Saby")]
    [Given(@"я открываю главную страницу Saby")]
    public void OpenHome() => _home = new HomePage().Open();

    [Then(@"заголовок страницы содержит ""(.*)""")]
    public void TitleContains(string text)
    {
        Assert.That(_home!.Title, Does.Contain(text).IgnoreCase);
    }

    [Then(@"отображается логотип")]
    public void LogoIsVisible()
    {
        Assert.That(_home!.Header.LogoIsDisplayed(), Is.True);
    }

    [When(@"я открываю раздел ""(.*)"" в шапке")]
    public void OpenHeaderSection(string name)
    {
        _contacts = name switch
        {
            "Контакты" => _home!.OpenContacts(),
            _ => throw new ArgumentOutOfRangeException(nameof(name), name, "Unknown header item")
        };
    }

    [Then(@"текущий url содержит ""(.*)""")]
    public void UrlContains(string fragment)
    {
        var url = _contacts?.Url ?? _home?.Url ?? _auth?.Url ?? string.Empty;
        Assert.That(url, Does.Contain(fragment).IgnoreCase);
    }

    [When(@"я открываю страницу контактов")]
    public void OpenContacts() => _contacts = new ContactsPage().Open();

    [Then(@"отображается телефон")]
    public void PhoneVisible() => Assert.That(_contacts!.PhoneIsDisplayed(), Is.True);

    [Then(@"отображается адрес офиса")]
    public void AddressVisible() => Assert.That(_contacts!.AddressIsDisplayed(), Is.True);

    [When(@"я открываю страницу авторизации")]
    [Given(@"я открываю страницу авторизации")]
    public void OpenAuth() => _auth = new AuthPage().Open();

    [Then(@"отображается форма входа")]
    public void LoginFormVisible() => Assert.That(_auth!.LoginFormIsDisplayed(), Is.True);

    [When(@"я ввожу логин ""(.*)"" и пароль ""(.*)""")]
    public void FillCredentials(string login, string password)
    {
        _auth!.FillLogin(login).FillPassword(password);
    }

    [When(@"нажимаю кнопку входа")]
    public void SubmitAuth() => _auth!.Submit();

    [Then(@"я остаюсь на странице авторизации")]
    public void StayOnAuth() => Assert.That(_auth!.StaysOnAuthOrShowsError(), Is.True);
}
