using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NetClock.Application.Admin.AdminAccounts.Commands.UpdateAccount;
using NetClock.Application.Common.Utils;
using NetClock.Domain.Entities.Identity;
using NetClock.WebApi.IntegrationTests.Helpers;
using Shouldly;
using Xunit;

namespace NetClock.WebApi.IntegrationTests.Controllers.Admin.AdminAccountsController
{
    public class UpdateAccountTest : BaseControllerTest
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UpdateAccountTest(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
            _userManager = Factory.Services.GetRequiredService<UserManager<ApplicationUser>>();
        }

        [Fact]
        public async Task Post_actualizar_usuario_autenticado_Created()
        {
            // Arrange
            await GetAuthenticatedClientAsync();
            var user = await _userManager.FindByNameAsync("Bob");
            var userId = user.Id;
            var data = new UpdateUserCommand(userId, "Bob112", "Bob112", "Bob212", "bob112@example.com", false);
            var requestContent = SerializerUtils.GetRequestContent(data);
            var url = Utilities.ComposeUri("admin/accounts/update");

            // Act
            var response = await Client.PutAsync(url, requestContent);
            var responseContent = await SerializerUtils.GetResponseContentAsync<UpdateUserDto>(response);

            // Assert
            response.EnsureSuccessStatusCode();
            responseContent.Slug.ShouldBe("bob112");
        }

        [Fact]
        public async Task Post_actualizar_usuario_Bob_Forbidden()
        {
            // Arrange
            await GetAuthenticatedClientAsync("Bob", "123456");
            var user = await _userManager.FindByNameAsync("Bob");
            var userId = user.Id;
            var data = new UpdateUserCommand(userId, "Bob112", "Bob112", "Bob212", "bob112@example.com", false);
            var requestContent = SerializerUtils.GetRequestContent(data);
            var url = Utilities.ComposeUri($"admin/accounts/update");

            // Act
            var response = await Client.PutAsync(url, requestContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Post_actualizar_usuario_no_autenticado_Unauthorized()
        {
            // Arrange
            var user = await _userManager.FindByNameAsync("Bob");
            var userId = user.Id;
            var data = new UpdateUserCommand(userId, "Bob112", "Bob112", "Bob212", "bob112@example.com", false);
            var requestContent = SerializerUtils.GetRequestContent(data);
            var url = Utilities.ComposeUri($"admin/accounts/update");

            // Act
            var response = await Client.PutAsync(url, requestContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }

        [Theory]
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
            var data = new UpdateUserCommand(userId, userName, firstName, lastName, email, false);
            var requestContent = SerializerUtils.GetRequestContent(data);
            var url = Utilities.ComposeUri($"admin/accounts/update");

            // Act
            var response = await Client.PutAsync(url, requestContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
    }
}
