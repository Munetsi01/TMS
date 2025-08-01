using Api.Models.Task;
using Core.Abstractions;
using Data.Entities;
using FluentValidation;

namespace Api.Validators
{
    public class UpdateTaskRequestValidator : AbstractValidator<UpdateTaskRequest>
    {
        private IRepository<User> _userRepository;

        public UpdateTaskRequestValidator(IRepository<User> userRepository)
        {
            _userRepository = userRepository;

            RuleFor(m => m.Id)
             .Cascade(CascadeMode.Stop)
             .NotNull()
             .WithMessage($"{nameof(UpdateTaskRequest.Id)} is required.")
             .NotEmpty()
             .WithMessage($"{nameof(UpdateTaskRequest.Id)} is required.");

            RuleFor(m => m.Title)
             .Cascade(CascadeMode.Stop)
             .NotNull()
             .WithMessage($"{nameof(UpdateTaskRequest.Title)} is required.")
             .NotEmpty()
             .WithMessage($"{nameof(UpdateTaskRequest.Title)} is required.");

            RuleFor(m => m.Description)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage($"{nameof(UpdateTaskRequest.Description)} is required.")
            .NotEmpty()
            .WithMessage($"{nameof(UpdateTaskRequest.Description)} is required.");

            RuleFor(m => m.Status)
            .Cascade(CascadeMode.Stop)
            .Must(status =>
            {
                List<int> validStatuses = new List<int>()
                {
                 0,1,2
                };

                return validStatuses.Contains(status);
            })
            .WithMessage($"{nameof(UpdateTaskRequest.Status)} is invalid.");

            RuleFor(m => m.Priority)
           .Cascade(CascadeMode.Stop)
           .Must(priority =>
           {
               List<int> validPriorities = new List<int>()
               {
                 0,1,2
               };

               return validPriorities.Contains(priority);
           })
           .WithMessage($"{nameof(UpdateTaskRequest.Priority)} is invalid.");

            RuleFor(m => m.AssigneeId)
              .Cascade(CascadeMode.Stop)
              .NotNull()
              .WithMessage($"{nameof(UpdateTaskRequest.AssigneeId)} is required.")
              .NotEmpty()
              .WithMessage($"{nameof(UpdateTaskRequest.AssigneeId)} is required.")
              .Must(assigneeId =>
              {
                  var result = _userRepository.Find(x => x.Id == assigneeId).Any();
                  return result;
              })
             .WithMessage($"{nameof(UpdateTaskRequest.AssigneeId)} is invalid.");
        }
    }
}
