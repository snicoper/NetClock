using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetClock.Application.Admin.AdminRoles.Queries;
using NetClock.Application.Common.Api;
using NetClock.Application.Common.Authorization;
using NetClock.Application.Common.Http;

namespace NetClock.WebApi.Controllers.Admin
{
    [Authorize(Roles = "Superuser,Staff")]
    [Route("api/v{version:apiVersion}/admin/roles")]
    public class AdminRolesController : ApiControllerBase
    {
        [HttpGet]
        [Authorize(Permissions.AdminRoles.View)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponseData<GetRolesDto>>> GetRoles([FromQuery] RequestData requestData)
        {
            return await Mediator.Send(new GetRolesQuery(requestData));
        }
    }
}
