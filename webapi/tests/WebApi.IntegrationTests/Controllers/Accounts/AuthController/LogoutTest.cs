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
            BaseUrl = Utilities.ComposeUri("auth/logout");
        }

        [Fact]
        public async Task Post_usuario_logueado_desloguea_si_tiene_iniciada_session_204NoContent()
        {
            // Arrange
            await GetAuthenticatedClientAsync();

            // Act
            var response = await Client.PostAsync(BaseUrl, null);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Post_usuario_sin_session_Unauthorized_al_hacer_logout_401Unauthorized()
        {
            // Arrange

            // Act
            var response = await Client.PostAsync(BaseUrl, null);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }
    }
}
