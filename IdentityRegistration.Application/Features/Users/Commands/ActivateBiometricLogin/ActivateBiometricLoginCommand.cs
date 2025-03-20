using MediatR;

namespace IdentityRegistration.Application.Features.Users.Commands.ActivateBiometricLogin;

public sealed record ActivateBiometricLoginCommand(Guid UserId,
                                                   bool IsActivated) : IRequest<Unit>;
