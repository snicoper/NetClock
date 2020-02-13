using System.Collections.Generic;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Common.Interfaces.Identity
{
    public interface IJwtSecurityTokenService
    {
        string CreateToken(ApplicationUser user, IEnumerable<string> roles);
    }
}
