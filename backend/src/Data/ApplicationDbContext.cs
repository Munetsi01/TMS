using Core;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(AppConfig.GetConnectionString());
            }

            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                        .HasIndex(u => u.Username)
                        .IsUnique();

            base.OnModelCreating(modelBuilder);
            Seed(modelBuilder);
        }

        private static void Seed(ModelBuilder modelBuilder)
        {
            var users = EntityInitializer.InitializeUsers();
            modelBuilder.Entity<User>().HasData(users);

            var taskCreator = users.FirstOrDefault(x => x.Username == "alok");
            var taskAssignee = users.FirstOrDefault(x => x.Username == "lazzie");
            var tasks = EntityInitializer.InitializeTasks(taskCreator.Id, taskAssignee.Id);
            modelBuilder.Entity<Data.Entities.Task>().HasData(tasks);

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Data.Entities.Task> Tasks { get; set; }

     }
}
