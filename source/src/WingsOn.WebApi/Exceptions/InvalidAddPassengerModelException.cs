using System.Runtime.Serialization;

namespace WingsOn.WebApi.Exceptions;

[Serializable]
public class InvalidAddPassengerModelException : Exception
{
    public InvalidAddPassengerModelException(string message)
        : base(message)
    {
        // do nothing
    }
    
    private InvalidAddPassengerModelException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        // do nothing
    }
}