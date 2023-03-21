using WingsOn.Domain.Entities;
using WingsOn.WebApi.Exceptions;

namespace WingsOn.WebApi.Validators;

public static class GenderValidator
{
    public static GenderType? TryParse(int? value)
    {
        if (value is null)
        {
            return null;
        }
        
        var genderType = (GenderType)value;
        if (Enum.IsDefined(typeof(GenderType), genderType))
        {
            return genderType;
        }

        throw new InvalidGenderParamException(value.Value);
    }
}