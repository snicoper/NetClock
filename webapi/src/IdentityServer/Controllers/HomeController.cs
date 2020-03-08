using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetClock.Application.Common.Controllers;

namespace NetClock.IdentityServer.Controllers
{
    public class HomeController : AppControllerBase
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
    }
}
