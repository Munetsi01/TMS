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
        [AllowAnonymous]//only for authorized
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
    }
}
