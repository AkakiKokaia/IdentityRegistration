using MediatR;

namespace IdentityRegistration.Application.Features.Users.Commands.CreatePin;

public sealed record CreatePinCommand(Guid UserId,
                                      string PinCode, 
                                      string RepeatPinCode) : IRequest<Unit>;