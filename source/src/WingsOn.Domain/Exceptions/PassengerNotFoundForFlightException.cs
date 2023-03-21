using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace WingsOn.Domain.Exceptions;

[Serializable]
[ExcludeFromCodeCoverage(Justification = "A domain exception")]
public class PassengerNotFoundForFlightException : Exception
{
    public PassengerNotFoundForFlightException(string flightNumber, int personId)
        : base($"The passenger with id '{personId}' was not found for the flight number '{flightNumber}'")
    {
        // do nothing
    }
    
    private PassengerNotFoundForFlightException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        // do nothing
    }
}