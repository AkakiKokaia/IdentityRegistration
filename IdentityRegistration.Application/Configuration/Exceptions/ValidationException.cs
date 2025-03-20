using FluentValidation.Results;

namespace IdentityRegistration.Application.Configuration.Exceptions;

public class ValidationException : Exception
{
    public ValidationException() : base("Validation failure have occurred.")
    {
    }

    public List<ValidationError> ValidationErrors { get; set; } = new();

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        foreach (ValidationFailure failure in failures)
        {
            ValidationErrors.Add(new ValidationError
            {
                PropertyName = failure.PropertyName,
                ErrorMessage = failure.ErrorMessage
            });
        }
    }

    public ValidationException(List<ValidationError>? failures)
        : this()
    {
        if (failures != null)
        {
            ValidationErrors = failures;
        }
    }
}

public class ValidationError
{
    public string PropertyName { get; set; }
    public string ErrorMessage { get; set; }
}