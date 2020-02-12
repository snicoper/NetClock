using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
        }

        private static async Task SeedRoles()
        {
            var roleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var roles = new[] {"Superuser", "Admin", "Staff", "Employee"};

            foreach (var role in roles)
            {
                if (await roleManager.Roles.AnyAsync(r => r.Name == role))
                {
                    _logger.LogInformation($"Role {role} ya existe en la base de datos");
                    continue;
                }

                await roleManager.CreateAsync(new IdentityRole {Name = role});
                _logger.LogInformation($"Role {role} creado con éxito");
            }
        }


        private static async Task SeedUsers()
        {
            var userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var usernames = new[] {"Admin", "Alice", "Bob", "Joe"};
            var admins = new[] {"Admin"};
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

                var roles = new[] {"SuperUser", "Admin", "Staff"};
                await userManager.AddToRolesAsync(newUser, roles);
                _logger.LogInformation($"Usuario {newUser.UserName}, roles asignados: {roles}");
            }
        }
    }
}
