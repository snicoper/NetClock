using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetClock.Application.Common.Controllers;

namespace NetClock.IdentityServer.Controllers
{
    [Route("auth")]
    public class AuthController : AppControllerBase
    {
        [AllowAnonymous]
        [Route("login")]
        public ActionResult Login()
        {
            return View();
        }
    }
}
