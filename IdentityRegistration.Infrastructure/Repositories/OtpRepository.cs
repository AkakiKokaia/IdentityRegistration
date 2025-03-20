using IdentityRegistration.Domain.Entities;
using IdentityRegistration.Domain.Enum.Otp;
using IdentityRegistration.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IdentityRegistration.Infrastructure.Repositories;

public class OtpRepository : BaseRepository<Otp>, IOtpRepository
{
    public OtpRepository(DbContext context) : base(context) { }

    public async Task<Otp?> GetValidOtpAsync(Guid userId, NotificationAddressType notificationAddressType)
    {
        return await _dbSet
            .Where(o => o.UserId == userId &&
                        o.AlreadyUsed == false &&
                        o.NotificationAddressType == notificationAddressType &&
                        o.ExpirationDate > DateTimeOffset.UtcNow)
            .FirstOrDefaultAsync();
    }

    public async Task<Otp?> IsOtpValidAsync(Guid userId, string code, NotificationAddressType notificationAddressType)
    {
        return await _dbSet.FirstOrDefaultAsync(o =>
            o.UserId == userId &&
            o.Code == code &&
            o.AlreadyUsed == false &&
            o.NotificationAddressType == notificationAddressType &&
            o.ExpirationDate > DateTimeOffset.UtcNow);
    }
}