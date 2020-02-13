using System.Linq;
using System.Net;
using System.Threading.Tasks;
using NetClock.Application.Admin.AdminAccounts.Queries.GetUsers;
using NetClock.Application.Common.Models.Http;
using NetClock.WebApi.IntegrationTests.Helpers;
using Shouldly;
using Xunit;

namespace NetClock.WebApi.IntegrationTests.Controllers.Admin.AdminAccountsController
{
    public class GetUsersTests : BaseControllerTest
    {
        public GetUsersTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task Get_obtener_lista_de_usuarios_200Ok()
        {
            // Arrange
            await GetAuthenticatedClientAsync();
            var uri = Utilities.ComposeUri("admin/accounts");

            // Act
            var response = await Client.GetAsync(uri);
            var responseContent = await Utilities.GetResponseContentAsync<ResponseData<AdminUserListViewModel>>(response);

            // Assert
            response.EnsureSuccessStatusCode();
            responseContent.ShouldBeOfType<ResponseData<AdminUserListViewModel>>();
            responseContent.Items.Count().ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task Get_obtener_lista_de_usuarios_anonymous_401Unauthorized()
        {
            // Arrange
            var uri = Utilities.ComposeUri("admin/accounts");

            // Act
            var response = await Client.GetAsync(uri);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }

        // TODO: Probar role o permisos.
        [Fact(Skip = "Cuando se implemente los permisos")]
        public async Task Get_obtener_lista_de_usuarios_registrado_sin_permisos_401Unauthorized()
        {
            // Arrange
            await GetAuthenticatedClientAsync("Bob", "123456");
            var uri = Utilities.ComposeUri("admin/accounts");

            // Act
            var response = await Client.GetAsync(uri);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }
    }
}
