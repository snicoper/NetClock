using Microsoft.AspNetCore.Authorization;

namespace NetClock.Application.Common.Authorization.Requirements
{
    internal class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }

        public string Permission { get; }
    }
}
