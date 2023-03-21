using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace WingsOn.Domain.Exceptions;

[Serializable]
[ExcludeFromCodeCoverage(Justification = "A domain exception")]
public class PersonNotFoundException : Exception
{
    public PersonNotFoundException(int id)
        : base($"A person with id '{id}' was not found")
    {
        // do nothing
    }

    private PersonNotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        // do nothing
    }
}