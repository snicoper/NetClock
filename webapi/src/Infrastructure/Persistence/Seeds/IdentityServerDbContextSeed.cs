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

            await SeedIdentityResources();
            await SeedApiResources();
            await SeedClients();
        }

        private static async Task SeedIdentityResources()
        {
            var dbContext = _serviceProvider.GetRequiredService<ConfigurationDbContext>();
            if (await dbContext.IdentityResources.AnyAsync())
            {
                _logger.LogInformation("Ya hay IdentityResource en la db.");
                return;
            }

            var resources = new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

            foreach (var resource in resources)
            {
                await dbContext.IdentityResources.AddAsync(resource.ToEntity());
            }

            await dbContext.SaveChangesAsync();
            _logger.LogInformation("Creados Client en la db.");
        }

        private static async Task SeedApiResources()
        {
            var dbContext = _serviceProvider.GetRequiredService<ConfigurationDbContext>();

            if (await dbContext.ApiResources.AnyAsync())
            {
                _logger.LogInformation("Ya hay ApiResource en la db.");
                return;
            }

            var apis = new List<ApiResource> { new ApiResource("resource_api", "Angular client") };

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
                    ClientId = "clock_client",
                    ClientName = "Angular client",
                    AllowedGrantTypes = GrantTypes.Code,
                    ClientSecrets = { new Secret("acf2ec6fb01a4b698ba240c2b10a0243".Sha256()) },
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RedirectUris = { "http://localhost:4200/auth-callback" },
                    PostLogoutRedirectUris = { "http://localhost:4200" },
                    AllowedCorsOrigins = { "http://localhost:4200" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "resource_api"
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
