using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace NetClock.Application.Common.Controllers
{
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ApiController]
    [Authorize]
    public abstract class ApiControllerBase : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

        private IAuthorizationService AuthorizationService =>
            HttpContext.RequestServices.GetRequiredService<IAuthorizationService>();

        protected ActionResult<T> ResponseCreate<T>(T response)
            where T : class
        {
            HttpContext.Response.StatusCode = StatusCodes.Status201Created;

            return new JsonResult(response);
        }

        protected ActionResult ResponseCreate()
        {
            HttpContext.Response.StatusCode = StatusCodes.Status201Created;

            return new JsonResult(StatusCodes.Status201Created);
        }
    }
}
