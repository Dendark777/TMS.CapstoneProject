using Allure.NUnit.Attributes;
using NUnit.Framework;
using TMS.CapstoneProject.Tests.UI.Infrastructure;
using TMS.CapstoneProject.UI.Pages;

namespace TMS.CapstoneProject.Tests.UI;

[TestFixture]
[AllureEpic("Saby")]
[AllureFeature("UI")]
[AllureStory("Контакты")]
[Category("UI")]
[Category("Regression")]
public sealed class ContactsTests : UiTestBase
{
    [Test]
    [AllureName("Страница контактов показывает телефон и офисы")]
    [Category("Smoke")]
    public void ContactsPage_ShowsPhoneAndOffices()
    {
        var page = new ContactsPage().Open();

        Assert.Multiple(() =>
        {
            Assert.That(page.IsOpened(), Is.True);
            Assert.That(page.PhoneIsDisplayed(), Is.True);
            Assert.That(page.AddressIsDisplayed(), Is.True);
            Assert.That(page.OfficesAreDisplayed(), Is.True);
        });
    }
}
