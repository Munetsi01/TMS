using Api.Models.User;
using Core.Abstractions;

namespace Api.Models.Task
{
    public class ListTasksResponse : IEnvelopeResponse<IEnumerable<TaskDTO>>, IResponse
    {
    }
}
