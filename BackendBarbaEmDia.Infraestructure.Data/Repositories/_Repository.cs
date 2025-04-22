using BackendBarbaEmDia.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BackendBarbaEmDia.Infraestructure.Data.Repositories
{
    public class Repository<T, C> : IRepository<T> where T : class where C : DbContext
    {
        private readonly C _context;

        public Repository(C context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.FindAsync<T>(id);
        }

        public async Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>>? condition = null)
        {
            IQueryable<T> query = _context.Set<T>().AsQueryable();

            if (condition is not null)
                query = query.Where(condition);

            return await query.ToListAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> condition)
        {
            return await _context.Set<T>().AnyAsync(condition);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? condition = null)
        {
            IQueryable<T> query = _context.Set<T>().AsQueryable();
            if (condition is not null)
                query = query.Where(condition);
            return await query.CountAsync();
        }

        public async Task<IEnumerable<T>> GetListWithIncludesAsync(
            Expression<Func<T, bool>>? condition = null,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>().AsQueryable();
            if (condition is not null)
                query = query.Where(condition);

            foreach (var include in includes)
                query = query.Include(include);

            return await query.ToListAsync();
        }

        public async Task<T?> GetFirstWithIncludesAsync(
            Expression<Func<T, bool>> condition,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>().Where(condition).AsQueryable();

            foreach (var include in includes)
                query = query.Include(include);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<T?> GetFirstAsync(Expression<Func<T, bool>> condition)
        {
            return await 
                _context.Set<T>()
                .Where(condition)
                .FirstOrDefaultAsync();
        }
    }
}
