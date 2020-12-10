using NetClock.Domain.Common;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Domain.Events.Identity
{
    public class ChangePasswordEvent : DomainEvent
    {
        public ChangePasswordEvent(ApplicationUser applicationUser)
        {
            ApplicationUser = applicationUser;
        }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
