using System.Collections.Generic;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Interfaces.Identity
{
    public interface IJwtSecurityTokenService
    {
        string CreateToken(ApplicationUser user, IEnumerable<string> roles);
    }
}
