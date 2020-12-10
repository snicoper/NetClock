using NetClock.Domain.Common;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Domain.Events.Identity
{
    public class ApplicationUserChangePasswordEvent : DomainEvent
    {
        public ApplicationUserChangePasswordEvent(ApplicationUser applicationUser)
        {
            ApplicationUser = applicationUser;
        }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
