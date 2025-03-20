namespace IdentityRegistration.Application.Configuration.Exceptions;

public class ApiException : Exception
{
    public ApiException() : base() { }

    public ApiException(string? message = null) : base(message)
    {
    }
}
