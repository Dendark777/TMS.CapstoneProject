using Allure.NUnit.Attributes;
using NUnit.Framework;
using TMS.CapstoneProject.Tests.UI.Infrastructure;
using TMS.CapstoneProject.UI.Pages;

namespace TMS.CapstoneProject.Tests.UI;

[TestFixture]
[AllureEpic("Saby")]
[AllureFeature("UI")]
[AllureStory("Продукты")]
[Category("UI")]
[Category("Regression")]
public sealed class ProductsTests : UiTestBase
{
    [Test]
    [AllureName("Каталог продуктов открывается по прямому URL")]
    [Category("Smoke")]
    public void ProductsPage_Opens_ByDirectUrl()
    {
        var page = new ProductsPage().Open();

        Assert.Multiple(() =>
        {
            Assert.That(page.IsOpened(), Is.True);
            Assert.That(page.Title, Does.Contain("продукт").IgnoreCase.Or.Contain("Saby").IgnoreCase);
            Assert.That(page.HasProductCards(), Is.True);
        });
    }
}
