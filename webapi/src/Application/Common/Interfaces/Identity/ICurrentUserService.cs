using System.Collections.Generic;

namespace NetClock.Application.Common.Interfaces.Identity
{
    public interface ICurrentUserService
    {
        public string Id { get; }

        public string UserName { get; }

        public string Email { get; }

        public ICollection<string> Roles { get; }
    }
}
