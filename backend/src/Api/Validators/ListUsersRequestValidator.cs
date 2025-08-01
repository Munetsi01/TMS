using Api.Models.User;
using FluentValidation;

namespace Api.Validators
{
    public class ListUsersRequestValidator : AbstractValidator<ListUsersRequest>
    {
        public ListUsersRequestValidator()
        {
                
        }
    }
}
