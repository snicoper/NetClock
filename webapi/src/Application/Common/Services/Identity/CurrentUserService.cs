using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using NetClock.Application.Common.Interfaces.Identity;

namespace NetClock.Application.Common.Services.Identity
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            User = httpContextAccessor.HttpContext?.User;
            Id = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            UserName = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
            Email = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
            Roles = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role)?.Split(",");
        }

        public ClaimsPrincipal User { get; }

        public string Id { get; }

        public string UserName { get; }

        public string Email { get; }

        public ICollection<string> Roles { get; }
    }
}
