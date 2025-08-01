using Api.Extensions;
using Api.Models;
using Api.Models.User;
using AutoMapper;
using Core;
using Core.Abstractions;
using Data.Entities;
using FluentValidation;
using System.Net;

namespace Api.Handlers
{
    public class ListUsersHandler : IHandler<ListUsersRequest, ListUsersResponse>
    {
        private readonly IRepository<User> _userRepository;

        private readonly IValidator<ListUsersRequest> _validator;

        private readonly ILogger<User> _logger;

        private readonly IMapper _mapper;


        public ListUsersHandler(IRepository<User> userRepository,
            IValidator<ListUsersRequest> validator,
            ILogger<User> logger,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ListUsersResponse> Handle(ListUsersRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errorResponse = new ErrorResponse
                {
                    TimeStamp = DateTime.UtcNow.ToTimeStampFormatString(),
                    ApplicationName = $"{nameof(ListUsersHandler)}",
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = string.Join(',', validationResult.Errors.Select(e => e.ErrorMessage))
                };

                _logger.LogError(errorResponse.ToJsonString());

                throw new BusinessException(errorResponse);
            }

            var results = await _userRepository.ListAsync();

            var users = _mapper.Map<IEnumerable<UserDTO>>(results);


            return new ListUsersResponse
            {
                Data = users,
                Summary = new Summary
                {
                    TotalCount = users.Count()
                }
            };
        }
    }
}
