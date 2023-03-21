using FluentAssertions;
using FluentAssertions.Execution;
using Moq;
using WingsOn.Domain.Entities;
using WingsOn.Domain.Exceptions;
using WingsOn.Domain.Interfaces;
using WingsOn.Domain.Services;
using Xunit;

namespace WingsOn.Domain.UnitTests.Services;

public class FlightServiceTests
{
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void GetPassengers_NullOrEmptyFlightNumber_ThrowsException(string flightNumber)
    {
        var bookingRepository = Mock.Of<IRepository<Booking>>();
        var flightRepository = Mock.Of<IRepository<Flight>>();
        var personService = Mock.Of<IPersonService>();

        var service = new FlightService(bookingRepository, flightRepository, personService);

        var act = () => service.GetPassengers(flightNumber);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void GetPassengers_NonExistingFlight_ThrowsException()
    {
        var bookingRepository = Mock.Of<IRepository<Booking>>();
        var personService = Mock.Of<IPersonService>();
        var flightRepository = new Mock<IRepository<Flight>>();

        flightRepository
            .Setup(_ => _.GetAll())
            .Returns(Enumerable.Empty<Flight>());

        var service = new FlightService(bookingRepository, flightRepository.Object, personService);

        var act = () => service.GetPassengers("XYZ");

        act.Should().ThrowExactly<FlightNotFoundException>();
    }
    
    [Fact]
    public void GetPassengers_ExistingFlight_ReturnsPassengers()
    {
        var flight = new Flight { Id = 1, Number = "XYZ" };
        var person = new Person { Id = 10, Name = "John Doe" };
        var booking = new Booking { Id = 100, Number = "BOOK", Flight = flight, Passengers = new[] { person } };
        var flightNumber = flight.Number;
        
        var flightRepository = new Mock<IRepository<Flight>>();
        flightRepository
            .Setup(_ => _.GetAll())
            .Returns(new List<Flight> { flight });
        
        var bookingRepository = new Mock<IRepository<Booking>>();
        bookingRepository
            .Setup(_ => _.GetAll())
            .Returns(new List<Booking> { booking });
        
        var personService = Mock.Of<IPersonService>();

        var service = new FlightService(bookingRepository.Object, flightRepository.Object, personService);

        var result = service.GetPassengers(flightNumber);

        result.Should().BeEquivalentTo(new List<Person> { person });
    }
    
    [Theory]
    [InlineData(GenderType.Male)]
    [InlineData(GenderType.Female)]
    public void GetPassengers_ExistingFlightWithGenderFilter_ReturnsPassengers(GenderType genderType)
    {
        var flight = new Flight { Id = 1, Number = "XYZ" };
        var person = new Person { Id = 10, Name = "John Doe", Gender = genderType};
        var booking = new Booking { Id = 100, Number = "BOOK", Flight = flight, Passengers = new[] { person } };
        var flightNumber = flight.Number;
        
        var flightRepository = new Mock<IRepository<Flight>>();
        flightRepository
            .Setup(_ => _.GetAll())
            .Returns(new List<Flight> { flight });
        
        var bookingRepository = new Mock<IRepository<Booking>>();
        bookingRepository
            .Setup(_ => _.GetAll())
            .Returns(new List<Booking> { booking });
        
        var personService = Mock.Of<IPersonService>();

        var service = new FlightService(bookingRepository.Object, flightRepository.Object, personService);

        var result = service.GetPassengers(flightNumber, genderType);

        result.Should().BeEquivalentTo(new List<Person> { person });
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void ValidatePassengerForFlight_NullOrEmptyFlightNumber_ThrowsException(string flightNumber)
    {
        var bookingRepository = Mock.Of<IRepository<Booking>>();
        var flightRepository = Mock.Of<IRepository<Flight>>();
        var personService = Mock.Of<IPersonService>();

        var service = new FlightService(bookingRepository, flightRepository, personService);

        var act = () => service.ValidatePassengerForFlight(flightNumber, 10);

        act.Should().Throw<ArgumentException>();
    }
    
    [Fact]
    public void ValidatePassengerForFlight_NonExistingFlight_ThrowsException()
    {
        var bookingRepository = Mock.Of<IRepository<Booking>>();
        var personService = Mock.Of<IPersonService>();
        var flightRepository = new Mock<IRepository<Flight>>();

        flightRepository
            .Setup(_ => _.GetAll())
            .Returns(Enumerable.Empty<Flight>());

        var service = new FlightService(bookingRepository, flightRepository.Object, personService);

        var act = () => service.ValidatePassengerForFlight("XYZ", 100);

        act.Should().ThrowExactly<FlightNotFoundException>();
    }
    
    [Fact]
    public void ValidatePassengerForFlight_ExistingFlightNonExistingPassenger_ThrowsException()
    {
        var flight = new Flight { Id = 1, Number = "XYZ" };
        var person = new Person { Id = 10, Name = "John Doe" };
        var booking = new Booking { Id = 100, Number = "BOOK", Flight = flight, Passengers = new[] { person } };
        var flightNumber = flight.Number;
        
        var flightRepository = new Mock<IRepository<Flight>>();
        flightRepository
            .Setup(_ => _.GetAll())
            .Returns(new List<Flight> { flight });
        
        var bookingRepository = new Mock<IRepository<Booking>>();
        bookingRepository
            .Setup(_ => _.GetAll())
            .Returns(new List<Booking> { booking });
        
        var personService = Mock.Of<IPersonService>();

        var service = new FlightService(bookingRepository.Object, flightRepository.Object, personService);

        const int wrongPersonId = 100;
        var act = () => service.ValidatePassengerForFlight(flightNumber, wrongPersonId);

        act.Should().ThrowExactly<PassengerNotFoundForFlightException>();
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void CreateBookingOnFlight_NullOrEmptyFlightNumber_ThrowsException(string flightNumber)
    {
        var bookingRepository = Mock.Of<IRepository<Booking>>();
        var flightRepository = Mock.Of<IRepository<Flight>>();
        var personService = Mock.Of<IPersonService>();

        var service = new FlightService(bookingRepository, flightRepository, personService);

        var act = () => service.CreateBookingOnFlight(flightNumber, 10, DateTime.UtcNow);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void CreateBookingOnFlight_NonExistingFlight_ThrowsException()
    {
        var bookingRepository = Mock.Of<IRepository<Booking>>();
        var personService = Mock.Of<IPersonService>();
        var flightRepository = new Mock<IRepository<Flight>>();

        flightRepository
            .Setup(_ => _.GetAll())
            .Returns(Enumerable.Empty<Flight>());

        var service = new FlightService(bookingRepository, flightRepository.Object, personService);

        var act = () => service.CreateBookingOnFlight("XYZ", 100, DateTime.UtcNow);

        act.Should().ThrowExactly<FlightNotFoundException>();
    }
    
    [Fact]
    public void CreateBookingOnFlight_ExistingFlightWithExistingPassenger_ThrowsException()
    {
        var flight = new Flight { Id = 1, Number = "XYZ" };
        var person = new Person { Id = 10, Name = "John Doe" };
        var booking = new Booking { Id = 100, Number = "BOOK", Flight = flight, Passengers = new[] { person } };
        var flightNumber = flight.Number;
        
        var flightRepository = new Mock<IRepository<Flight>>();
        flightRepository
            .Setup(_ => _.GetAll())
            .Returns(new List<Flight> { flight });
        
        var bookingRepository = new Mock<IRepository<Booking>>();
        bookingRepository
            .Setup(_ => _.GetAll())
            .Returns(new List<Booking> { booking });
        
        var personService = Mock.Of<IPersonService>();

        var service = new FlightService(bookingRepository.Object, flightRepository.Object, personService);

        var personId = person.Id;
        var act = () => service.CreateBookingOnFlight(flightNumber, personId, DateTime.UtcNow);

        act.Should().ThrowExactly<PassengerAlreadyExistsOnFlightException>();
    }
    
    [Fact]
    public void CreateBookingOnFlight_ExistingFlightNonExistingPassenger_CreatesBooking()
    {
        var flight = new Flight { Id = 1, Number = "XYZ" };
        var person = new Person { Id = 10, Name = "John Doe" };
        var flightNumber = flight.Number;
        var personId = person.Id;
        
        var flightRepository = new Mock<IRepository<Flight>>();
        flightRepository
            .Setup(_ => _.GetAll())
            .Returns(new List<Flight> { flight });
        
        var bookingRepository = new Mock<IRepository<Booking>>();
        bookingRepository
            .Setup(_ => _.GetAll())
            .Returns(Enumerable.Empty<Booking>());
        
        var personService = new Mock<IPersonService>();
        personService
            .Setup(_ => _.Get(personId))
            .Returns(person);

        var service = new FlightService(bookingRepository.Object, flightRepository.Object, personService.Object);
        
        var result = service.CreateBookingOnFlight(flightNumber, personId, DateTime.UtcNow);

        using (new AssertionScope())
        {
            result.Should().NotBeNullOrEmpty();
            bookingRepository.Verify(_ => _.Save(It.IsAny<Booking>()), Times.Once);
        }
    }
}