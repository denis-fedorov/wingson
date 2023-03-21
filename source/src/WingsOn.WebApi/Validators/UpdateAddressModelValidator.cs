using WingsOn.WebApi.Exceptions;
using WingsOn.WebApi.Models;

namespace WingsOn.WebApi.Validators;

public static class UpdateAddressModelValidator
{
    public static void Validate(UpdateAddressModel model)
    {
        if (model is null || string.IsNullOrWhiteSpace(model.Address))
        {
            throw new InvalidUpdateAddressModelException();
        }
    }
}