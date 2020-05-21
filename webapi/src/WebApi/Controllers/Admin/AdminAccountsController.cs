using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetClock.Application.Admin.AdminAccounts.Commands.ChangePasswordAccount;
using NetClock.Application.Admin.AdminAccounts.Commands.CreateAccount;
using NetClock.Application.Admin.AdminAccounts.Commands.UpdateAccount;
using NetClock.Application.Admin.AdminAccounts.Queries.GetAccounts;
using NetClock.Application.Admin.AdminAccounts.Queries.GetBySlug;
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
        public async Task<ActionResult<ResponseData<GetAccountsDto>>> GetAccounts([FromQuery] RequestData requestData)
        {
            return await Mediator.Send(new GetAccountsQuery(requestData));
        }

        [HttpGet("{slug}")]
        [Authorize(Permissions.AdminAccounts.View)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<GetBySlugDto> GetBySlug(string slug)
        {
            return await Mediator.Send(new GetBySlugQuery(slug));
        }

        [HttpPost]
        [Authorize(Permissions.AdminAccounts.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateUserDto>> CreateAccount(
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
        public async Task<ActionResult<UpdateUserDto>> UpdateAccount(UpdateUserCommand updateUserCommand)
        {
            return await Mediator.Send(updateUserCommand);
        }

        [HttpPost("change-password")]
        [Authorize(Permissions.AdminAccounts.Update)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Unit>> ChangePasswordAccount(ChangePasswordAccountCommand changePasswordAccountCommand)
        {
            return await Mediator.Send(changePasswordAccountCommand);
        }
    }
}
