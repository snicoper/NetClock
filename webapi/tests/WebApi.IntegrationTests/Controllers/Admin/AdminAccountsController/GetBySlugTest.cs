using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NetClock.Application.Admin.AdminAccounts.Queries.GetAccounts;
using NetClock.Application.Common.Utils;
using NetClock.Domain.Entities.Identity;
using NetClock.WebApi.IntegrationTests.Helpers;
using Shouldly;
using Xunit;

namespace NetClock.WebApi.IntegrationTests.Controllers.Admin.AdminAccountsController
{
    public class GetBySlugTest : BaseControllerTest
    {
        public GetBySlugTest(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task Get_usuario_logueado_obtiene_resultado_Ok()
        {
            // Arrange
            await GetAuthenticatedClientAsync();
            var userManager = Factory.Services.GetRequiredService<UserManager<ApplicationUser>>();
            var userBob = await userManager.FindByNameAsync("Bob");
            var uri = Utilities.ComposeUri($"admin/accounts/{userBob.Slug}");

            // Act
            var response = await Client.GetAsync(uri);
            var responseContent = await SerializerUtils.GetResponseContentAsync<GetAccountsDto>(response);

            // Assert
            response.EnsureSuccessStatusCode();
            responseContent.ShouldBeOfType<GetAccountsDto>();
            responseContent.UserName.ShouldNotBeEmpty();
        }
        [Fact]
        public async Task Get_usuario_Bob_Forbidden()
        {
            // Arrange
            await GetAuthenticatedClientAsync("Bob", "123456");
            var userManager = Factory.Services.GetRequiredService<UserManager<ApplicationUser>>();
            var userBob = await userManager.FindByNameAsync("Bob");
            var uri = Utilities.ComposeUri($"admin/accounts/{userBob.Slug}");

            // Act
            var response = await Client.GetAsync(uri);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Forbidden);
        }


        [Fact]
        public async Task Get_usuario_no_logueado_obtiene_resultado_Unauthorized()
        {
            // Arrange
            var userManager = Factory.Services.GetRequiredService<UserManager<ApplicationUser>>();
            var userBob = await userManager.FindByNameAsync("Bob");
            var uri = Utilities.ComposeUri($"admin/accounts/{userBob.Slug}");

            // Act
            var response = await Client.GetAsync(uri);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }
    }
}
