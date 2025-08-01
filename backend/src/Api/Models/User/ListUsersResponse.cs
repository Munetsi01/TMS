using Core.Abstractions;

namespace Api.Models.User
{
    public class ListUsersResponse : IEnvelopeResponse<IEnumerable<UserDTO>>, IResponse
    {
    }
}
