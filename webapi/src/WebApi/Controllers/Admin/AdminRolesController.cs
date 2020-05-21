using Microsoft.AspNetCore.Components;
using NetClock.Application.Common.Api;

namespace NetClock.WebApi.Controllers.Admin
{
    [Route("api/v{version:apiVersion}/admin/roles")]
    public class AdminRolesController : ApiControllerBase
    {
    }
}
