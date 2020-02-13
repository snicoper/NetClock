using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NetClock.Application.Accounts.Auth.Commands.Login;
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
        }

        [Fact]
        public async Task Post_usuario_loguea_correctamente_200Ok()
        {
            // Arrange
            var uri = Utilities.ComposeUri("auth/login");
            var data = new LoginCommand("Admin", "123456", true);
            var requestContent = Utilities.GetRequestContent(data);

            // Act
            var response = await Client.PostAsync(uri, requestContent);
            var responseContent = await Utilities.GetResponseContentAsync<CurrentUserViewModel>(response);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            responseContent.UserName.ShouldNotBeNullOrEmpty();
        }

        [Theory]
        [InlineData("admin1", "123456")] // UserName incorrecto.
        [InlineData("Admin", "12345678")] // Password incorrecto.
        public async Task Post_usuario_no_loguea_con_credenciales_incorrectas_400BadRequest(string userName, string password)
        {
            // Arrange
            var uri = Utilities.ComposeUri("auth/login");
            var data = new LoginCommand(userName, password, true);
            var requestContent = Utilities.GetRequestContent(data);

            // Act
            var response = await Client.PostAsync(uri, requestContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("", "123456")] // UserName vacío.
        [InlineData("Admin", "")] // Password vacío.
        public async Task Post_usuario_no_loguea_form_no_valido_400BadRequest(string userName, string password)
        {
            // Arrange
            var uri = Utilities.ComposeUri("auth/login");
            var data = new LoginCommand(userName, password, true);
            var requestContent = Utilities.GetRequestContent(data);

            // Act
            var response = await Client.PostAsync(uri, requestContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Post_usuario_no_activo_no_puede_loguear_400BadRequest()
        {
            // Arrange
            var userManager = ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var bob = await userManager.FindByNameAsync("Bob");
            bob.Active = false;
            await userManager.UpdateAsync(bob);

            var uri = Utilities.ComposeUri("auth/login");
            var data = new LoginCommand("Bob", "123456", true);
            var requestContent = Utilities.GetRequestContent(data);

            // Act
            var response = await Client.PostAsync(uri, requestContent);

            // Assert
            bob.Active.ShouldBeFalse();
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
    }
}
