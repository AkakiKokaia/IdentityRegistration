using IdentityRegistration.Domain.Entities;

namespace IdentityRegistration.Domain.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByMobileNumberAsync(string mobileNumber);
    Task<bool> IsEmailRegisteredAsync(string email);
    Task<bool> IsMobileRegisteredAsync(string mobileNumber);
}