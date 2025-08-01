using Api.Extensions;
using Api.Models;
using Api.Models.Task;
using Core.Abstractions;
using Data.Enums;
using FluentValidation;
using System.Net;

namespace Api.Handlers
{
    public class CreateTaskHandler : IHandler<CreateTaskRequest, CreateTaskResponse>
    {
        private readonly IRepository<Data.Entities.Task> _taskRepository;

        private readonly IValidator<CreateTaskRequest> _validator;

        private readonly ILogger<Data.Entities.Task> _logger;

        public CreateTaskHandler(IRepository<Data.Entities.Task> taskRepository,
            IValidator<CreateTaskRequest> validator,
            ILogger<Data.Entities.Task> logger)
        {
            _taskRepository = taskRepository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<CreateTaskResponse> Handle(CreateTaskRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errorResponse = new ErrorResponse
                {
                    TimeStamp = DateTime.UtcNow.ToTimeStampFormatString(),
                    ApplicationName = $"{nameof(CreateTaskHandler)}",
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = string.Join(',', validationResult.Errors.Select(e => e.ErrorMessage))
                };

                _logger.LogError(errorResponse.ToJsonString());

                throw new BusinessException(errorResponse);
            }

            var task = new Data.Entities.Task
            {
                Id = Guid.NewGuid().ToString(),
                Title = request.Title,
                Description = request.Description,
                Status = (TaskStatusEnum)request.Status,
                Priority = (TaskPriorityEnum)request.Priority,
                AssigneeId = request.AssigneeId,
                CreatorId = request.CreatorId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _taskRepository.AddAsync(task);

            return new CreateTaskResponse
            {
                TaskId = task.Id,
            };
        }
    }
}
