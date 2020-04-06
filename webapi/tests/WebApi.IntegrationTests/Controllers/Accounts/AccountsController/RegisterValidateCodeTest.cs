using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using NetClock.Application.Accounts.Accounts.Commands.Register;
using NetClock.Application.Accounts.Accounts.Commands.RegisterValidate;
using NetClock.Application.Common.Utils;
using NetClock.WebApi.IntegrationTests.Helpers;
using Shouldly;
using Xunit;

namespace NetClock.WebApi.IntegrationTests.Controllers.Accounts.AccountsController
{
    public class RegisterValidateCodeTest : BaseControllerTest
    {
        public RegisterValidateCodeTest(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
            BaseUrl = Utilities.ComposeUri("accounts/register/validate");
        }

        [Fact]
        public async Task Post_validar_registro_usuario_OK()
        {
            // Arrange
            var responseContent = await RegisterUser();
            var parts = responseContent.Callback.Split("code=");
            var code = parts.Last();
            var data = new RegisterValidateCommand(responseContent.Id, code);
            var requestContent = SerializerUtils.GetRequestContent(data);

            // Act
            var response = await Client.PostAsync(BaseUrl, requestContent);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Post_validar_registro_usuario_invalido_NotFound()
        {
            // Arrange
            var responseContent = await RegisterUser();
            var data = new RegisterValidateCommand(responseContent.Id, "token-no-valido");
            var requestContent = SerializerUtils.GetRequestContent(data);

            // Act
            var response = await Client.PostAsync(BaseUrl, requestContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Post_validar_registro_usuario_no_existe_NotFound()
        {
            // Arrange
            var data = new RegisterValidateCommand("123123123", "token-no-valido");
            var requestContent = SerializerUtils.GetRequestContent(data);

            // Act
            var response = await Client.PostAsync(BaseUrl, requestContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// Registrar usuario con userName perico.
        /// </summary>
        /// <returns>Información básica del registro</returns>
        private async Task<RegisterDto> RegisterUser()
        {
            var guid = Guid.NewGuid().ToString().Substring(0, 7);
            var uri = Utilities.ComposeUri("accounts/register");
            var data = new RegisterCommand(guid, guid, guid, $"{guid}@example.com", "123456", "123456");
            var requestContent = SerializerUtils.GetRequestContent(data);
            var response = await Client.PostAsync(uri, requestContent);
            var responseContent = await SerializerUtils.GetResponseContentAsync<RegisterDto>(response);

            return responseContent;
        }
    }
}
