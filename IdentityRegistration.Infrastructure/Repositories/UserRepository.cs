using IdentityRegistration.Domain.Entities;
using IdentityRegistration.Domain.Interfaces;
using IdentityRegistration.Infrastructure.Db;
using Microsoft.EntityFrameworkCore;

namespace IdentityRegistration.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(IdentityRegistrationDbContext context) : base(context) { }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByMobileNumberAsync(string mobileNumber)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.MobileNumber == mobileNumber);
    }

    public async Task<bool> IsEmailRegisteredAsync(string email)
    {
        return await _dbSet.AnyAsync(u => u.Email == email);
    }

    public async Task<bool> IsMobileRegisteredAsync(string mobileNumber)
    {
        return await _dbSet.AnyAsync(u => u.MobileNumber == mobileNumber);
    }
}