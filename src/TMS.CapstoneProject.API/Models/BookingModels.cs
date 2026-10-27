using System.Text.Json.Serialization;

namespace TMS.CapstoneProject.API.Models;

public sealed class BookingDates
{
    [JsonPropertyName("checkin")]
    public string Checkin { get; set; } = string.Empty;

    [JsonPropertyName("checkout")]
    public string Checkout { get; set; } = string.Empty;
}

public sealed class Booking
{
    [JsonPropertyName("firstname")]
    public string Firstname { get; set; } = string.Empty;

    [JsonPropertyName("lastname")]
    public string Lastname { get; set; } = string.Empty;

    [JsonPropertyName("totalprice")]
    public int TotalPrice { get; set; }

    [JsonPropertyName("depositpaid")]
    public bool DepositPaid { get; set; }

    [JsonPropertyName("bookingdates")]
    public BookingDates BookingDates { get; set; } = new();

    [JsonPropertyName("additionalneeds")]
    public string AdditionalNeeds { get; set; } = string.Empty;
}

public sealed class CreatedBooking
{
    [JsonPropertyName("bookingid")]
    public int BookingId { get; set; }

    [JsonPropertyName("booking")]
    public Booking Booking { get; set; } = new();
}

public sealed class AuthToken
{
    [JsonPropertyName("token")]
    public string Token { get; set; } = string.Empty;
}

public sealed class BookingId
{
    [JsonPropertyName("bookingid")]
    public int Id { get; set; }
}
