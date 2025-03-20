using IdentityRegistration.Domain.Enum.Otp;
using MediatR;

namespace IdentityRegistration.Application.Features.Otps.Commands.Verify;

public sealed record VerifyOtpCommand(Guid UserId, 
                                      string OtpCode, 
                                      NotificationAddressType NotificationAddressType,
                                      OtpVerificationType OtpVerificationType) : IRequest<Unit>;
