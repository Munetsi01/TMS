using Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected ApplicationDbContext Context { get; set; }

        public Repository(ApplicationDbContext context)
        {
            Context = context;
        }

        public virtual async Task<T> AddAsync(T t)
        {
            await Context.Set<T>().AddAsync(t);
            await Context.SaveChangesAsync();
            return t;
        }

        public virtual async Task<T> GetAsync(Guid id)
        {
            var result = await Context.Set<T>().FindAsync(id);
            if (result != null)
                return result;
            return null;
        }

        public virtual async Task<IEnumerable<T>> ListAsync(params Expression<Func<T, object>>[] includes)
        {
            var query = Context.Set<T>().AsQueryable();
            if (includes != null)
            {
                query = includes.Aggregate(query,
                          (current, include) => current.Include(include));
            }
            return await query.ToListAsync();
        }

        public virtual async Task RemoveAsync(Guid id)
        {
            var deletedEntity = await Context.Set<T>().FindAsync(id);
            if (deletedEntity != null)
            {
                Context.Set<T>().Remove(deletedEntity);
                await Context.SaveChangesAsync();
            }
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = Context.Set<T>().Where(predicate);
            if (includes != null)
            {
                query = includes.Aggregate(query,
                          (current, include) => current.Include(include));
            }
            return query;
        }

        public async Task<T> UpdateAsync(T t)
        {
            Context.Set<T>().Update(t);
            await Context.SaveChangesAsync();
            return t;
        }
    }
}
