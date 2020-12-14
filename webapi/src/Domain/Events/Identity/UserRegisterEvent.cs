using NetClock.Domain.Common;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Domain.Events.Identity
{
    public class UserRegisterEvent : DomainEvent
    {
        public UserRegisterEvent(ApplicationUser applicationUser)
        {
            ApplicationUser = applicationUser;
        }

        public ApplicationUser ApplicationUser { get; }
    }
}
