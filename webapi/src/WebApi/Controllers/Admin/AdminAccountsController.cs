using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetClock.Application.Admin.AdminAccounts.Commands.CreateUser;
using NetClock.Application.Admin.AdminAccounts.Queries.GetBySlug;
using NetClock.Application.Admin.AdminAccounts.Queries.GetUsers;
using NetClock.Application.Common.Controllers;
using NetClock.Application.Common.Http;
using NetClock.Domain.Entities.Identity;

namespace NetClock.WebApi.Controllers.Admin
{
    [Route("api/v{version:apiVersion}/admin/accounts")]
    public class AdminAccountsController : ApiControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponseData<AdminUserListViewModel>>> GetUsers(
            [FromQuery] RequestData requestData)
        {
            return await Mediator.Send(new GetUsersQuery(requestData));
        }

        [HttpGet("{slug}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<AdminUserDetailsViewModel> GetBySlug(string slug)
        {
            return await Mediator.Send(new GetBySlugQuery(slug));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateUserCommand createUserCommand, ApiVersion version)
        {
            var applicationUser = await Mediator.Send(createUserCommand);
            var routeValues = new { slug = applicationUser.Slug, version = version.ToString() };

            return CreatedAtAction(nameof(GetBySlug), routeValues, applicationUser);
        }
    }
}
