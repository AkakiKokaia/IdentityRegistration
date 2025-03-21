using FluentValidation;

namespace IdentityRegistration.Application.Features.Users.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.IcNumber)
            .NotEmpty().WithMessage("IC Number is required.");
    }
}