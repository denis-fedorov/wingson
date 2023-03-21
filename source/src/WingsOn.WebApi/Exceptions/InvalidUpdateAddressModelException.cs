using System.Runtime.Serialization;

namespace WingsOn.WebApi.Exceptions;

[Serializable]
public class InvalidUpdateAddressModelException : Exception
{
    public InvalidUpdateAddressModelException()
        : base("New address value is invalid")
    {
        // do nothing
    }
    
    private InvalidUpdateAddressModelException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        // do nothing
    }
}