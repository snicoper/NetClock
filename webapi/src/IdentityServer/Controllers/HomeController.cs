using Microsoft.AspNetCore.Authorization;
using NetClock.Application.Common.Controllers;

namespace NetClock.IdentityServer.Controllers
{
    public class HomeController : AppControllerBase
    {
        [AllowAnonymous]
        public string Index()
        {
            return "Hello world";
        }
    }
}
