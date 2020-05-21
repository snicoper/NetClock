using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetClock.Application.Common.Authorization;
using NetClock.Domain.Entities.Identity;
using NetClock.Domain.Extensions;

namespace NetClock.Infrastructure.Persistence.Seeds
{
    public static class ApplicationDbContextSeed
    {
        private static IServiceProvider _serviceProvider;
        private static ILogger<ApplicationDbContext> _logger;

        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _logger = serviceProvider.GetRequiredService<ILogger<ApplicationDbContext>>();

            var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            await dbContext.Database.EnsureCreatedAsync();

            await SeedRoles();
            await SeedPolicies();
            await SeedUsers();
        }

        private static async Task SeedRoles()
        {
            var roleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var roles = new[]
            {
                AppRoles.Superuser,
                AppRoles.Staff,
                AppRoles.Employee
            };

            foreach (var role in roles)
            {
                if (await roleManager.Roles.AnyAsync(r => r.Name == role))
                {
                    _logger.LogInformation($"Role {role} ya existe en la base de datos");
                    continue;
                }

                await roleManager.CreateAsync(new IdentityRole { Name = role });
                _logger.LogInformation($"Role {role} creado con éxito");
            }
        }

        private static async Task SeedPolicies()
        {
            // FIXME: Los tests no pasan
            var roleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Role Superuser.
            var role = await roleManager.FindByNameAsync(AppRoles.Superuser);
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Accounts.View));
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Accounts.Create));
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Accounts.Update));
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Accounts.Delete));

            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.AdminAccounts.View));
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.AdminAccounts.Create));
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.AdminAccounts.Update));
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.AdminAccounts.Delete));

            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.AdminRoles.View));
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.AdminRoles.Create));
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.AdminRoles.Update));
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.AdminRoles.Delete));

            // Role Staff.
            role = await roleManager.FindByNameAsync(AppRoles.Staff);
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Accounts.View));
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Accounts.Create));
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Accounts.Update));
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Accounts.Delete));

            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.AdminAccounts.View));
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.AdminAccounts.Create));
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.AdminAccounts.Update));
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.AdminAccounts.Delete));

            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.AdminRoles.View));
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.AdminRoles.Create));
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.AdminRoles.Update));
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.AdminRoles.Delete));

            // Role Employee.
            role = await roleManager.FindByNameAsync(AppRoles.Employee);
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Accounts.View));
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Accounts.Update));
        }

        private static async Task SeedUsers()
        {
            var userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var usernames = new[]
            {
                "Admin", "Alice", "Bob", "Joe", "Maria", "Jordi", "Sonia", "Sara", "Perico", "Palote", "Lorena"
            };
            var superusers = new[] { "Admin" };
            var staffs = new[] { "Admin", "Alice" };
            const string password = "123456";

            foreach (var username in usernames)
            {
                if (await userManager.Users.AnyAsync(u => u.UserName == username))
                {
                    _logger.LogInformation($"Usuario {username} ya existe en la base de datos");
                    continue;
                }

                var newUser = new ApplicationUser
                {
                    UserName = username,
                    Slug = username.Slugify(),
                    FirstName = username,
                    LastName = username,
                    Email = $"{username.ToLower()}@example.com",
                    EmailConfirmed = true,
                    Active = true
                };

                var result = await userManager.CreateAsync(newUser, password);
                if (!result.Succeeded)
                {
                    _logger.LogInformation($"ERROR: {result.Errors}");
                    continue;
                }

                _logger.LogInformation($"Usuario {username} creado con éxito");

                // Add role Employee.
                await userManager.AddToRoleAsync(newUser, AppRoles.Employee);
                _logger.LogInformation($"Usuario {newUser.UserName}, role asignado: {AppRoles.Employee}");

                // Add role Staff.
                if (Array.IndexOf(staffs, newUser.UserName) >= 0)
                {
                    await userManager.AddToRoleAsync(newUser, AppRoles.Staff);
                    _logger.LogInformation($"Usuario {newUser.UserName}, role asignado: {AppRoles.Staff}");
                }

                // Add role Superuser.
                if (Array.IndexOf(superusers, newUser.UserName) < 0)
                {
                    continue;
                }

                await userManager.AddToRoleAsync(newUser, AppRoles.Superuser);
                _logger.LogInformation($"Usuario {newUser.UserName}, role asignado: {AppRoles.Superuser}");
            }
        }
    }
}
