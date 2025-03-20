using IdentityRegistration.Domain.Interfaces;
using MediatR;

namespace IdentityRegistration.Application.Features.Users.Commands.ActivateBiometricLogin;

public class ActivateBiometricLoginCommandHandler : IRequestHandler<ActivateBiometricLoginCommand, Unit>
{
    private readonly IUserRepository _userRepository;

    public ActivateBiometricLoginCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(ActivateBiometricLoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId) ?? throw new Exception("User not found");

        if (request.IsActivated)
        {
            user.ActivateBiometricLogin();
            await _userRepository.UpdateAsync(user);
        }

        return Unit.Value;
    }
}