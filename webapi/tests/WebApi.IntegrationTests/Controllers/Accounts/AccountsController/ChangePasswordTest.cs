using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NetClock.Application.Accounts.Accounts.Commands.ChangePassword;
using NetClock.Application.Common.Utils;
using NetClock.Domain.Entities.Identity;
using NetClock.WebApi.IntegrationTests.Helpers;
using Shouldly;
using Xunit;

namespace NetClock.WebApi.IntegrationTests.Controllers.Accounts.AccountsController
{
    public class ChangePasswordTest : BaseControllerTest
    {
        public ChangePasswordTest(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
            BaseUrl = Utilities.ComposeUri("accounts/change-password");
        }

        [Fact]
        public async Task Post_cambia_contrasena_correctamente_Ok()
        {
            // Arrange
            await GetAuthenticatedClientAsync();
            var userManager = ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var user = await userManager.FindByNameAsync("Admin");
            var data = new ChangePasswordCommand(user.Id, "123456", "1234567", "1234567");
            var requestContent = SerializerUtils.GetRequestContent(data);

            // Act
            var response = await Client.PostAsync(BaseUrl, requestContent);
            var responseContent = SerializerUtils.GetResponseContentAsync<ChangePasswordDto>(response);

            // Assert
            response.EnsureSuccessStatusCode();
            responseContent.Result.ShouldBeOfType<ChangePasswordDto>();
        }

        [Fact]
        public async Task Post_OldPassword_incorrecto_BadRequest()
        {
            // Arrange
            await GetAuthenticatedClientAsync();
            var userManager = ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var user = await userManager.FindByNameAsync("Admin");
            var data = new ChangePasswordCommand(user.Id, "123456789", "1234567", "1234567");
            var requestContent = SerializerUtils.GetRequestContent(data);

            // Act
            var response = await Client.PostAsync(BaseUrl, requestContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Post_nuevos_passwords_diferentes_BadRequest()
        {
            // Arrange
            await GetAuthenticatedClientAsync();
            var userManager = ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var user = await userManager.FindByNameAsync("Admin");
            var data = new ChangePasswordCommand(user.Id, "123456", "1234567", "1234567999");
            var requestContent = SerializerUtils.GetRequestContent(data);

            // Act
            var response = await Client.PostAsync(BaseUrl, requestContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Post_usuarios_no_existe_NotFound()
        {
            // Arrange
            await GetAuthenticatedClientAsync();
            var data = new ChangePasswordCommand("99999999", "123456", "1234567", "1234567");
            var requestContent = SerializerUtils.GetRequestContent(data);

            // Act
            var response = await Client.PostAsync(BaseUrl, requestContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
