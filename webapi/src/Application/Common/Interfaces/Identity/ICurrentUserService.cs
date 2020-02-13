using System.Collections.Generic;

namespace NetClock.Application.Common.Interfaces.Identity
{
    public interface ICurrentUserService
    {
        public string UserId { get; }

        public string Name { get; }

        public string Email { get; }

        public ICollection<string> Roles { get; }
    }
}
