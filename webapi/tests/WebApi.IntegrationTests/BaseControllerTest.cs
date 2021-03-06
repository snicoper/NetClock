using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetClock.Application.Accounts.Auth.Commands.Login;
using NetClock.Application.Common.Utils;
using NetClock.Infrastructure.Persistence;
using NetClock.Infrastructure.Persistence.Seeds;
using NetClock.WebApi.IntegrationTests.Helpers;
using Xunit;

namespace NetClock.WebApi.IntegrationTests
{
    public class BaseControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        protected readonly CustomWebApplicationFactory<Startup> Factory;
        protected readonly HttpClient Client;
        protected string BaseUrl;

        protected BaseControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            Factory = factory;
            Client = Factory.CreateClient();
            RestoreDatabase().GetAwaiter().GetResult();
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
            var requestContent = SerializerUtils.GetRequestContent(data);
            var response = await Client.PostAsync(uri, requestContent);
            var responseContent = await SerializerUtils.GetResponseContentAsync<LoginDto>(response);

            return responseContent.Token;
        }

        /// <summary>
        /// Restaurar la base de datos en cada test.
        /// De lo contrario da problemas con la autenticación.
        /// </summary>
        private async Task RestoreDatabase()
        {
            var host = Factory.Services.GetRequiredService<IHost>();
            using var scope = host.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await ApplicationDbContextSeed.SeedAsync(Factory.Services, dbContext);
        }
    }
}
