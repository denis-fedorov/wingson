using System;
using System.Runtime.Serialization;

namespace WingsOn.Domain.Exceptions;

[Serializable]
public class PassengerAlreadyExistsOnFlightException : Exception
{
    public PassengerAlreadyExistsOnFlightException(string flightNumber, int personId)
        : base($"The passenger with id '{personId}' already exists for the flight number '{flightNumber}'")
    {
        // do nothing
    }

    private PassengerAlreadyExistsOnFlightException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        // do nothing
    }
}