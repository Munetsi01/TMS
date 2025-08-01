using Api.Extensions;
using Api.Models;
using Api.Models.Task;
using Api.Models.User;
using AutoMapper;
using Core.Abstractions;
using Data.Entities;
using Data.Enums;
using Data.Repositories;
using FluentValidation;
using System.Net;

namespace Api.Handlers
{
    public class ListTasksHandler : IHandler<ListTasksRequest, ListTasksResponse>
    {
        private readonly IRepository<Data.Entities.Task> _taskRepository;

        private readonly IValidator<ListTasksRequest> _validator;

        private readonly ILogger<Data.Entities.Task> _logger;

        private readonly IMapper _mapper;

        public ListTasksHandler(IRepository<Data.Entities.Task> taskRepository,
           IValidator<ListTasksRequest> validator,
           ILogger<Data.Entities.Task> logger,
           IMapper mapper)
        {
            _taskRepository = taskRepository;
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ListTasksResponse> Handle(ListTasksRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errorResponse = new ErrorResponse
                {
                    TimeStamp = DateTime.UtcNow.ToTimeStampFormatString(),
                    ApplicationName = $"{nameof(ListTasksHandler)}",
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = string.Join(',', validationResult.Errors.Select(e => e.ErrorMessage))
                };

                _logger.LogError(errorResponse.ToJsonString());

                throw new BusinessException(errorResponse);
            }

            IEnumerable<Data.Entities.Task> results = null;

            if(request.Status == -1 && request.AssigneeId == string.Empty)
            {
               results = await _taskRepository.ListAsync();
            }

            else if (request.Status == -1 && request.AssigneeId != string.Empty)
            {
               results = _taskRepository.Find(x => x.AssigneeId == request.AssigneeId);
            }

            else if (request.Status != -1 && request.AssigneeId == string.Empty)
            {
                results = _taskRepository.Find(x => x.Status == (TaskStatusEnum)request.Status);
            }
            else
            {
               results =  _taskRepository.Find(x=>x.Status == (TaskStatusEnum)request.Status && x.AssigneeId == request.AssigneeId);

            }

            var users = _mapper.Map<IEnumerable<TaskDTO>>(results);


            return new ListTasksResponse
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
