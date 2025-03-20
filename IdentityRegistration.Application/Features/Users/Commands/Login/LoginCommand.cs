using MediatR;

namespace IdentityRegistration.Application.Features.Users.Commands.Login;

public sealed record LoginCommand(string IcNumber) : IRequest<Unit>;