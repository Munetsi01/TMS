using Api.Extensions;
using Api.Models;
using Api.Models.Task;
using Core.Abstractions;
using FluentValidation;
using System.Net;

namespace Api.Handlers
{
    public class DeleteTaskHandler : IHandler<DeleteTaskRequest, DeleteTaskResponse>
    {
        private readonly IRepository<Data.Entities.Task> _taskRepository;

        private readonly IValidator<DeleteTaskRequest> _validator;

        private readonly ILogger<Data.Entities.Task> _logger;

        public DeleteTaskHandler(IRepository<Data.Entities.Task> taskRepository,
            IValidator<DeleteTaskRequest> validator,
            ILogger<Data.Entities.Task> logger)
        {
            _taskRepository = taskRepository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<DeleteTaskResponse> Handle(DeleteTaskRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errorResponse = new ErrorResponse
                {
                    TimeStamp = DateTime.UtcNow.ToTimeStampFormatString(),
                    ApplicationName = $"{nameof(DeleteTaskHandler)}",
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = string.Join(',', validationResult.Errors.Select(e => e.ErrorMessage))
                };

                _logger.LogError(errorResponse.ToJsonString());

                throw new BusinessException(errorResponse);
            }

            var task = await _taskRepository.GetAsync(request.Id);

            if(task == null)
            {
                var errorResponse = new ErrorResponse
                {
                    TimeStamp = DateTime.UtcNow.ToTimeStampFormatString(),
                    ApplicationName = $"{nameof(DeleteTaskHandler)}",
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Resource not found on the server."
                };

                _logger.LogError(errorResponse.ToJsonString());

                throw new BusinessException(errorResponse);
            }

           await _taskRepository.RemoveAsync(task.Id);

            return new DeleteTaskResponse();
        }
    }
}
