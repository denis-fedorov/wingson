using System;
using System.Collections.ObjectModel;
using WingsOn.Domain.Entities;

namespace WingsOn.Domain.Interfaces;

public interface IFlightService
{
    public ReadOnlyCollection<Person> GetPassengers(string flightNumber, GenderType? genderType);

    public void ValidatePassengerForFlight(string flightNumber, int personId);

    public string CreateBookingOnFlight(string flightNumber, int personId, DateTime bookingTime);
}