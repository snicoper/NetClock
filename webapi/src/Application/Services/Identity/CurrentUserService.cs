using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using NetClock.Application.Interfaces.Identity;

namespace NetClock.Application.Services.Identity
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            User = httpContextAccessor.HttpContext?.User;
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            Name = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
            Email = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
            Roles = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role)?.Split(",");
        }

        public ClaimsPrincipal User { get; }

        public string UserId { get; }

        public string Name { get; }

        public string Email { get; }

        public ICollection<string> Roles { get; }
    }
}
