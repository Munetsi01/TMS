using Api.Models.Task;
using FluentValidation;

namespace Api.Validators
{
    public class DeleteTaskRequestValidator : AbstractValidator<DeleteTaskRequest>
    {
        public DeleteTaskRequestValidator()
        {
            RuleFor(m => m.Id)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage($"{nameof(DeleteTaskRequest.Id)} is required.")
            .NotEmpty()
            .WithMessage($"{nameof(DeleteTaskRequest.Id)} is required.");
        }
    }
}
