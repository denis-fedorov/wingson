using WingsOn.WebApi.Exceptions;
using WingsOn.WebApi.Models;

namespace WingsOn.WebApi.Validators;

public static class AddPassengerModelValidator
{
    public static void Validate(AddPassengerModel model)
    {
        if (model is null)
        {
            throw new InvalidAddPassengerModelException("The model is empty");
        }

        var personId = model.PersonId;
        if (personId <= 0)
        {
            throw new InvalidAddPassengerModelException($"The person id '{personId}' in the model is invalid");
        }
    }
}