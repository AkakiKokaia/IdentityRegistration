using IdentityRegistration.Domain.Entities;
using IdentityRegistration.Domain.Enum.Otp;

namespace IdentityRegistration.Domain.Interfaces;

public interface IOtpRepository : IBaseRepository<Otp>
{
    Task<Otp?> GetValidOtpAsync(Guid userId, NotificationAddressType notificationAddressType);
    Task<Otp?> IsOtpValidAsync(Guid userId, string code, NotificationAddressType notificationAddressType);
}