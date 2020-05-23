using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetClock.Application.Common.Authorization;
using NetClock.Application.Common.Authorization.Constants;
using NetClock.Domain.Entities.Identity;
using NetClock.Domain.Extensions;

namespace NetClock.Infrastructure.Persistence.Seeds
{
    public static class SeedUsers
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider, ILogger<ApplicationDbContext> logger)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
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
                    logger.LogInformation($"Usuario {username} ya existe en la base de datos");
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
                    logger.LogInformation($"ERROR: {result.Errors}");
                    continue;
                }

                logger.LogInformation($"Usuario {username} creado con éxito");

                // Add role Employee.
                await userManager.AddToRoleAsync(newUser, AppRoles.Employee);
                logger.LogInformation($"Usuario {newUser.UserName}, role asignado: {AppRoles.Employee}");

                // Add role Staff.
                if (Array.IndexOf(staffs, newUser.UserName) >= 0)
                {
                    await userManager.AddToRoleAsync(newUser, AppRoles.Staff);
                    logger.LogInformation($"Usuario {newUser.UserName}, role asignado: {AppRoles.Staff}");
                }

                // Add role Superuser.
                if (Array.IndexOf(superusers, newUser.UserName) == -1)
                {
                    continue;
                }

                await userManager.AddToRoleAsync(newUser, AppRoles.Superuser);
                logger.LogInformation($"Usuario {newUser.UserName}, role asignado: {AppRoles.Superuser}");
            }
        }
    }
}
