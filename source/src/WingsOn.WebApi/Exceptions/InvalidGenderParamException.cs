using System.Runtime.Serialization;

namespace WingsOn.WebApi.Exceptions;

[Serializable]
public class InvalidGenderParamException : Exception
{
    public InvalidGenderParamException(int value)
        : base($"The value '{value}' is not a valid gender param'")
    {
        // do nothing
    }
    
    private InvalidGenderParamException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        // do nothing
    }
}