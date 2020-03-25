using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NetClock.Application.Accounts.Auth.Commands.RecoveryPasswordValidate;
using NetClock.Application.Common.Utils;
using NetClock.Domain.Entities.Identity;
using NetClock.WebApi.IntegrationTests.Helpers;
using Shouldly;
using Xunit;

namespace NetClock.WebApi.IntegrationTests.Controllers.Accounts.AuthController
{
    public class RecoveryPasswordValidateTest : BaseControllerTest
    {
        public RecoveryPasswordValidateTest(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
            BaseUrl = Utilities.ComposeUri("auth/recovery-password/validate");
        }

        [Fact]
        public async Task Post_recuperar_contrasena_valida_Ok()
        {
            // Arrange
            var userManager = ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var user = await userManager.FindByNameAsync("Admin");
            var code = await userManager.GeneratePasswordResetTokenAsync(user);
            var data = new RecoveryPasswordValidateCommand(user.Id, code, "1234567", "1234567");
            var requestContent = SerializerUtils.GetRequestContent(data);

            // Act
            var response = await Client.PostAsync(BaseUrl, requestContent);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Post_password_diferentes_BadRequest()
        {
            // Arrange
            var userManager = ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var user = await userManager.FindByNameAsync("Admin");
            var code = await userManager.GeneratePasswordResetTokenAsync(user);
            var data = new RecoveryPasswordValidateCommand(user.Id, code, "123", "1234");
            var requestContent = SerializerUtils.GetRequestContent(data);

            // Act
            var response = await Client.PostAsync(BaseUrl, requestContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Post_usuario_no_existe_NotFound()
        {
            // Arrange
            var userManager = ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var user = await userManager.FindByNameAsync("Admin");
            var code = await userManager.GeneratePasswordResetTokenAsync(user);
            var data = new RecoveryPasswordValidateCommand("123123123", code, "123456", "123456");
            var requestContent = SerializerUtils.GetRequestContent(data);

            // Act
            var response = await Client.PostAsync(BaseUrl, requestContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
