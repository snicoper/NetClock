using NetClock.Domain.Common;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Domain.Events.Identity
{
    public class ApplicationUserEvent : DomainEvent
    {
        public ApplicationUserEvent(ApplicationUser applicationUser)
        {
            ApplicationUser = applicationUser;
        }

        public ApplicationUser ApplicationUser { get; }
    }
}
