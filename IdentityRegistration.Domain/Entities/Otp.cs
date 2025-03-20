using IdentityRegistration.Domain.Enum.Otp;
using IdentityRegistration.Shared;

namespace IdentityRegistration.Domain.Entities;

public class Otp : Entity
{
    public Otp(
        Guid userId,
        NotificationAddressType notificationAddressType)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        NotificationAddressType = notificationAddressType;
        // Change for GenerateOtp for future
        Code = "1234";
        CreatedAt = DateTimeOffset.UtcNow;
        ExpirationDate = DateTimeOffset.UtcNow.AddHours(1);
    }

    public Guid UserId { get; private set; }
    public string Code { get; private set; }
    public DateTimeOffset ExpirationDate { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public NotificationAddressType NotificationAddressType { get; private set; }
    public bool AlreadyUsed { get; private set; } = false;

    public void IsUsed()
    {
        AlreadyUsed = true;
        LastChangeDate();
    }

    public void LastChangeDate()
    {
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void GenerateOtp()
    {
        const string digits = "0123456789";
        var random = new Random();
        var otp = new char[digits.Length];
        for (int i = 0; i < otp.Length; i++)
            otp[i] = digits[random.Next(digits.Length)];

        Code = new string(otp);
    }
}
