using FluentAssertions;
using WingsOn.Domain.Entities;
using Xunit;

namespace WingsOn.Domain.UnitTests.Entities;

public class AirportTests
{
    [Fact]
    public void Airport_SetProperties()
    {
        // Arrange
        var airport = new Airport();

        // Act
        airport.Id = 1;
        airport.Code = "ABC";
        airport.Country = "USA";
        airport.City = "New York";

        // Assert
        airport.Id.Should().Be(1);
        airport.Code.Should().Be("ABC");
        airport.Country.Should().Be("USA");
        airport.City.Should().Be("New York");
    }
}