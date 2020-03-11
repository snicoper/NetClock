using System.Linq;
using System.Net;
using System.Threading.Tasks;
using NetClock.Application.Admin.AdminAccounts.Queries.GetUsers;
using NetClock.Application.Common.Http;
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
            BaseUrl = Utilities.ComposeUri("admin/accounts");
        }

        [Fact]
        public async Task Get_obtener_lista_de_usuarios_Ok()
        {
            // Arrange
            await GetAuthenticatedClientAsync();

            // Act
            var response = await Client.GetAsync(BaseUrl);
            var responseContent = await Utilities.GetResponseContentAsync<ResponseData<AdminUserListViewModel>>(response);

            // Assert
            response.EnsureSuccessStatusCode();
            responseContent.ShouldBeOfType<ResponseData<AdminUserListViewModel>>();
            responseContent.Items.Count().ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task Get_obtener_lista_de_usuarios_anonymous_Unauthorized()
        {
            // Arrange

            // Act
            var response = await Client.GetAsync(BaseUrl);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }

        [Fact(Skip = "Cuando se implemente los permisos")]
        public async Task Get_obtener_lista_de_usuarios_registrado_sin_permisos_Unauthorized()
        {
            // Arrange
            await GetAuthenticatedClientAsync("Bob", "123456");

            // Act
            var response = await Client.GetAsync(BaseUrl);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }
    }
}
