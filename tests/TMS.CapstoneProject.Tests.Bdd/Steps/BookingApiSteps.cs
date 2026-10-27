using NUnit.Framework;
using Reqnroll;
using TMS.CapstoneProject.API.Clients;
using TMS.CapstoneProject.API.Models;
using TMS.CapstoneProject.API.TestData;

namespace TMS.CapstoneProject.Tests.Bdd.Steps;

[Binding]
public sealed class BookingApiSteps : IDisposable
{
    private readonly RestfulBookerClient _client = new();
    private CreatedBooking? _created;

    [When(@"я создаю бронирование через API с именем ""(.*)"" и фамилией ""(.*)""")]
    public void CreateBooking(string firstName, string lastName)
    {
        var response = _client.CreateBooking(BookingFactory.Create(firstName, lastName));
        Assert.That((int)response.StatusCode, Is.EqualTo(200));
        _created = response.Data;
    }

    [Then(@"API возвращает идентификатор бронирования")]
    public void BookingIdReturned()
    {
        Assert.That(_created, Is.Not.Null);
        Assert.That(_created!.BookingId, Is.GreaterThan(0));
    }

    [Then(@"бронирование можно прочитать по идентификатору")]
    public void BookingCanBeRead()
    {
        var fetched = _client.GetBooking(_created!.BookingId);
        Assert.That((int)fetched.StatusCode, Is.EqualTo(200));
        Assert.That(fetched.Data, Is.Not.Null);
        Assert.That(fetched.Data!.Firstname, Is.EqualTo(_created.Booking.Firstname));
    }

    public void Dispose() => _client.Dispose();
}
