using Api.Models;
using Api.Models.User;
using Core.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class UsersController : Controller
    {
        [AllowAnonymous]
        [Route("auth/login")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginUserResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<ActionResult> LoginUserAsync([FromServices] IHandler<LoginUserRequest, LoginUserResponse> handler, [FromBody] LoginUserRequest request)
        {
            try
            {
                var response = await handler.Handle(request).ConfigureAwait(false);
                return Ok(response);
            }
            catch (BusinessException ex)
            {
                return StatusCode(ex.ErrorResponse!.StatusCode, ex.ErrorResponse);
            }
        }

        [AllowAnonymous]
        [Route("auth/register")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegisterUserResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<ActionResult> RegisterUserAsync([FromServices] IHandler<RegisterUserRequest, RegisterUserResponse> handler, [FromBody] RegisterUserRequest request)
        {
            try
            {
                var response = await handler.Handle(request).ConfigureAwait(false);
                return Ok(response);
            }
            catch (BusinessException ex)
            {
                return StatusCode(ex.ErrorResponse!.StatusCode, ex.ErrorResponse);
            }
        }

        [Authorize]
        [Route("users")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ListUsersResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
        public async Task<ActionResult> ListUsersAsync([FromServices] IHandler<ListUsersRequest, ListUsersResponse> handler)
        {
            try
            {
                var request = new ListUsersRequest();
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
