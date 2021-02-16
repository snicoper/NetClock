using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NetClock.Application.Common.Authorization.Constants;
using NetClock.Application.Common.Authorization.Requirements;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Common.Authorization.Handlers
{
    internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public PermissionAuthorizationHandler(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            if (!context.User.IsAuthenticated())
            {
                return;
            }

            var user = await _userManager.GetUserAsync(context.User);
            var userRoleNames = await _userManager.GetRolesAsync(user);
            var userRoles = _roleManager.Roles.Where(x => userRoleNames.Contains(x.Name)).ToList();

            foreach (var role in userRoles)
            {
                var roleClaims = await _roleManager.GetClaimsAsync(role);
                var permissions = roleClaims
                    .Where(x => x.Type == CustomClaimTypes.Permission
                                && x.Value == requirement.Permission
                                && x.Issuer == "LOCAL AUTHORITY")
                    .Select(x => x.Value);

                if (!permissions.Any())
                {
                    continue;
                }

                context.Succeed(requirement);
                return;
            }
        }
    }
}
