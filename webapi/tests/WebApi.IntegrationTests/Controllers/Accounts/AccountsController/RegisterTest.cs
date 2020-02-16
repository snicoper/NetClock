using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NetClock.Application.Accounts.Accounts.Commands.Register;
using NetClock.Application.Accounts.Auth.Commands.Login;
using NetClock.Domain.Entities.Identity;
using NetClock.WebApi.IntegrationTests.Helpers;
using Shouldly;
using Xunit;

namespace NetClock.WebApi.IntegrationTests.Controllers.Accounts.AccountsController
{
    public class RegisterTest : BaseControllerTest
    {
        public RegisterTest(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task Post_registro_de_usuario_201Created()
        {
            // Arrange
            var uri = Utilities.ComposeUri("accounts/register");
            var data = new RegisterCommand("testUser", "Perico", "Palote", "testUser@example.com", "123456", "123456");
            var requestContent = Utilities.GetRequestContent(data);

            // Act
            var response = await Client.PostAsync(uri, requestContent);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.ShouldBe(HttpStatusCode.Created);
        }

        [Theory]
        [InlineData("Admin", "Admin", "Admin", "admin1@example.com", "123456", "123456")] // UserName ya existe.
        [InlineData("Admin1", "Admin", "Admin", "admin@example.com", "123456", "123456")] // Email ya existe.
        [InlineData("Admin1", "Admin", "Admin", "admin1@example.com", "123456", "1234567")] // Contraseñas diferentes.
        [InlineData("Admin1", "Admin", "Admin", "admin1@example.com", "123", "123")] // Contraseña corta.
        [InlineData("Ad", "Admin", "Admin", "admin1@example.com", "123456", "123456")] // UserName corto.
        [InlineData("Ad min", "Admin", "Admin", "admin1@example.com", "123456", "123456")] // UserName invalido (espacio).
        [InlineData("Admin", "Admin", "Admin", "admin1@example.com", "123 456", "123 456")] // Password invalido (espacio).
        [InlineData("Admin1", "Admin", "Admin", "admin1.com", "123456", "123456")] // Email invalido.
        [InlineData("", "Admin", "Admin", "admin1.com", "123456", "123456")] // UserName vacío.
        [InlineData("Admin1", "Admin", "Admin", "", "123456", "123456")] // Email vacío.
        [InlineData("Admin1", "Admin", "Admin", "admin1@example.com", "", "")] // Passwords vacíos.
        [InlineData("Admin1", "", "Admin", "admin1@example.com", "123456", "123456")] // FirstName vacío.
        [InlineData("Admin1", "Admin", "", "admin1@example.com", "123456", "123456")] // LastName vacío.
        // [InlineData("TestUser", "Admin", "Admin", "admin1123@example.com", "123456", "123456")] // First y Last name repetidos.
        public async Task Post_registro_de_usuario_400BadRequest(
            string username,
            string firstName,
            string lastName,
            string email,
            string password,
            string confirmPassword)
        {
            // FIXME: El test no debería pasar, según la configuración.
            // builder.HasIndex(e => new { e.FirstName, e.LastName }).IsUnique();
            // Sin embargo, la prueba pasa teniendo todos el mismo first y last name.

            // Arrange
            var uri = Utilities.ComposeUri("accounts/register");
            var data = new RegisterCommand(username, firstName, lastName, email, password, confirmPassword);
            var requestContent = Utilities.GetRequestContent(data);

            // Act
            var response = await Client.PostAsync(uri, requestContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Post_registro_usuario_requiere_confirmacion_de_email_201Created()
        {
            // Arrange
            var guid = Guid.NewGuid().ToString().Substring(0, 7);
            var uri = Utilities.ComposeUri("accounts/register");
            var data = new RegisterCommand(guid, guid, guid, $"{guid}@example.com", "123456", "123456");
            var requestContent = Utilities.GetRequestContent(data);
            var userManager = ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Act
            var response = await Client.PostAsync(uri, requestContent);
            var responseContent = await Utilities.GetResponseContentAsync<CurrentUserViewModel>(response);
            var user = await userManager.FindByNameAsync(responseContent.UserName);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.ShouldBe(HttpStatusCode.Created);
            user.EmailConfirmed.ShouldBeFalse();
        }
    }
}
