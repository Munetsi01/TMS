using Core;
using Data.Entities;
using Data.Enums;

namespace Data
{
    internal static class EntityInitializer
    {
        public static List<User> InitializeUsers()
        {
            var list = new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Username = "lazzie",
                    Email = "munetsilazzie@gmail.com",
                    Password = Helper.CreateMD5("T5#t0Gg12#"),
                    Role = UserRoleEnum.ADMIN,
                    CreatedAt = DateTime.UtcNow,
                },
                new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Username = "alok",
                    Email = "alok.kumar@smoothstack.com",
                    Password = Helper.CreateMD5("T2as78$kPy"),
                    Role = UserRoleEnum.ADMIN,
                    CreatedAt = DateTime.UtcNow,
                }
            };
            return list;
        }

        public static List<Data.Entities.Task> InitializeTasks(string creatorId, string assigneeId)
        {
            var list = new List<Data.Entities.Task>
            {
                new Data.Entities.Task
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "React DotNet - Coding Assignment - Medlogix",
                    Description = "NOTE: Do not use AI tools to write the code. The code will be screened thoroughly. Use DotNet for Backend and React for Frontend",
                    Status = TaskStatusEnum.IN_PROGRESS,
                    Priority = TaskPriorityEnum.HIGH,
                    AssigneeId = assigneeId,
                    CreatorId = creatorId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                },
            };
            return list;
        }
    }
}
