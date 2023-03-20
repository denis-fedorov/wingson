using System;
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

    public FlightService(IRepository<Booking> bookingRepository, IRepository<Flight> flightRepository)
    {
        _bookingRepository = bookingRepository;
        _flightRepository = flightRepository;
    }

    public ReadOnlyCollection<Person> GetPassengers(string flightNumber)
    {
        var flight = GetByNumber(flightNumber);
        if (flight is null)
        {
            throw new FlightNotFoundException(flightNumber);
        }

        var flightId = flight.Id;
        var passengers = _bookingRepository
            .GetAll()
            .Where(booking => booking.Flight?.Id == flightId)
            .SelectMany(booking => booking.Passengers)
            .ToList()
            .AsReadOnly();

        return passengers;
    }

    private Flight GetByNumber(string flightNumber)
    {
        var flight = _flightRepository
            .GetAll()
            .SingleOrDefault(f => string.Equals(f.Number, flightNumber, StringComparison.InvariantCultureIgnoreCase));

        return flight;
    }
}