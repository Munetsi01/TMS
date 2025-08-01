using Api.Models.User;
using Core.Abstractions;
using Data.Entities;
using FluentValidation;

namespace Api.Validators
{
    public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
    {
        private IRepository<User> _userRepository;

        public RegisterUserRequestValidator(IRepository<User> userRepository)
        {
            _userRepository = userRepository;

            RuleFor(m => m.Username)
               .Cascade(CascadeMode.Stop)
               .NotNull()
               .WithMessage($"{nameof(RegisterUserRequest.Username)} is required.")
               .NotEmpty()
               .WithMessage($"{nameof(RegisterUserRequest.Username)} is required.")
               .Must(username =>
                {
                 var result = _userRepository.Find(x=>x.Username == username).Any();
                 return !result;
               })
              .WithMessage($"{nameof(RegisterUserRequest.Username)} already taken."); ;

            RuleFor(m => m.Email)
               .Cascade(CascadeMode.Stop)
               .NotNull()
               .WithMessage($"{nameof(RegisterUserRequest.Email)} is required.")
               .NotEmpty()
               .WithMessage($"{nameof(RegisterUserRequest.Email)} is required.")
               .Must(Utilities.BeValidEmail)
               .WithMessage($"{nameof(RegisterUserRequest.Email)} is invalid."); ;

            RuleFor(m => m.Password)
              .Cascade(CascadeMode.Stop)
              .NotNull()
              .WithMessage($"{nameof(RegisterUserRequest.Password)} is required.")
              .NotEmpty()
              .WithMessage($"{nameof(RegisterUserRequest.Password)} is required.")
              .Length(8, 32)
              .WithMessage($"{nameof(RegisterUserRequest.Password)} must have at least 8 characters and at most 32 characters.")
              .Must(Utilities.ContainAtLeastOneDigit)
              .WithMessage($"{nameof(RegisterUserRequest.Password)} must contain at least one digit.")
              .Must(Utilities.ContainAtLeastOneUppercase)
              .WithMessage($"{nameof(RegisterUserRequest.Password)} must contain at least one uppercase character.")
              .Must(Utilities.ContainAtLeastOneLowercase)
              .WithMessage($"{nameof(RegisterUserRequest.Password)} must contain at least one lowercase character.")
              .Must(Utilities.ContainAtLeastOneSpecialCharacter)
              .WithMessage($"{nameof(RegisterUserRequest.Password)} must contain at least one special character.");
        }
    }
}
