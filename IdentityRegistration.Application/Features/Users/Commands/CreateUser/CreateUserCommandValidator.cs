using FluentValidation;

namespace IdentityRegistration.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.CustomerName)
            .NotEmpty().WithMessage("Customer Name is required.")
            .MaximumLength(100).WithMessage("Customer Name must not exceed 100 characters.");

        RuleFor(x => x.IcNumber)
            .NotEmpty().WithMessage("IC Number is required.")
            .Matches(@"^\d{12}$").WithMessage("IC Number must be exactly 12 digits.");

        RuleFor(x => x.MobileNumber)
            .NotEmpty().WithMessage("Mobile Number is required.")
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Mobile Number must be in valid international format.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid Email format.");
    }
}