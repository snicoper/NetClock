using NetClock.Domain.Common;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Domain.Events.Identity
{
    public class UserChangePasswordEvent : DomainEvent
    {
        public UserChangePasswordEvent(ApplicationUser applicationUser)
        {
            ApplicationUser = applicationUser;
        }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
