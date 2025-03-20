using System.Linq.Expressions;

namespace IdentityRegistration.Domain.Interfaces;

public interface IBaseRepository<T> where T : class
{
    IQueryable<T> Query(Expression<Func<T, bool>>? expression = default);
    Task<T> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    Task UpdateAsync(T entity);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
}
