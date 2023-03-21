using FluentAssertions;
using WingsOn.Domain.Entities;
using Xunit;

namespace WingsOn.Domain.UnitTests.Entities;

public class BookingTests
{
    [Fact]
    public void Booking_GetNewNumber_ReturnsGuidString()
    {
        // Arrange

        // Act
        var number = Booking.GetNewNumber;

        // Assert
        Guid.TryParse(number, out _).Should().BeTrue();
    }
    
    [Fact]
    public void Booking_SetProperties()
    {
        // Arrange
        var booking = new Booking();

        // Act
        booking.Id = 1;
        booking.Number = "ABC123";
        var flight = new Flight();
        booking.Flight = flight;
        var customer = new Person();
        booking.Customer = customer;
        var passengers = new List<Person> { customer };
        booking.Passengers = passengers;
        booking.DateBooking = DateTime.Now;

        // Assert
        booking.Id.Should().Be(1);
        booking.Number.Should().Be("ABC123");
        booking.Flight.Should().BeEquivalentTo(flight);
        booking.Customer.Should().BeEquivalentTo(customer);
        booking.Passengers.Should().BeEquivalentTo(new List<Person> { customer });
        booking.DateBooking.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
    }
}