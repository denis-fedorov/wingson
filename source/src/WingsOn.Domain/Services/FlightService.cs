using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WingsOn.Domain.Entities;
using WingsOn.Domain.Exceptions;
using WingsOn.Domain.Interfaces;

namespace WingsOn.Domain.Services;

public sealed class FlightService : IFlightService
{
    private readonly IRepository<Booking> _bookingRepository;
    private readonly IRepository<Flight> _flightRepository;
    private readonly IPersonService _personService;

    public FlightService(
        IRepository<Booking> bookingRepository, 
        IRepository<Flight> flightRepository,
        IPersonService personService)
    {
        _bookingRepository = bookingRepository;
        _flightRepository = flightRepository;
        _personService = personService;
    }

    public ReadOnlyCollection<Person> GetPassengers(string flightNumber, GenderType? genderType)
    {
        ArgumentException.ThrowIfNullOrEmpty(flightNumber);
        
        var flight = GetByNumber(flightNumber);
        if (flight is null)
        {
            throw new FlightNotFoundException(flightNumber);
        }

        var flightId = flight.Id;
        var passengers = _bookingRepository
            .GetAll()
            .Where(booking => booking.Flight?.Id == flightId)
            .SelectMany(booking => booking.Passengers);

        if (genderType is not null)
        {
            passengers = passengers.Where(p => p.Gender == genderType);
        }

        return passengers
            .ToList()
            .AsReadOnly();
    }

    public void ValidatePassengerForFlight(string flightNumber, int personId)
    {
        ArgumentException.ThrowIfNullOrEmpty(flightNumber);
        
        var flight = GetByNumber(flightNumber);
        if (flight is null)
        {
            throw new FlightNotFoundException(flightNumber);
        }
        
        var passengerExists = PassengerExistsOnFlight(flight.Id, personId);
        if (!passengerExists)
        {
            throw new PassengerNotFoundForFlightException(flightNumber, personId);
        }
    }

    public string CreateBookingOnFlight(string flightNumber, int personId, DateTime bookingTime)
    {
        ArgumentException.ThrowIfNullOrEmpty(flightNumber);
        
        var flight = GetByNumber(flightNumber);
        if (flight is null)
        {
            throw new FlightNotFoundException(flightNumber);
        }
        
        var passengerExists = PassengerExistsOnFlight(flight.Id, personId);
        if (passengerExists)
        {
            throw new PassengerAlreadyExistsOnFlightException(flightNumber, personId);
        }

        var person = _personService.Get(personId);

        var booking = new Booking
        {
            Id = GenerateFreeBookingId(),
            Number = Booking.GetNewNumber,
            DateBooking = bookingTime,
            Customer = person,
            Flight = flight,
            Passengers = new List<Person> { person }
        };
        
        _bookingRepository.Save(booking);

        return booking.Number;
    }

    private bool PassengerExistsOnFlight(int flightId, int personId)
    {
        var passengerExists = _bookingRepository
            .GetAll()
            .Where(booking => booking.Flight?.Id == flightId)
            .SelectMany(booking => booking.Passengers)
            .Any(p => p.Id == personId);

        return passengerExists;
    }
    
    private Flight GetByNumber(string flightNumber)
    {
        var flight = _flightRepository
            .GetAll()
            .SingleOrDefault(f => string.Equals(f.Number, flightNumber, StringComparison.InvariantCultureIgnoreCase));

        return flight;
    }

    private int GenerateFreeBookingId()
    {
        // a small hack to get the unused id (should be done on the DAL layer in the real world)
        var lastBookingId = _bookingRepository
            .GetAll()
            .Max(b => b.Id);

        return lastBookingId + 1;
    }
}