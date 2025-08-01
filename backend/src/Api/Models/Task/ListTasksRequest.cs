namespace Api.Models.Task
{
    public class ListTasksRequest
    {
        public string AssigneeId { get; set; }

        public int Status { get; set; }
    }
}
