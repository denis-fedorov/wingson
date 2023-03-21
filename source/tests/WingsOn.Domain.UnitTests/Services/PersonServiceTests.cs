using FluentAssertions;
using FluentAssertions.Execution;
using Moq;
using WingsOn.Domain.Entities;
using WingsOn.Domain.Exceptions;
using WingsOn.Domain.Interfaces;
using WingsOn.Domain.Services;
using Xunit;

namespace WingsOn.Domain.UnitTests.Services;

public class PersonServiceTests
{
    [Fact]
    public void Get_NonExistingPerson_ThrowsException()
    {
        const int personId = 100;
        const Person? nullPerson = null;
        
        var personRepository = new Mock<IRepository<Person>>();
        personRepository
            .Setup(_ => _.Get(personId))
            .Returns(nullPerson);

        var service = new PersonService(personRepository.Object);

        var act = () => service.Get(personId);

        act.Should().ThrowExactly<PersonNotFoundException>();
    }

    [Fact]
    public void Get_ExistingPerson_ReturnsPerson()
    {
        var person = new Person { Id = 100, Name = "John Doe" };
        var personId = person.Id;
        
        var personRepository = new Mock<IRepository<Person>>();
        personRepository
            .Setup(_ => _.Get(personId))
            .Returns(person);

        var service = new PersonService(personRepository.Object);

        var result = service.Get(personId);

        result.Should().BeEquivalentTo(person);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void UpdateAddress_NonOrEmptyAddress_ThrowsException(string address)
    {
        var personRepository = Mock.Of<IRepository<Person>>();

        var service = new PersonService(personRepository);

        var act = () => service.UpdateAddress(100, address);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void UpdateAddress_ValidAddress_CallsRepo()
    {
        var person = new Person { Id = 100, Name = "John Doe", Address = "Old address"};
        var personId = person.Id;
        
        var personRepository = new Mock<IRepository<Person>>();
        personRepository
            .Setup(_ => _.Get(personId))
            .Returns(person);

        const string newAddress = "New address";

        var service = new PersonService(personRepository.Object);

        var act = () => service.UpdateAddress(personId, newAddress);

        using (new AssertionScope())
        {
            act.Should().NotThrow();
            personRepository.Verify(_ => 
                _.Save(It.Is<Person>(p => p.Id == personId && p.Address == newAddress)),
                    Times.Once());
        }
    }
}