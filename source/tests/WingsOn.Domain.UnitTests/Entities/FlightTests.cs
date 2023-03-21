using FluentAssertions;
using WingsOn.Domain.Entities;
using Xunit;

namespace WingsOn.Domain.UnitTests.Entities;

public class FlightTests
{
    [Fact]
    public void Flight_SetProperties()
    {
        // Arrange
        var flight = new Flight();
        var airline = new Airline { Id = 1, Name = "Delta", Code = "DL" };
        var departureAirport = new Airport { Id = 1, Country = "USA", City = "New York", Code = "JFK" };
        var arrivalAirport = new Airport { Id = 2, Country = "USA", City = "Los Angeles", Code = "LAX" };
        var departureDate = new DateTime(2023, 3, 25, 8, 0, 0);
        var arrivalDate = new DateTime(2023, 3, 25, 11, 0, 0);
        const decimal price = 300m;

        // Act
        flight.Id = 1;
        flight.Number = "DL123";
        flight.Carrier = airline;
        flight.DepartureAirport = departureAirport;
        flight.DepartureDate = departureDate;
        flight.ArrivalAirport = arrivalAirport;
        flight.ArrivalDate = arrivalDate;
        flight.Price = price;

        // Assert
        flight.Id.Should().Be(1);
        flight.Number.Should().Be("DL123");
        flight.Carrier.Should().Be(airline);
        flight.DepartureAirport.Should().Be(departureAirport);
        flight.DepartureDate.Should().Be(departureDate);
        flight.ArrivalAirport.Should().Be(arrivalAirport);
        flight.ArrivalDate.Should().Be(arrivalDate);
        flight.Price.Should().Be(price);
    }
}
