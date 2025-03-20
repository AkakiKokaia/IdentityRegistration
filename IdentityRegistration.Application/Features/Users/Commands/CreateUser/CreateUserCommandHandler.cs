using IdentityRegistration.Domain.Entities;
using IdentityRegistration.Domain.Enum.Otp;
using IdentityRegistration.Domain.Interfaces;
using MediatR;

namespace IdentityRegistration.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly IOtpRepository _otpRepository;
    public CreateUserCommandHandler(IUserRepository userRepository, IOtpRepository otpRepository)
    {
        _userRepository = userRepository;
        _otpRepository = otpRepository;
    }

    public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        bool existingUser = await _userRepository.ExistsAsync(x => x.IcNumber == request.IcNumber);

        if (existingUser)
            throw new Exception("There is account already registered with the IC number, Please login to continue");

        var user = new User(request.CustomerName, request.IcNumber, request.MobileNumber, request.Email);
        await _userRepository.AddAsync(user);

        var otps = new List<Otp>
        {
            new(user.Id, NotificationAddressType.Mobile),
            new(user.Id, NotificationAddressType.Email)
        };

        await _otpRepository.AddRangeAsync(otps);

        return Unit.Value;
    }
}