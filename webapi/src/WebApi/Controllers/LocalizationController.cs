using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetClock.Application.Common.Api;
using NetClock.Application.Common.Interfaces.Common;
using NetClock.Application.Localization.Commands.SetCulture;

namespace NetClock.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/localization")]
    public class LocalizationController : ApiControllerBase
    {
        private readonly ICultureService _cultureService;

        public LocalizationController(ICultureService cultureService)
        {
            _cultureService = cultureService;
        }

        [AllowAnonymous]
        [HttpGet("cultures")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<string>> GetCultures()
        {
            return _cultureService.GetCultures().Select(c => c.Name).ToList();
        }

        [AllowAnonymous]
        [HttpGet("cultures/current")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<string> GetCurrentCulture()
        {
            return _cultureService.GetCurrentCulture().Name;
        }

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
