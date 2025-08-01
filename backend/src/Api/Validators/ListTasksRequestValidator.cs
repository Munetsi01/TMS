using Api.Models.Task;
using Api.Models.User;
using FluentValidation;

namespace Api.Validators
{
    public class ListTasksRequestValidator : AbstractValidator<ListTasksRequest>
    {
        public ListTasksRequestValidator()
        {
                
        }
    }
}
