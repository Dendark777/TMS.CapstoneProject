using TMS.CapstoneProject.API.Models;
using TMS.CapstoneProject.Core.Logging;

namespace TMS.CapstoneProject.API.TestData;

public static class BookingFactory
{
    public static Booking Create(string? firstName = null, string? lastName = null)
    {
        var booking = new Booking
        {
            Firstname = firstName ?? $"Saby{DateTime.Now:HHmmss}",
            Lastname = lastName ?? "Tester",
            TotalPrice = 1200,
            DepositPaid = true,
            AdditionalNeeds = "Breakfast",
            BookingDates = new BookingDates
            {
                Checkin = DateTime.Today.AddDays(14).ToString("yyyy-MM-dd"),
                Checkout = DateTime.Today.AddDays(17).ToString("yyyy-MM-dd")
            }
        };

        TestLogger.Info($"Prepared booking {booking.Firstname} {booking.Lastname}");
        return booking;
    }
}
