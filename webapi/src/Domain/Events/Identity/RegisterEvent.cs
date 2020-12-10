using NetClock.Domain.Common;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Domain.Events.Identity
{
    public class RegisterEvent : DomainEvent
    {
        public RegisterEvent(ApplicationUser applicationUser)
        {
            ApplicationUser = applicationUser;
        }

        public ApplicationUser ApplicationUser { get; }
    }
}
