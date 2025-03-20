using FluentValidation;
using FluentValidation.Results;

namespace IdentityRegistration.Application.Configuration.Exceptions;

public abstract class CustomAbstractValidator<T> : AbstractValidator<T>
{
    public override ValidationResult Validate(ValidationContext<T> context)
    {
        try
        {
            ValidationResult result = base.Validate(context);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            return result;
        }
        catch (FluentValidation.ValidationException ex)
        {
            throw new ValidationException(ex.Errors);
        }
    }
}