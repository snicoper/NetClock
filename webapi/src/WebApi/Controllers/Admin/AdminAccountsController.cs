using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetClock.Application.Admin.AdminAccounts.Commands.ChangePasswordUser;
using NetClock.Application.Admin.AdminAccounts.Commands.CreateUser;
using NetClock.Application.Admin.AdminAccounts.Commands.UpdateUser;
using NetClock.Application.Admin.AdminAccounts.Queries.GetBySlug;
using NetClock.Application.Admin.AdminAccounts.Queries.GetUsers;
using NetClock.Application.Common.Api;
using NetClock.Application.Common.Authorization;
using NetClock.Application.Common.Http;

namespace NetClock.WebApi.Controllers.Admin
{
    [Authorize(Roles = "Superuser,Staff")]
    [Route("api/v{version:apiVersion}/admin/accounts")]
    public class AdminAccountsController : ApiControllerBase
    {
        [HttpGet]
        [Authorize(Permissions.AdminAccounts.View)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponseData<AdminUserListDto>>> GetUsers([FromQuery] RequestData requestData)
        {
            return await Mediator.Send(new GetUsersQuery(requestData));
        }

        [HttpGet("{slug}")]
        [Authorize(Permissions.AdminAccounts.View)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<AdminUserDetailsDto> GetBySlug(string slug)
        {
            return await Mediator.Send(new GetBySlugQuery(slug));
        }

        [HttpPost]
        [Authorize(Permissions.AdminAccounts.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateUserDto>> CreateUser(
            CreateUserCommand createUserCommand,
            ApiVersion version)
        {
            var applicationUser = await Mediator.Send(createUserCommand);
            var routeValues = new { slug = applicationUser.Slug, version = version.ToString() };

            return CreatedAtAction(nameof(GetBySlug), routeValues, applicationUser);
        }

        [HttpPut("update")]
        [Authorize(Permissions.AdminAccounts.Update)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UpdateUserDto>> UpdateUser(UpdateUserCommand updateUserCommand)
        {
            return Ok(await Mediator.Send(updateUserCommand));
        }

        [HttpPost("change-password")]
        [Authorize(Permissions.AdminAccounts.Update)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ChangePasswordUser(ChangePasswordUserCommand changePasswordUserCommand)
        {
            return Ok(await Mediator.Send(changePasswordUserCommand));
        }
    }
}
