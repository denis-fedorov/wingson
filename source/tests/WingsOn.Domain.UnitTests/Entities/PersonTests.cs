using FluentAssertions;
using WingsOn.Domain.Entities;
using Xunit;

namespace WingsOn.Domain.UnitTests.Entities;

public class PersonTests
{
    [Fact]
    public void Person_SetProperties()
    {
        // Arrange
        var person = new Person();

        // Act
        person.Id = 1;
        person.Name = "John";
        person.DateBirth = new DateTime(1980, 1, 1);
        person.Gender = GenderType.Male;
        person.Address = "123 Main St.";
        person.Email = "john@example.com";

        // Assert
        person.Id.Should().Be(1);
        person.Name.Should().Be("John");
        person.DateBirth.Should().Be(new DateTime(1980, 1, 1));
        person.Gender.Should().Be(GenderType.Male);
        person.Address.Should().Be("123 Main St.");
        person.Email.Should().Be("john@example.com");
    }
}