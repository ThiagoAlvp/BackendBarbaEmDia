using System.Linq.Expressions;

namespace BackendBarbaEmDia.Domain.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {        
        Task<T?> GetByIdAsync(int id);        
        Task<List<T>> GetListAsync(Expression<Func<T, bool>>? condition = null);
        Task<T?> GetFirstAsync(Expression<Func<T, bool>> condition);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> condition);
        Task<int> CountAsync(Expression<Func<T, bool>>? condition = null);
        Task<List<T>> GetListWithIncludesAsync(
            Expression<Func<T, bool>>? condition = null,
            params Expression<Func<T, object?>>[] includes);
        Task<T?> GetFirstWithIncludesAsync(
            Expression<Func<T, bool>> condition,
            params Expression<Func<T, object?>>[] includes);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
