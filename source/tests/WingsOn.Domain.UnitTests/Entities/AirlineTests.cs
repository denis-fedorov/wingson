using FluentAssertions;
using WingsOn.Domain.Entities;
using Xunit;

namespace WingsOn.Domain.UnitTests.Entities;

public class AirlineTests
{
    [Fact]
    public void Airline_SetProperties()
    {
        // Arrange
        var airline = new Airline();

        // Act
        airline.Id = 1;
        airline.Code = "ABC";
        airline.Name = "Airline";
        airline.Address = "123 Main St.";

        // Assert
        airline.Id.Should().Be(1);
        airline.Code.Should().Be("ABC");
        airline.Name.Should().Be("Airline");
        airline.Address.Should().Be("123 Main St.");
    }
}