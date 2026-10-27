using Allure.NUnit;
using Allure.NUnit.Attributes;
using NUnit.Framework;
using TMS.CapstoneProject.API.Clients;
using TMS.CapstoneProject.API.TestData;

namespace TMS.CapstoneProject.Tests.API;

[TestFixture]
[AllureNUnit]
[AllureEpic("Saby")]
[AllureFeature("API")]
[AllureStory("Restful Booker CRUD")]
[Category("API")]
[Category("Regression")]
public sealed class RestfulBookerApiTests
{
    private RestfulBookerClient _client = null!;

    [SetUp]
    public void SetUp() => _client = new RestfulBookerClient();

    [TearDown]
    public void TearDown() => _client.Dispose();

    [Test]
    [AllureName("CRUD бронирования: создать, прочитать, обновить, удалить")]
    [Category("Smoke")]
    public void Booking_CrudLifecycle()
    {
        var token = _client.CreateToken().Data?.Token;
        Assert.That(token, Is.Not.Null.And.Not.Empty);

        var created = _client.CreateBooking(BookingFactory.Create("Denis", "Diploma"));
        Assert.That((int)created.StatusCode, Is.EqualTo(200), created.Content);
        Assert.That(created.Data, Is.Not.Null);
        var id = created.Data!.BookingId;
        Assert.That(id, Is.GreaterThan(0));

        var fetched = _client.GetBooking(id);
        Assert.That((int)fetched.StatusCode, Is.EqualTo(200));
        Assert.That(fetched.Data!.Firstname, Is.EqualTo("Denis"));

        var updatedPayload = BookingFactory.Create("Denis", "Updated");
        var updated = _client.UpdateBooking(id, updatedPayload, token!);
        Assert.That((int)updated.StatusCode, Is.EqualTo(200));
        Assert.That(updated.Data!.Lastname, Is.EqualTo("Updated"));

        var deleted = _client.DeleteBooking(id, token!);
        Assert.That((int)deleted.StatusCode, Is.InRange(200, 201));
    }

    [Test]
    [AllureName("GET /booking возвращает список идентификаторов")]
    public void GetBookingIds_ReturnsList()
    {
        var response = _client.GetBookingIds();

        Assert.That((int)response.StatusCode, Is.EqualTo(200));
        Assert.That(response.Data, Is.Not.Null);
        Assert.That(response.Data!, Has.Count.GreaterThan(0));
    }
}
