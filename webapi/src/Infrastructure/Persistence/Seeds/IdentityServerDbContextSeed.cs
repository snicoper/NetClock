using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace NetClock.Infrastructure.Persistence.Seeds
{
    public static class IdentityServerDbContextSeed
    {
        private static IServiceProvider _serviceProvider;
        private static ILogger<ConfigurationDbContext> _logger;

        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _logger = serviceProvider.GetRequiredService<ILogger<ConfigurationDbContext>>();

            var dbContext = serviceProvider.GetRequiredService<ConfigurationDbContext>();
            await dbContext.Database.EnsureCreatedAsync();

            await SeedResources();
            await SeedApis();
            await SeedClients();
        }

        private static async Task SeedResources()
        {
            var dbContext = _serviceProvider.GetRequiredService<ConfigurationDbContext>();
            if (await dbContext.IdentityResources.AnyAsync())
            {
                _logger.LogInformation("Ya hay IdentityResource en la db.");
                return;
            }

            var resources = new List<IdentityResource>
            {
                new IdentityResources.OpenId(), new IdentityResources.Email(), new IdentityResources.Profile()
            };

            foreach (var resource in resources)
            {
                await dbContext.IdentityResources.AddAsync(resource.ToEntity());
            }

            await dbContext.SaveChangesAsync();
            _logger.LogInformation("Creados Client en la db.");
        }

        private static async Task SeedApis()
        {
            var dbContext = _serviceProvider.GetRequiredService<ConfigurationDbContext>();

            if (await dbContext.ApiResources.AnyAsync())
            {
                _logger.LogInformation("Ya hay ApiResource en la db.");
                return;
            }

            var apis = new List<ApiResource>
            {
                new ApiResource("api1", "Api test")
            };

            foreach (var api in apis)
            {
                await dbContext.ApiResources.AddAsync(api.ToEntity());
            }

            await dbContext.SaveChangesAsync();
            _logger.LogInformation("Creados ApiResource en la db.");
        }

        private static async Task SeedClients()
        {
            var dbContext = _serviceProvider.GetRequiredService<ConfigurationDbContext>();
            if (await dbContext.Clients.AnyAsync())
            {
                _logger.LogInformation("Ya hay Client en la db.");
                return;
            }

            var clients = new List<Client>
            {
                new Client
                {
                    ClientId = "api",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RedirectUris = { "https://localhost:5000/home/signin" },
                    PostLogoutRedirectUris = { "https://localhost:5000/home/index" },
                    AllowedCorsOrigins = { "http://localhost" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    },
                    AccessTokenLifetime = 1,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false
                }
            };

            foreach (var client in clients)
            {
                await dbContext.Clients.AddAsync(client.ToEntity());
            }

            await dbContext.SaveChangesAsync();
            _logger.LogInformation("Creados Client en la db.");
        }
    }
}
