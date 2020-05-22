using Microsoft.AspNetCore.Identity;

namespace NetClock.Domain.Entities.Identity
{
    public class ApplicationUserClaim : IdentityUserClaim<string>
    {
        public virtual ApplicationUser User { get; set; }
    }
}
