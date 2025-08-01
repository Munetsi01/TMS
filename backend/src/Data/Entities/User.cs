using Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public UserRoleEnum Role { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
