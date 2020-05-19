using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetClock.Application.Common.Api;
using NetClock.Application.Localization.Commands.SetCulture;

namespace NetClock.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/localization")]
    public class LocalizationController : ApiControllerBase
    {
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> SetCulture(SetCultureCommand cultureCommand)
        {
            await Mediator.Send(cultureCommand);

            return NoContent();
        }
    }
}
