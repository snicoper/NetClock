using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.Extensions.DependencyInjection;
using NetClock.Application.Cqrs.Accounts.Auth.Commands.Login;
using NetClock.Infrastructure.Persistence;
using NetClock.Infrastructure.Persistence.Seeds;
using NetClock.WebApi.IntegrationTests.Helpers;
using Xunit;

namespace NetClock.WebApi.IntegrationTests
{
    public class BaseControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        protected readonly CustomWebApplicationFactory<Startup> Factory;
        protected readonly IServiceProvider ServiceProvider;
        protected readonly HttpClient Client;

        protected BaseControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            Factory = factory;
            ServiceProvider = Factory.Services;
            RestoreDatabase().GetAwaiter().GetResult();
            Client = Factory.CreateClient();
        }

        protected async Task GetAuthenticatedClientAsync()
        {
            await GetAuthenticatedClientAsync("Admin", "123456");
        }

        protected async Task GetAuthenticatedClientAsync(string userName, string password)
        {
            var token = await GetAccessTokenAsync(userName, password);
            Client.SetBearerToken(token);
        }

        private async Task<string> GetAccessTokenAsync(string userName, string password)
        {
            var uri = Utilities.ComposeUri("auth/login");
            var data = new LoginCommand(userName, password, true);
            var requestContent = Utilities.GetRequestContent(data);
            var response = await Client.PostAsync(uri, requestContent);
            var responseContent = await Utilities.GetResponseContentAsync<CurrentUserViewModel>(response);

            return responseContent.Token;
        }

        /// <summary>
        /// Restaurar la base de datos en cada test.
        /// De lo contrario da problemas con la autenticación.
        /// </summary>
        private async Task RestoreDatabase()
        {
            var dbContext = ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();
            await ApplicationDbContextSeed.SeedAsync(ServiceProvider);
        }
    }
}
