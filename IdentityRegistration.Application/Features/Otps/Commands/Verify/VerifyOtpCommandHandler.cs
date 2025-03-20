using IdentityRegistration.Domain.Enum.Otp;
using IdentityRegistration.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IdentityRegistration.Application.Features.Otps.Commands.Verify;

public class VerifyOtpCommandHandler : IRequestHandler<VerifyOtpCommand, Unit>
{
    public IUserRepository _userRepository;
    public IOtpRepository _otpRepository;
    public VerifyOtpCommandHandler(IUserRepository userRepository, IOtpRepository otpRepository)
    {
        _userRepository = userRepository;
        _otpRepository = otpRepository;
    }

    public async Task<Unit> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Query()
                                        .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

        if (user == default)
            throw new Exception("User not found");

        var otp = await _otpRepository.IsOtpValidAsync(request.UserId, request.OtpCode, request.NotificationAddressType);

        if (otp == default)
            throw new Exception("Incorrect OTP, Please enter your OTP again");

        otp.IsUsed();
        await _otpRepository.UpdateAsync(otp);

        switch (request.OtpVerificationType)
        {
            case OtpVerificationType.AccountVerification:
                if (otp.NotificationAddressType == NotificationAddressType.Email)
                    user.VerifyEmail();
                else if (otp.NotificationAddressType == NotificationAddressType.Mobile)
                    user.VerifyMobile();
                break;

            case OtpVerificationType.Login:
                break;

            default:
                throw new Exception("Invalid OTP verification type.");
        }

        await _userRepository.UpdateAsync(user);
        return Unit.Value;
    }
}