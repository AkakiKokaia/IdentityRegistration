using IdentityRegistration.Domain.Entities;
using IdentityRegistration.Domain.Enum.Otp;
using IdentityRegistration.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IdentityRegistration.Application.Features.Otps.Commands.Resend;

public class ResendOtpCommandHandler : IRequestHandler<ResendOtpCommand, Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly IOtpRepository _otpRepository;

    public ResendOtpCommandHandler(IUserRepository userRepository, IOtpRepository otpRepository)
    {
        _userRepository = userRepository;
        _otpRepository = otpRepository;
    }

    public async Task<Unit> Handle(ResendOtpCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Query().FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

        if (user == default)
            throw new Exception("User not found");

        var lastOtp = await _otpRepository.GetValidOtpAsync(request.UserId, request.NotificationType);

        if (lastOtp != null)
        {
            var timeSinceLastOtp = DateTimeOffset.UtcNow - lastOtp.CreatedAt;
            if (timeSinceLastOtp.TotalMinutes < 2)
                throw new Exception("OTP can only be requested once every 2 minutes.");

            lastOtp.IsUsed();
            await _otpRepository.UpdateAsync(lastOtp);
        }

        var newOtp = new Otp(user.Id, request.NotificationType);
        await _otpRepository.AddAsync(newOtp);

        return Unit.Value;
    }
}