using System;
using System.Linq;
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
            await SeedUsers();
            await SeedPolicies();
        }

        private static async Task SeedRoles()
        {
            var roleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var roles = new[] { "Superuser", "Admin", "Staff", "Employee" };

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
            var roleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var role = await roleManager.FindByNameAsync("Superuser");
            var roleClaims = await roleManager.GetClaimsAsync(role);
            if (roleClaims.Any())
            {
                return;
            }

            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Superusers.Full));
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Superusers.View));
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Superusers.Create));
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Superusers.Update));
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Superusers.Delete));

            role = await roleManager.FindByNameAsync("Admin");
            roleClaims = await roleManager.GetClaimsAsync(role);
            if (roleClaims.Any())
            {
                return;
            }

            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Admins.Full));
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Admins.View));
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Admins.Create));
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Admins.Update));
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Admins.Delete));

            role = await roleManager.FindByNameAsync("Staff");
            roleClaims = await roleManager.GetClaimsAsync(role);
            if (roleClaims.Any())
            {
                return;
            }

            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Staffs.Full));
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Staffs.View));
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Staffs.Create));
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Staffs.Update));
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Staffs.Delete));

            role = await roleManager.FindByNameAsync("Employee");
            roleClaims = await roleManager.GetClaimsAsync(role);
            if (roleClaims.Any())
            {
                return;
            }

            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Employees.Full));
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Employees.View));
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Employees.Create));
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Employees.Update));
            await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Employees.Delete));
        }


        private static async Task SeedUsers()
        {
            var userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var usernames = new[]
            {
                "Admin", "Alice", "Bob", "Joe", "Maria", "Jordi", "Sonia", "Sara", "Perico", "Palote", "Lorena"
            };
            var admins = new[] { "Admin" };
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

                // Todo usuario es employee por defecto.
                await userManager.AddToRoleAsync(newUser, "Employee");

                if (Array.IndexOf(admins, newUser.UserName) <= -1)
                {
                    continue;
                }

                var roles = new[] { "SuperUser", "Admin", "Staff" };
                await userManager.AddToRolesAsync(newUser, roles);
                _logger.LogInformation($"Usuario {newUser.UserName}, roles asignados: {roles}");
            }
        }
    }
}
