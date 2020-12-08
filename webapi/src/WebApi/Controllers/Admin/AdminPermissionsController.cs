using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NetClock.WebApi.Controllers.Admin
{
    [Authorize(Roles = "Superuser,Staff")]
    [Route("api/v{version:apiVersion}/admin/permissions")]
    public class AdminPermissionsController : ApiControllerBase
    {
    }
}
