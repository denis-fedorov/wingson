using System.Collections.ObjectModel;
using WingsOn.Domain.Entities;

namespace WingsOn.Domain.Interfaces;

public interface IFlightService
{
    public ReadOnlyCollection<Person> GetPassengers(string flightNumber);
}