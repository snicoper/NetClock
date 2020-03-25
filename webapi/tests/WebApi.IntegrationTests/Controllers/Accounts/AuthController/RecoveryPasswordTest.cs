using System.Net;
using System.Threading.Tasks;
using NetClock.Application.Accounts.Auth.Commands.RecoveryPassword;
using NetClock.Application.Common.Utils;
using NetClock.WebApi.IntegrationTests.Helpers;
using Shouldly;
using Xunit;

namespace NetClock.WebApi.IntegrationTests.Controllers.Accounts.AuthController
{
    public class RecoveryPasswordTest : BaseControllerTest
    {
        public RecoveryPasswordTest(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
            BaseUrl = Utilities.ComposeUri("auth/recovery-password");
        }

        [Fact]
        public async Task Post_recuperar_contrasena_valida_Created()
        {
            // Arrange
            var data = new RecoveryPasswordCommand("admin@example.com");
            var requestContent = SerializerUtils.GetRequestContent(data);

            // Act
            var response = await Client.PostAsync(BaseUrl, requestContent);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [InlineData("admin")]
        [InlineData("admin@")]
        [InlineData("admin@example")]
        public async Task Post_email_invalido_BadRequest(string email)
        {
            // Arrange
            var data = new RecoveryPasswordCommand(email);
            var requestContent = SerializerUtils.GetRequestContent(data);

            // Act
            var response = await Client.PostAsync(BaseUrl, requestContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Post_email_no_existe_BadRequest()
        {
            // Arrange
            var data = new RecoveryPasswordCommand("no@example.com");
            var requestContent = SerializerUtils.GetRequestContent(data);

            // Act
            var response = await Client.PostAsync(BaseUrl, requestContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
