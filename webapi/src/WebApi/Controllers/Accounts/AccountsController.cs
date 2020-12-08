using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetClock.Application.Accounts.Accounts.Commands.ChangeEmail;
using NetClock.Application.Accounts.Accounts.Commands.ChangeEmailValidate;
using NetClock.Application.Accounts.Accounts.Commands.ChangePassword;
using NetClock.Application.Accounts.Accounts.Commands.Register;
using NetClock.Application.Accounts.Accounts.Commands.RegisterValidate;
using NetClock.Application.Common.Authorization.Constants;

namespace NetClock.WebApi.Controllers.Accounts
{
    [Route("api/v{version:apiVersion}/accounts")]
    public class AccountsController : ApiControllerBase
    {
        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> Register(RegisterCommand registerCommand)
        {
            var result = await Mediator.Send(registerCommand);

            return ResponseCreate(result);
        }

        [AllowAnonymous]
        [HttpPost("register/validate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Unit>> RegisterValidateCode(RegisterValidateCommand registerValidateCommand)
        {
            return await Mediator.Send(registerValidateCommand);
        }

        [HttpPost("change-password")]
        [Authorize(Permissions.Accounts.Update)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Unit>> ChangePassword(ChangePasswordCommand changePasswordCommand)
        {
            return await Mediator.Send(changePasswordCommand);
        }

        [HttpPost("change-email")]
        [Authorize(Permissions.Accounts.Update)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Unit>> ChangeEmail(ChangeEmailCommand changeEmailCommand)
        {
            return await Mediator.Send(changeEmailCommand);
        }

        [HttpPost("change-email/validate")]
        [Authorize(Permissions.Accounts.Update)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Unit>> ChangeEmailValidate(ChangeEmailValidateCommand changeEmailValidateCommand)
        {
            return await Mediator.Send(changeEmailValidateCommand);
        }
    }
}
