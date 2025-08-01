using System.Linq.Expressions;

namespace Core.Abstractions
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddAsync(T t);
        Task<T> GetAsync(Guid id);
        Task<IEnumerable<T>> ListAsync(params Expression<Func<T, object>>[] includes);
        Task RemoveAsync(Guid id);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<T> UpdateAsync(T t);
    }
}
