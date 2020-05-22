using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NetClock.Application.Admin.AdminAccounts.Commands.ChangePasswordAccount;
using NetClock.Application.Common.Utils;
using NetClock.Domain.Entities.Identity;
using NetClock.WebApi.IntegrationTests.Helpers;
using Shouldly;
using Xunit;

namespace NetClock.WebApi.IntegrationTests.Controllers.Admin.AdminAccountsController
{
    public class ChangePasswordAccountTest : BaseControllerTest
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ChangePasswordAccountTest(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
            _userManager = ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        }

        [Fact]
        public async Task Post_actualizar_password_usuario_autenticado_Created()
        {
            // Arrange
            await GetAuthenticatedClientAsync();
            var user = await _userManager.FindByNameAsync("Bob");
            var userId = user.Id;
            var data = new ChangePasswordAccountCommand(userId, "123123", "123123");
            var requestContent = SerializerUtils.GetRequestContent(data);
            var url = Utilities.ComposeUri("admin/accounts/change-password");

            // Act
            var response = await Client.PostAsync(url, requestContent);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Post_actualizar_password_usuario_Bob_Forbidden()
        {
            // Arrange
            await GetAuthenticatedClientAsync("Bob", "123456");
            var user = await _userManager.FindByNameAsync("Alice");
            var userId = user.Id;
            var data = new ChangePasswordAccountCommand(userId, "123123", "123123");
            var requestContent = SerializerUtils.GetRequestContent(data);
            var url = Utilities.ComposeUri("admin/accounts/change-password");

            // Act
            var response = await Client.PostAsync(url, requestContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Post_actualizar_password_usuario_anonimo_Unauthorized()
        {
            // Arrange
            var user = await _userManager.FindByNameAsync("Bob");
            var userId = user.Id;
            var data = new ChangePasswordAccountCommand(userId, "123123", "123123");
            var requestContent = SerializerUtils.GetRequestContent(data);
            var url = Utilities.ComposeUri("admin/accounts/change-password");

            // Act
            var response = await Client.PostAsync(url, requestContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }

        [Theory]
        [InlineData("123", "123")] // Contraseña corta.
        [InlineData("123123", "123333")] // Contraseñas diferentes.
        public async Task Post_actualizar_password_validacion_form_BadRequest(
            string newPassword,
            string confirmPassword)
        {
            // Arrange
            await GetAuthenticatedClientAsync();
            var user = await _userManager.FindByNameAsync("Bob");
            var userId = user.Id;
            var data = new ChangePasswordAccountCommand(userId, newPassword, confirmPassword);
            var requestContent = SerializerUtils.GetRequestContent(data);
            var url = Utilities.ComposeUri("admin/accounts/change-password");

            // Act
            var response = await Client.PostAsync(url, requestContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
    }
}
