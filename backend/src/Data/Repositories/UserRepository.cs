using Core.Abstractions;
using Data.Entities;

namespace Data.Repositories
{
    public class UserRepository : Repository<User>, IRepository<User>
    {
        private ApplicationDbContext applicationDbContext;

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            applicationDbContext = context;
        }
    }
}
