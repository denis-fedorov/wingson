using System;
using System.Runtime.Serialization;

namespace WingsOn.Domain.Exceptions;

[Serializable]
public class FlightNotFoundException : Exception
{
    public FlightNotFoundException(string flightNumber)
        : base($"A flight with number '{flightNumber}' was not found")
    {
        // do nothing
    }

    private FlightNotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        // do nothing
    }
}