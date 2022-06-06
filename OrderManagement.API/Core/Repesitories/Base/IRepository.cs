using System.Linq.Expressions;

namespace OrderManagement.API.Core.Repesitories.Base
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Edit(T entity);
        void Remove(T entity);
        ValueTask<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    }
}
