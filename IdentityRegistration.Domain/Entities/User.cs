using IdentityRegistration.Shared;

namespace IdentityRegistration.Domain.Entities;

public class User : Entity
{

    public User(string customerName,
                string icNumber,
                string mobileNumber,
                string email)
    {
        CustomerName = customerName;
        IcNumber = icNumber;
        MobileNumber = mobileNumber;
        Email = email;
    }

    public string CustomerName { get; private set; }
    public string IcNumber { get; private set; }
    public string MobileNumber { get; private set; }
    public string Email { get; private set; }

    public bool IsEmailVerified { get; private set; } = false;
    public DateTimeOffset? EmailVerifiedAt { get; private set; }

    public bool IsMobileVerified { get; private set; } = false;
    public DateTimeOffset? MobileVerifiedAt { get; private set; }

    public string? HashedPin { get; private set; }

    public bool HasAgreedToTerms { get; private set; } = false;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public bool IsBiometricLoginActivated { get; private set; } = false;

    public void AgreeToTerms()
    {
        HasAgreedToTerms = true;
    }

    public void ActivateBiometricLogin()
    {
        IsBiometricLoginActivated = true;
    }

    public void VerifyEmail()
    {
        IsEmailVerified = true;
        EmailVerifiedAt = DateTimeOffset.UtcNow;
    }

    public void VerifyMobile()
    {
        IsMobileVerified = true;
        MobileVerifiedAt = DateTimeOffset.UtcNow;
    }

    public void SetPin(string pin)
    {
        if (string.IsNullOrEmpty(pin) || pin.Length != 6)
            throw new ArgumentException("PIN must be a 6-digit number");

        HashedPin = BCrypt.Net.BCrypt.HashPassword(pin);
    }
    public bool VerifyPin(string pin)
    {
        return BCrypt.Net.BCrypt.Verify(pin, HashedPin);
    }
}
