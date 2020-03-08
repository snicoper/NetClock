using System.Threading.Tasks;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetClock.Application.Accounts.Auth.Commands.Login;
using NetClock.Application.Common.Controllers;
using NetClock.Application.Common.Exceptions;
using NetClock.Domain.Entities.Identity;
using NetClock.IdentityServer.Common;

namespace NetClock.IdentityServer.Controllers
{
    [Route("auth")]
    public class AuthController : AppControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IIdentityServerInteractionService _identityServerInteractionService;

        public AuthController(
            SignInManager<ApplicationUser> signInManager,
            IIdentityServerInteractionService identityServerInteractionService)
        {
            _signInManager = signInManager;
            _identityServerInteractionService = identityServerInteractionService;
        }

        [AllowAnonymous]
        [HttpGet("login")]
        public ActionResult<LoginCommand> Login()
        {
            var model = new LoginCommand();

            return View(model);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<CurrentUserViewModel>> Login(LoginCommand loginCommand)
        {
            try
            {
                await Mediator.Send(loginCommand);
            }
            catch (ValidationException validationException)
            {
                ModelStateHelper.AddErrorsInModelState(ModelState, validationException);

                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [HttpGet("logout")]
        public ActionResult Logout()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
            var logoutRequest = await _identityServerInteractionService.GetLogoutContextAsync(logoutId);

            return string.IsNullOrEmpty(logoutRequest.PostLogoutRedirectUri)
                ? RedirectToAction("Index", "Home")
                : RedirectToAction(logoutRequest.PostLogoutRedirectUri);
        }
    }
}
