using Core.Abstractions;

namespace Data.Repositories
{
    public class TaskRepository : Repository<Data.Entities.Task>, IRepository<Data.Entities.Task>
    {
        private ApplicationDbContext applicationDbContext;

        public TaskRepository(ApplicationDbContext context) : base(context)
        {
            applicationDbContext = context;
        }
    }
}
