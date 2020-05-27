using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NetClock.Application.Accounts.Accounts.Commands.ChangeEmail;
using NetClock.Application.Common.Utils;
using NetClock.Domain.Entities.Identity;
using NetClock.WebApi.IntegrationTests.Helpers;
using Shouldly;
using Xunit;

namespace NetClock.WebApi.IntegrationTests.Controllers.Accounts.AccountsController
{
    public class ChangeEmailTest : BaseControllerTest
    {
        public ChangeEmailTest(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
            BaseUrl = Utilities.ComposeUri("accounts/change-email");
        }

        [Fact]
        public async Task Post_cambiar_email_datos_correctos_Ok()
        {
            // Arrange
            await GetAuthenticatedClientAsync();
            var userManager = Factory.Services.GetRequiredService<UserManager<ApplicationUser>>();
            var user = await userManager.FindByNameAsync("Admin");
            var data = new ChangeEmailCommand(user.Id, "nuevo@example.com");
            var requestContent = SerializerUtils.GetRequestContent(data);

            // Act
            var response = await Client.PostAsync(BaseUrl, requestContent);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Post_id_usuario_no_existe_NotFound()
        {
            // Arrange
            await GetAuthenticatedClientAsync();
            var userManager = Factory.Services.GetRequiredService<UserManager<ApplicationUser>>();
            var user = await userManager.FindByNameAsync("Admin");
            var data = new ChangeEmailCommand("123123213", user.Email);
            var requestContent = SerializerUtils.GetRequestContent(data);

            // Act
            var response = await Client.PostAsync(BaseUrl, requestContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData("email@mal")]
        [InlineData("email")]
        public async Task Post_email_mal_formado_BadRequest(string email)
        {
            // Arrange
            await GetAuthenticatedClientAsync();
            var userManager = Factory.Services.GetRequiredService<UserManager<ApplicationUser>>();
            var user = await userManager.FindByNameAsync("Admin");
            var data = new ChangeEmailCommand(user.Id, email);
            var requestContent = SerializerUtils.GetRequestContent(data);

            // Act
            var response = await Client.PostAsync(BaseUrl, requestContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
    }
}
