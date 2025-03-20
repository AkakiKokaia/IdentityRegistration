using IdentityRegistration.Domain.Entities;
using IdentityRegistration.Domain.Enum.Otp;
using IdentityRegistration.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IdentityRegistration.Application.Features.Users.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly IOtpRepository _otpRepository;

    public LoginCommandHandler(IUserRepository userRepository, IOtpRepository otpRepository)
    {
        _userRepository = userRepository;
        _otpRepository = otpRepository;
    }

    public async Task<Unit> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Query().FirstOrDefaultAsync(x => x.IcNumber == request.IcNumber);

        if (user == default)
            throw new Exception("User with this Ic number not found");

        var otps = new List<Otp>
        {
            new(user.Id, NotificationAddressType.Mobile),
            new(user.Id, NotificationAddressType.Email)
        };

        await _otpRepository.AddRangeAsync(otps);

        return Unit.Value;
    }
}