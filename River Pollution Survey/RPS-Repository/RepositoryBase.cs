using Microsoft.EntityFrameworkCore;
using River_Pollution_Survey.Models.DBModels;
using River_Pollution_Survey.RPS_Contracts;
using System.Linq.Expressions;

namespace River_Pollution_Survey.RPS_Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T>  where T : class
    {
        protected readonly RiverDBContext _dbContext;

        public RepositoryBase(RiverDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IAsyncEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return _dbContext.Set<T>().Where(expression).AsAsyncEnumerable();
        }

        public async Task<IAsyncEnumerable<T>> GetAllAsync()
        {
            return _dbContext.Set<T>().AsAsyncEnumerable();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T?> FindByIdAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbContext.Set<T>().SingleOrDefaultAsync(expression);
        }

        public async Task<IAsyncEnumerable<T>> GetByForeignKeyIdAsync(Expression<Func<T, bool>> foreignKeyExpression)
        {
            return _dbContext.Set<T>().Where(foreignKeyExpression).AsAsyncEnumerable();
        }

        
    }
}
