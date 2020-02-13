using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NetClock.Application.Admin.AdminAccounts.Queries.GetUsers;
using NetClock.Domain.Entities.Identity;
using NetClock.WebApi.IntegrationTests.Helpers;
using Shouldly;
using Xunit;

namespace NetClock.WebApi.IntegrationTests.Controllers.Admin.AdminAccountsController
{
    public class GetBySlugTests : BaseControllerTest
    {
        public GetBySlugTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task Get_usuario_logueado_obtiene_resultado_200Ok()
        {
            // Arrange
            await GetAuthenticatedClientAsync();
            var userManager = ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var userBob = await userManager.FindByNameAsync("Bob");
            var uri = Utilities.ComposeUri($"admin/accounts/{userBob.Slug}");

            // Act
            var response = await Client.GetAsync(uri);
            var responseContent = await Utilities.GetResponseContentAsync<AdminUserListViewModel>(response);

            // Assert
            response.EnsureSuccessStatusCode();
            responseContent.ShouldBeOfType<AdminUserListViewModel>();
            responseContent.UserName.ShouldNotBeEmpty();
        }
    }
}
