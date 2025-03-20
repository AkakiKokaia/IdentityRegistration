using FluentValidation;

namespace IdentityRegistration.Application.Features.Users.Commands.CreatePin;

public class CreatePinCommandValidator : AbstractValidator<CreatePinCommand>
{
    public CreatePinCommandValidator()
    {
        RuleFor(x => x.PinCode)
            .NotEmpty().WithMessage("PIN is required.")
            .Matches(@"^\d{6}$").WithMessage("PIN must be exactly 6 digits.");

        RuleFor(x => x.RepeatPinCode)
            .NotEmpty().WithMessage("Repeat PIN is required.")
            .Equal(x => x.PinCode).WithMessage("PIN and Repeat PIN must match.");
    }
}