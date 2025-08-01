using Api.Models.User;
using Api.Models;
using Core.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Models.Task;

namespace Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class TasksController : Controller
    {
        [Authorize]
        [Route("tasks")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ListTasksResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
        public async Task<ActionResult> ListTasksAsync([FromServices] IHandler<ListTasksRequest, ListTasksResponse> handler, [FromQuery] int status=-1, [FromQuery] string assignee = "")
        {
            try
            {
                var request = new ListTasksRequest
                {
                    Status = status,
                    AssigneeId = assignee
                };
                var response = await handler.Handle(request).ConfigureAwait(false);
                return Ok(response);
            }
            catch (BusinessException ex)
            {
                return StatusCode(ex.ErrorResponse!.StatusCode, ex.ErrorResponse);
            }
        }

        [Authorize]
        [Route("tasks")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateTaskResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
        public async Task<ActionResult> CreateTaskAsync([FromServices] IHandler<CreateTaskRequest, CreateTaskResponse> handler, [FromBody] CreateTaskRequest request)
        {
            try
            {
                request.CreatorId = User?.Claims?.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;

                var response = await handler.Handle(request).ConfigureAwait(false);
                return Ok(response);
            }
            catch (BusinessException ex)
            {
                return StatusCode(ex.ErrorResponse!.StatusCode, ex.ErrorResponse);
            }
        }

        [Authorize]
        [Route("tasks/{id}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeleteTaskResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
        public async Task<ActionResult> DeleteTaskAsync([FromServices] IHandler<DeleteTaskRequest, DeleteTaskResponse> handler, [FromRoute] string id)
        {
            try
            {
                var request = new DeleteTaskRequest
                {
                    Id = id
                };

                var response = await handler.Handle(request).ConfigureAwait(false);
                return Ok(response);
            }
            catch (BusinessException ex)
            {
                return StatusCode(ex.ErrorResponse!.StatusCode, ex.ErrorResponse);
            }
        }

        [Authorize]
        [Route("tasks/{id}")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateTaskResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
        public async Task<ActionResult> UpdateTaskAsync([FromServices] IHandler<UpdateTaskRequest, UpdateTaskResponse> handler, [FromRoute]string id, [FromBody] UpdateTaskRequest request)
        {
            try
            {
                request.Id = id;
                var response = await handler.Handle(request).ConfigureAwait(false);
                return Ok(response);
            }
            catch (BusinessException ex)
            {
                return StatusCode(ex.ErrorResponse!.StatusCode, ex.ErrorResponse);
            }
        }
    }
}
