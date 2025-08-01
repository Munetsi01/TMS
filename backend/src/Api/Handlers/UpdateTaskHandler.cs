using Api.Extensions;
using Api.Models;
using Api.Models.Task;
using Core.Abstractions;
using Data.Enums;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

namespace Api.Handlers
{
    public class UpdateTaskHandler : IHandler<UpdateTaskRequest, UpdateTaskResponse>
    {
        private readonly IRepository<Data.Entities.Task> _taskRepository;

        private readonly IValidator<UpdateTaskRequest> _validator;

        private readonly ILogger<Data.Entities.Task> _logger;

        public UpdateTaskHandler(IRepository<Data.Entities.Task> taskRepository,
            IValidator<UpdateTaskRequest> validator,
            ILogger<Data.Entities.Task> logger)
        {
            _taskRepository = taskRepository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<UpdateTaskResponse> Handle(UpdateTaskRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errorResponse = new ErrorResponse
                {
                    TimeStamp = DateTime.UtcNow.ToTimeStampFormatString(),
                    ApplicationName = $"{nameof(UpdateTaskHandler)}",
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = string.Join(',', validationResult.Errors.Select(e => e.ErrorMessage))
                };

                _logger.LogError(errorResponse.ToJsonString());

                throw new BusinessException(errorResponse);
            }

            var task = await _taskRepository.GetAsync(request.Id);

            if (task == null)
            {
                var errorResponse = new ErrorResponse
                {
                    TimeStamp = DateTime.UtcNow.ToTimeStampFormatString(),
                    ApplicationName = $"{nameof(UpdateTaskHandler)}",
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Resource not found on the server."
                };

                _logger.LogError(errorResponse.ToJsonString());

                throw new BusinessException(errorResponse);
            }

            task.Title = request.Title;
            task.Description = request.Description;
            task.Status = (TaskStatusEnum)request.Status;
            task.Priority = (TaskPriorityEnum)request.Priority;
            task.AssigneeId = request.AssigneeId;
            task.UpdatedAt = DateTime.UtcNow;

            await _taskRepository.UpdateAsync(task);

            return new UpdateTaskResponse
            {
                TaskId = task.Id,
            };
        }
    }
}
