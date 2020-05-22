using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetClock.Application.Common.Authorization;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Infrastructure.Persistence.Seeds
{
    public static class SeedRoleClaims
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider, ILogger<ApplicationDbContext> logger)
        {
            // FIXME: Los tests no pasan.
            var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var permissions = PermissionsHelper.GetAllPermissionValues().ToArray();

            // Role Superuser, all permissions.
            var role = await roleManager.FindByNameAsync(AppRoles.Superuser);
            var roleClaims = permissions
                .Select(permission => CreateRoleClaim(role, permission))
                .ToList();
            logger.LogInformation($"Se han creado los claims para el role {role.Name}");

            // Role Staff, all permissions.
            role = await roleManager.FindByNameAsync(AppRoles.Staff);
            roleClaims.AddRange(permissions.Select(permission => CreateRoleClaim(role, permission)));
            logger.LogInformation($"Se han creado los claims para el role {role.Name}");

            // Role Employee.
            role = await roleManager.FindByNameAsync(AppRoles.Employee);
            AddClaimsRolesForEmployees(roleClaims, role);
            logger.LogInformation($"Se han creado los claims para el role {role.Name}");

            await dbContext.RoleClaims.AddRangeAsync(roleClaims);
            foreach (var roleClaim in roleClaims)
            {
                dbContext.Entry(roleClaim).State = EntityState.Added;
            }

            await dbContext.SaveChangesAsync();
        }

        private static void AddClaimsRolesForEmployees(
            ICollection<ApplicationRoleClaim> roleClaims,
            ApplicationRole role)
        {
            roleClaims.Add(CreateRoleClaim(role, Permissions.Accounts.View));
            roleClaims.Add(CreateRoleClaim(role, Permissions.Accounts.Update));
        }

        private static ApplicationRoleClaim CreateRoleClaim(ApplicationRole role, string claimValue)
        {
            return new ApplicationRoleClaim
            {
                RoleId = role.Id, ClaimType = CustomClaimTypes.Permission, ClaimValue = claimValue
            };
        }
    }
}
