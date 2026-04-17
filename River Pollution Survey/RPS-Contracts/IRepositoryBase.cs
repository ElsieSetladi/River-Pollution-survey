using System.Linq.Expressions;

namespace River_Pollution_Survey.RPS_Contracts
{
    public interface IRepositoryBase<T>
    {
        Task<IAsyncEnumerable<T>> GetAllAsync();
        Task<IAsyncEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression);
        Task<T?> FindByIdAsync(Expression<Func<T, bool>> expression);
        Task<IAsyncEnumerable<T>> GetByForeignKeyIdAsync(Expression<Func<T, bool>> foreignKeyExpression);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task SaveAsync();
        
    }
}
