using FluentValidation;

namespace IdentityRegistration.Application.Features.Otps.Commands.Verify;

public class VerifyOtpCommandValidator : AbstractValidator<VerifyOtpCommand>
{
    public VerifyOtpCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.OtpCode)
            .NotEmpty().WithMessage("OTP Code is required.")
            .Matches(@"^\d{4}$").WithMessage("OTP Code must be exactly 4 digits.");
    }
}