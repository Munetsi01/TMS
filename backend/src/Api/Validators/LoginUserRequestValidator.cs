using Api.Models.User;
using FluentValidation;

namespace Api.Validators
{
    public class LoginUserRequestValidator : AbstractValidator<LoginUserRequest>
    {
        public LoginUserRequestValidator()
        {
            RuleFor(m => m.Username)
               .Cascade(CascadeMode.Stop)
               .NotNull()
               .WithMessage($"{nameof(LoginUserRequest.Username)} is required.")
               .NotEmpty()
               .WithMessage($"{nameof(LoginUserRequest.Username)} is required.");

            RuleFor(m => m.Password)
              .Cascade(CascadeMode.Stop)
              .NotNull()
              .WithMessage($"{nameof(LoginUserRequest.Password)} is required.")
              .NotEmpty()
              .WithMessage($"{nameof(LoginUserRequest.Password)} is required.");
        }
    }
}
