using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NetClock.WebApi.Validators;

namespace NetClock.WebApi.Controllers
{
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ApiController]
    [Authorize]
    public abstract class ApiControllerBase : ControllerBase
    {
        private IMediator _mediator;
        private IValidateParams _validateParams;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

        protected IValidateParams ValidateParams =>
            _validateParams ??= HttpContext.RequestServices.GetRequiredService<IValidateParams>();

        [NonAction]
        protected ActionResult<T> ResponseCreate<T>(T response)
            where T : class
        {
            HttpContext.Response.StatusCode = StatusCodes.Status201Created;

            return new JsonResult(response);
        }

        [NonAction]
        protected ActionResult ResponseCreate()
        {
            HttpContext.Response.StatusCode = StatusCodes.Status201Created;

            return new JsonResult(StatusCodes.Status201Created);
        }
    }
}
