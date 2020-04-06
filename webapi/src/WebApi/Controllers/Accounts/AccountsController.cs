using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetClock.Application.Accounts.Accounts.Commands.ChangeEmail;
using NetClock.Application.Accounts.Accounts.Commands.ChangeEmailValidate;
using NetClock.Application.Accounts.Accounts.Commands.ChangePassword;
using NetClock.Application.Accounts.Accounts.Commands.Register;
using NetClock.Application.Accounts.Accounts.Commands.RegisterValidate;
using NetClock.Application.Common.Controllers;

namespace NetClock.WebApi.Controllers.Accounts
{
    [Route("api/v{version:apiVersion}/accounts")]
    public class AccountsController : ApiControllerBase
    {
        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RegisterDto>> Register(RegisterCommand registerCommand)
        {
            var result = await Mediator.Send(registerCommand);

            return string.IsNullOrEmpty(result.Id) ? BadRequest(result) : ResponseCreate(result);
        }

        [AllowAnonymous]
        [HttpPost("register/validate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterValidateCode(RegisterValidateCommand registerValidateCommand)
        {
            return Ok(await Mediator.Send(registerValidateCommand));
        }

        [HttpPost("change-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ChangePassword(ChangePasswordCommand changePasswordCommand)
        {
            return Ok(await Mediator.Send(changePasswordCommand));
        }

        [HttpPost("change-email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ChangeEmail(ChangeEmailCommand changeEmailCommand)
        {
            return Ok(await Mediator.Send(changeEmailCommand));
        }

        [HttpPost("change-email/validate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ChangeEmailValidate(ChangeEmailValidateCommand changeEmailValidateCommand)
        {
            return Ok(await Mediator.Send(changeEmailValidateCommand));
        }
    }
}
