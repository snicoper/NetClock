using System.Net;
using System.Threading.Tasks;
using NetClock.Application.Admin.AdminAccounts.Commands.CreateAccount;
using NetClock.Application.Common.Utils;
using NetClock.WebApi.IntegrationTests.Helpers;
using Shouldly;
using Xunit;

namespace NetClock.WebApi.IntegrationTests.Controllers.Admin.AdminAccountsController
{
    public class CreateAccountTests : BaseControllerTest
    {
        public CreateAccountTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
            BaseUrl = Utilities.ComposeUri("admin/accounts");
        }

        [Fact]
        public async Task Post_registro_usuario_require_authenticated_Created()
        {
            // Arrange
            await GetAuthenticatedClientAsync();
            var data = new CreateAccountCommand("testUser", "test", "User", "test@example.com", "123123", "123123", true);
            var requestContent = SerializerUtils.GetRequestContent(data);

            // Act
            var response = await Client.PostAsync(BaseUrl, requestContent);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.ShouldBe(HttpStatusCode.Created);
        }

        [Fact]
        public async Task Post_registro_usuario_Bob_Forbidden()
        {
            // Arrange
            await GetAuthenticatedClientAsync("Bob", "123456");
            var data = new CreateAccountCommand("testUser", "test", "User", "test@example.com", "123123", "123123", true);
            var requestContent = SerializerUtils.GetRequestContent(data);

            // Act
            var response = await Client.PostAsync(BaseUrl, requestContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Post_registro_usuario_no_authenticated_Unauthorized()
        {
            // Arrange
            var data = new CreateAccountCommand("testUser", "test", "User", "test@example.com", "123123", "123123", true);
            var requestContent = SerializerUtils.GetRequestContent(data);

            // Act
            var response = await Client.PostAsync(BaseUrl, requestContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }

        [Theory]
        [InlineData("Admin", "Admin", "Admin", "admin1@example.com", "123456", "123456", true)] // UserName ya existe.
        [InlineData("Admin1", "Admin", "Admin", "admin@example.com", "123456", "123456", true)] // Email ya existe.
        [InlineData("Admin1", "Admin", "Admin", "admin1@example.com", "123456", "1234567", true)] // Contraseñas diferentes.
        [InlineData("Admin1", "Admin", "Admin", "admin1@example.com", "123", "123", true)] // Contraseña corta.
        [InlineData("Ad", "Admin", "Admin", "admin1@example.com", "123456", "123456", true)] // UserName corto.
        [InlineData("Ad min", "Admin", "Admin", "admin1@example.com", "123456", "123456", true)] // UserName invalido (espacio).
        [InlineData("Admin", "Admin", "Admin", "admin1@example.com", "123 456", "123 456", true)] // Password invalido (espacio).
        [InlineData("Admin1", "Admin", "Admin", "admin1.com", "123456", "123456", true)] // Email invalido.
        [InlineData("", "Admin", "Admin", "admin1.com", "123456", "123456", true)] // UserName vacío.
        [InlineData("Admin1", "Admin", "Admin", "", "123456", "123456", true)] // Email vacío.
        [InlineData("Admin1", "Admin", "Admin", "admin1@example.com", "", "", true)] // Passwords vacíos.
        [InlineData("Admin1", "", "Admin", "admin1@example.com", "123456", "123456", true)] // FirstName vacío.
        [InlineData("Admin1", "Admin", "", "admin1@example.com", "123456", "123456", true)] // LastName vacío.
        [InlineData("TestUser", "Admin", "Admin", "admin1123@example.com", "123456", "123456", true)] // First y Last name repetidos.
        public async Task Post_registro_usuario_BadRequest(
            string username,
            string firstName,
            string lastName,
            string email,
            string password,
            string confirmPassword,
            bool active)
        {
            // Arrange
            await GetAuthenticatedClientAsync();
            var data = new CreateAccountCommand(username, firstName, lastName, email, password, confirmPassword, active);
            var requestContent = SerializerUtils.GetRequestContent(data);

            // Act
            var response = await Client.PostAsync(BaseUrl, requestContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
    }
}
