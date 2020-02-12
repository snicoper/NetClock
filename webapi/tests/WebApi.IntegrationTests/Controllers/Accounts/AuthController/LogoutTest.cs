using System.Net;
using System.Threading.Tasks;
using NetClock.WebApi.IntegrationTests.Helpers;
using Shouldly;
using Xunit;

namespace NetClock.WebApi.IntegrationTests.Controllers.Accounts.AuthController
{
    public class LogoutTest : BaseControllerTest
    {
        public LogoutTest(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task Post_usuario_logueado_desloguea_si_tiene_iniciada_session_204NoContent()
        {
            // Arrange
            await GetAuthenticatedClientAsync();
            var uri = Utilities.ComposeUri("auth/logout");

            // Act
            var response = await Client.PostAsync(uri, null);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Post_usuario_sin_session_Unauthorized_al_hacer_logout_401Unauthorized()
        {
            // Arrange
            var uri = Utilities.ComposeUri("auth/logout");

            // Act
            var response = await Client.PostAsync(uri, null);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }
    }
}
