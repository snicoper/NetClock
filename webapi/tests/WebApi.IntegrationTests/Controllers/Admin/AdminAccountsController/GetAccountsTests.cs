using System.Linq;
using System.Net;
using System.Threading.Tasks;
using NetClock.Application.Admin.AdminAccounts.Queries.GetAccounts;
using NetClock.Application.Common.Http;
using NetClock.Application.Common.Utils;
using NetClock.WebApi.IntegrationTests.Helpers;
using Shouldly;
using Xunit;

namespace NetClock.WebApi.IntegrationTests.Controllers.Admin.AdminAccountsController
{
    public class GetAccountsTests : BaseControllerTest
    {
        public GetAccountsTests(CustomWebApplicationFactory<Startup> factory)
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
            var responseContent =
                await SerializerUtils.GetResponseContentAsync<ResponseData<GetAccountsDto>>(response);

            // Assert
            response.EnsureSuccessStatusCode();
            responseContent.ShouldBeOfType<ResponseData<GetAccountsDto>>();
            responseContent.Items.Count().ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task Get_usuario_bob_no_tiene_permisos_para_ver_lista_usuarios_Forbidden()
        {
            // Arrange
            await GetAuthenticatedClientAsync("Bob", "123456");

            // Act
            var response = await Client.GetAsync(BaseUrl);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Forbidden);
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
