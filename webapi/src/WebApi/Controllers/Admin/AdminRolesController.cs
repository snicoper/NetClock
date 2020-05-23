using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetClock.Application.Admin.AdminRoles.Queries.GetRol;
using NetClock.Application.Admin.AdminRoles.Queries.GetRoles;
using NetClock.Application.Common.Api;
using NetClock.Application.Common.Authorization.Constants;
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

        [HttpGet("{id}")]
        [Authorize(Permissions.AdminRoles.View)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetRolDto>> GetRol(string id)
        {
            return await Mediator.Send(new GetRolQuery(id));
        }
    }
}
