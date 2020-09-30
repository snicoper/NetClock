using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NetClock.Application.Accounts.Auth.Commands.Login;
using NetClock.Application.Common.Utils;
using NetClock.Domain.Entities.Identity;
using NetClock.WebApi.IntegrationTests.Helpers;
using Shouldly;
using Xunit;

namespace NetClock.WebApi.IntegrationTests.Controllers.Accounts.AuthController
{
    public class LoginTest : BaseControllerTest
    {
        public LoginTest(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
            BaseUrl = Utilities.ComposeUri("auth/login");
        }

        [Fact]
        public async Task Post_usuario_loguea_correctamente_Ok()
        {
            // Arrange
            var data = new LoginCommand("Admin", "123456", true);
            var requestContent = SerializerUtils.GetRequestContent(data);

            // Act
            var response = await Client.PostAsync(BaseUrl, requestContent);
            var responseContent = await SerializerUtils.GetResponseContentAsync<LoginDto>(response);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            responseContent.UserName.ShouldNotBeNullOrEmpty();
        }

        [Theory]
        [InlineData("admin1", "123456")] // UserName incorrecto.
        [InlineData("Admin", "12345678")] // Password incorrecto.
        public async Task Post_usuario_no_loguea_con_credenciales_incorrectas_BadRequest(string userName, string password)
        {
            // Arrange
            var data = new LoginCommand(userName, password, true);
            var requestContent = SerializerUtils.GetRequestContent(data);

            // Act
            var response = await Client.PostAsync(BaseUrl, requestContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("", "123456")] // UserName vacío.
        [InlineData("Admin", "")] // Password vacío.
        public async Task Post_usuario_no_loguea_form_no_valido_BadRequest(string userName, string password)
        {
            // Arrange
            var data = new LoginCommand(userName, password, true);
            var requestContent = SerializerUtils.GetRequestContent(data);

            // Act
            var response = await Client.PostAsync(BaseUrl, requestContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Post_usuario_no_activo_no_puede_loguear_BadRequest()
        {
            // Arrange
            var userManager = Factory.Services.GetRequiredService<UserManager<ApplicationUser>>();
            var bob = await userManager.FindByNameAsync("Bob");
            bob.Active = false;
            await userManager.UpdateAsync(bob);

            var data = new LoginCommand("Bob", "123456", true);
            var requestContent = SerializerUtils.GetRequestContent(data);

            // Act
            var response = await Client.PostAsync(BaseUrl, requestContent);

            // Assert
            bob.Active.ShouldBeFalse();
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
    }
}
