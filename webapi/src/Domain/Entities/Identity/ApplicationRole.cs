using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace NetClock.Domain.Entities.Identity
{
    public class ApplicationRole : IdentityRole
    {
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }

        public virtual ICollection<ApplicationRoleClaim> RoleClaims { get; set; }
    }
}
