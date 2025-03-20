using IdentityRegistration.Domain.Interfaces;
using MediatR;

namespace IdentityRegistration.Application.Features.Users.Commands.CreatePin;

public class CreatePinCommandHandler : IRequestHandler<CreatePinCommand, Unit>
{
    private readonly IUserRepository _userRepository;

    public CreatePinCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(CreatePinCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId) ?? throw new Exception("User not found");

        if (!user.IsEmailVerified || !user.IsMobileVerified)
            throw new Exception("Both email and mobile must be verified before setting a PIN.");

        if (string.IsNullOrEmpty(request.PinCode) || request.PinCode.Length != 6)
            throw new Exception("PIN must be a 6-digit number.");

        if (request.PinCode != request.RepeatPinCode)
            throw new Exception("Unmached PIN, Please enter your PIN again.");

        user.SetPin(request.PinCode);
        await _userRepository.UpdateAsync(user);

        return Unit.Value;
    }
}