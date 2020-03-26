using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NetClock.Application.Admin.AdminAccounts.Commands.UpdateUser;
using NetClock.Application.Common.Utils;
using NetClock.Domain.Entities.Identity;
using NetClock.WebApi.IntegrationTests.Helpers;
using Shouldly;
using Xunit;

namespace NetClock.WebApi.IntegrationTests.Controllers.Admin.AdminAccountsController
{
    public class UpdateUserTests : BaseControllerTest
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UpdateUserTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
            _userManager = ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        }

        [Fact]
        public async Task Post_actualizar_usuario_autenticado_Created()
        {
            // Arrange
            await GetAuthenticatedClientAsync();
            var user = await _userManager.FindByNameAsync("Bob");
            var userId = user.Id;
            var data = new UpdateUserCommand(userId, userId, "Bob112", "Bob112", "Bob212", "bob112@example.com", false);
            var requestContent = SerializerUtils.GetRequestContent(data);
            var url = Utilities.ComposeUri($"admin/accounts/{user.Id}");

            // Act
            var response = await Client.PutAsync(url, requestContent);
            var responseContent = await SerializerUtils.GetResponseContentAsync<UpdateUserViewModel>(response);

            // Assert
            response.EnsureSuccessStatusCode();
            responseContent.Slug.ShouldBe("bob112");
        }

        [Fact]
        public async Task Post_actualizar_usuario_no_autenticado_Unauthorized()
        {
            // Arrange
            var user = await _userManager.FindByNameAsync("Bob");
            var userId = user.Id;
            var data = new UpdateUserCommand(userId, userId, "Bob112", "Bob112", "Bob212", "bob112@example.com", false);
            var requestContent = SerializerUtils.GetRequestContent(data);
            var url = Utilities.ComposeUri($"admin/accounts/{user.Id}");

            // Act
            var response = await Client.PutAsync(url, requestContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }

        [Theory]
        [InlineData("Abc", "Test", "User", "test@example.com")] // UserName corto mínimo 5.
        [InlineData("Admin", "Test", "User", "test@example.com")] // UserName en uso.
        [InlineData("Abc123", "Admin", "Admin", "test@example.com")] // FirstName y LastName en uso.
        [InlineData("Abc123", "Test", "User", "test@example")] // Invalid email.
        [InlineData("", "Test", "User", "test@example.com")] // UserName required.
        [InlineData("Abc123", "", "User", "test@example.com")] // FirstName required.
        [InlineData("Abc123", "Test", "", "test@example.com")] // LastName required.
        [InlineData("Abc123", "Test", "User", "")] // Email required.
        public async Task Post_actualizar_usuario_invalid_data_BadRequest(
            string userName,
            string firstName,
            string lastName,
            string email)
        {
            // Arrange
            await GetAuthenticatedClientAsync();
            var user = await _userManager.FindByNameAsync("Bob");
            var userId = user.Id;
            var data = new UpdateUserCommand(userId, userId, userName, firstName, lastName, email, false);
            var requestContent = SerializerUtils.GetRequestContent(data);
            var url = Utilities.ComposeUri($"admin/accounts/{user.Id}");

            // Act
            var response = await Client.PutAsync(url, requestContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Post_actualizar_usuario_IdParam_y_Id_no_iguales_BadRequest()
        {
            // Arrange
            await GetAuthenticatedClientAsync();
            var user = await _userManager.FindByNameAsync("Bob");
            var userId = user.Id;
            var data = new UpdateUserCommand(userId, userId, "Bob112", "Bob112", "Bob212", "bob112@example.com", false);
            var requestContent = SerializerUtils.GetRequestContent(data);
            var url = Utilities.ComposeUri("admin/accounts/no-existe");

            // Act
            var response = await Client.PutAsync(url, requestContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
    }
}