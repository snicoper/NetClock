using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetClock.Application.Cqrs.Admin.AdminAccounts.Queries.GetBySlug;
using NetClock.Application.Cqrs.Admin.AdminAccounts.Queries.GetUsers;
using NetClock.Application.Models.Http;

namespace NetClock.WebApi.Controllers.Admin
{
    [Route("api/v{version:apiVersion}/admin/accounts")]
    public class AdminAccountsController : ApiControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
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
    }
}
