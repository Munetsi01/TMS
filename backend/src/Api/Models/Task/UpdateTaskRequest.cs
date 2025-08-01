using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;

namespace Api.Models.Task
{
    public class UpdateTaskRequest
    {
        [JsonIgnore]
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Status { get; set; }

        public int Priority { get; set; }

        public string AssigneeId { get; set; }
    }
}
