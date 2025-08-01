using Api.Extensions;
using Api.Models;
using Api.Models.User;
using Core;
using Core.Abstractions;
using Data.Entities;
using Data.Repositories;
using FluentValidation;
using System.Net;

namespace Api.Handlers
{
    public class LoginUserHandler : IHandler<LoginUserRequest, LoginUserResponse>
    {
       private readonly IRepository<User> _userRepository;

        private readonly IValidator<LoginUserRequest> _validator;

        private readonly ILogger<User> _logger;

        private readonly IJwtProvider<User> _provider;

        public LoginUserHandler(IRepository<User> userRepository,
            IValidator<LoginUserRequest> validator,
            ILogger<User> logger,
            IJwtProvider<User> provider)
        {
            _userRepository = userRepository;
            _validator = validator;
            _logger = logger;
            _provider = provider;
        }

        public async Task<LoginUserResponse> Handle(LoginUserRequest request)
        {

            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errorResponse = new ErrorResponse
                {
                    TimeStamp = DateTime.UtcNow.ToTimeStampFormatString(),
                    ApplicationName = $"{nameof(LoginUserHandler)}",
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = string.Join(',', validationResult.Errors.Select(e => e.ErrorMessage))
                };

                _logger.LogError(errorResponse.ToJsonString());

                throw new BusinessException(errorResponse);
            }
            
            var user =  _userRepository.Find(x => x.Username == request.Username && x.Password == Helper.CreateMD5(request.Password))
                                       .FirstOrDefault();

            if (user == null)
            {
                var errorResponse = new ErrorResponse
                {
                    TimeStamp = DateTime.UtcNow.ToTimeStampFormatString(),
                    ApplicationName = $"{nameof(LoginUserHandler)}",
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    Message = "invalid username/password combination."
                };

                _logger.LogError(errorResponse.ToJsonString());

                throw new BusinessException(errorResponse);
            }

            var result = await _provider.GenereteAsync(user);

            var response = new LoginUserResponse
            {
                Token = result.Item1,
                TokenExpiryDate = result.Item2
            };
            return response;
        }
    }
}
