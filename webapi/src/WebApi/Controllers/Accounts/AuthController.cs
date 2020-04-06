using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetClock.Application.Accounts.Auth.Commands.Login;
using NetClock.Application.Accounts.Auth.Commands.Logout;
using NetClock.Application.Accounts.Auth.Commands.RecoveryPassword;
using NetClock.Application.Accounts.Auth.Commands.RecoveryPasswordValidate;
using NetClock.Application.Common.Controllers;

namespace NetClock.WebApi.Controllers.Accounts
{
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthController : ApiControllerBase
    {
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CurrentUserDto>> Login(LoginCommand loginCommand)
        {
            return Ok(await Mediator.Send(loginCommand));
        }

        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Logout()
        {
            await Mediator.Send(new LogoutCommand());

            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("recovery-password")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RecoveryPassword(RecoveryPasswordCommand recoveryPasswordCommand)
        {
            await Mediator.Send(recoveryPasswordCommand);

            return StatusCode(StatusCodes.Status201Created);
        }

        [AllowAnonymous]
        [HttpPost("recovery-password/validate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RecoveryPasswordValidate(
            RecoveryPasswordValidateCommand recoveryPasswordValidateCommand)
        {
            return Ok(await Mediator.Send(recoveryPasswordValidateCommand));
        }
    }
}
