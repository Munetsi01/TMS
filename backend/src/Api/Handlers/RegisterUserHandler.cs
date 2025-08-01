using Api.Extensions;
using Api.Models;
using Api.Models.User;
using Core;
using Core.Abstractions;
using Data.Entities;
using FluentValidation;
using System.Net;

namespace Api.Handlers
{
    public class RegisterUserHandler : IHandler<RegisterUserRequest, RegisterUserResponse>
    {
        private readonly IRepository<User> _userRepository;

        private readonly IValidator<RegisterUserRequest> _validator;

        private readonly ILogger<User> _logger;

        public RegisterUserHandler(IRepository<User> userRepository,
            IValidator<RegisterUserRequest> validator,
            ILogger<User> logger)
        {
            _userRepository = userRepository;
            _validator = validator;
            _logger = logger;
        }
        public async Task<RegisterUserResponse> Handle(RegisterUserRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errorResponse = new ErrorResponse
                {
                    TimeStamp = DateTime.UtcNow.ToTimeStampFormatString(),
                    ApplicationName = $"{nameof(RegisterUserHandler)}",
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = string.Join(',', validationResult.Errors.Select(e => e.ErrorMessage))
                };

                _logger.LogError(errorResponse.ToJsonString());

                throw new BusinessException(errorResponse);
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = request.Username,
                Email = request.Email,
                Password = Helper.CreateMD5(request.Password),
                Role = Data.Enums.UserRoleEnum.USER,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);

            return new RegisterUserResponse 
            {
                UserId = user.Id,
            };
        }
    }
}
