using IdentityRegistration.Domain.Enum.Otp;
using MediatR;

namespace IdentityRegistration.Application.Features.Otps.Commands.Resend;

public sealed record ResendOtpCommand(Guid UserId,
                                      NotificationAddressType NotificationType) : IRequest<Unit>;