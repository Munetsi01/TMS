using Api.Models.Task;
using Core.Abstractions;
using Data.Entities;
using FluentValidation;

namespace Api.Validators
{
    public class CreateTaskRequestValidator : AbstractValidator<CreateTaskRequest>
    {
        private IRepository<User> _userRepository;

        public CreateTaskRequestValidator(IRepository<User> userRepository)
        {
            _userRepository = userRepository;

            RuleFor(m => m.Title)
             .Cascade(CascadeMode.Stop)
             .NotNull()
             .WithMessage($"{nameof(CreateTaskRequest.Title)} is required.")
             .NotEmpty()
             .WithMessage($"{nameof(CreateTaskRequest.Title)} is required.");

            RuleFor(m => m.Description)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage($"{nameof(CreateTaskRequest.Description)} is required.")
            .NotEmpty()
            .WithMessage($"{nameof(CreateTaskRequest.Description)} is required.");

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
            .WithMessage($"{nameof(CreateTaskRequest.Status)} is invalid.");

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
           .WithMessage($"{nameof(CreateTaskRequest.Priority)} is invalid.");

            RuleFor(m => m.AssigneeId)
              .Cascade(CascadeMode.Stop)
              .NotNull()
              .WithMessage($"{nameof(CreateTaskRequest.AssigneeId)} is required.")
              .NotEmpty()
              .WithMessage($"{nameof(CreateTaskRequest.AssigneeId)} is required.")
              .Must(assigneeId =>
              {
                  var result = _userRepository.Find(x => x.Id == assigneeId).Any();
                  return result;
              })
             .WithMessage($"{nameof(CreateTaskRequest.AssigneeId)} is invalid.");

            RuleFor(m => m.CreatorId)
             .Cascade(CascadeMode.Stop)
             .NotNull()
             .WithMessage($"{nameof(CreateTaskRequest.CreatorId)} is required.")
             .NotEmpty()
             .WithMessage($"{nameof(CreateTaskRequest.CreatorId)} is required.")
             .Must(creatorId =>
             {
                 var result = _userRepository.Find(x => x.Id == creatorId).Any();
                 return result;
             })
            .WithMessage($"{nameof(CreateTaskRequest.CreatorId)} is invalid.");
        }
    }
}
