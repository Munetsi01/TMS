using Data.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class Task
    {
        [Key]
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public TaskStatusEnum Status { get; set; } 

        public TaskPriorityEnum Priority { get; set; }

        [ForeignKey("Assignee")]
        [Required]
        public string AssigneeId { get; set; }

        [ForeignKey("Creator")]
        [Required]
        public string CreatorId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public virtual User Assignee { get; set; }

        public virtual User Creator { get; set; }
    }
}
