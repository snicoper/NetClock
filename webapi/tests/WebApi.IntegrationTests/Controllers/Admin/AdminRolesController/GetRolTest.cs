using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NetClock.Application.Admin.AdminRoles.Queries.GetRol;
using NetClock.Application.Common.Authorization;
using NetClock.Application.Common.Utils;
using NetClock.WebApi.IntegrationTests.Helpers;
using Shouldly;
using Xunit;

namespace NetClock.WebApi.IntegrationTests.Controllers.Admin.AdminRolesController
{
    public class GetRolTest : BaseControllerTest
    {
        private readonly IdentityRole _rol;

        public GetRolTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
            var roleManager = ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            _rol = roleManager.FindByNameAsync(AppRoles.Superuser).GetAwaiter().GetResult();
            BaseUrl = Utilities.ComposeUri($"admin/roles/{_rol.Id}");
        }

        [Fact]
        public async Task Get_role_Ok()
        {
            // Arrange
            await GetAuthenticatedClientAsync();

            // Act
            var response = await Client.GetAsync(BaseUrl);
            var responseContent = await SerializerUtils.GetResponseContentAsync<GetRolDto>(response);

            // Assert
            response.EnsureSuccessStatusCode();
            responseContent.Name.ShouldBe(_rol.Name);
        }

        [Fact]
        public async Task Get_role_require_superuser_or_staff_Forbidden()
        {
            // Arrange
            await GetAuthenticatedClientAsync("Bob", "123456");

            // Act
            var response = await Client.GetAsync(BaseUrl);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task Get_role_require_anonymous_Unauthorized()
        {
            // Act
            var response = await Client.GetAsync(BaseUrl);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        }
    }
}
