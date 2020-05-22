using System.Linq;
using System.Threading.Tasks;
using NetClock.Application.Admin.AdminRoles.Queries.GetRoles;
using NetClock.Application.Common.Http;
using NetClock.Application.Common.Utils;
using NetClock.WebApi.IntegrationTests.Helpers;
using Shouldly;
using Xunit;

namespace NetClock.WebApi.IntegrationTests.Controllers.Admin.AdminRolesController
{
    public class GetRolesTest : BaseControllerTest
    {
        public GetRolesTest(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
            BaseUrl = Utilities.ComposeUri("admin/roles");
        }

        [Fact]
        public async Task Get_obtener_lista_roles_disponibles_Ok()
        {
            // Arrange
            await GetAuthenticatedClientAsync();

            // Act
            var response = await Client.GetAsync(BaseUrl);
            var responseContent = await SerializerUtils.GetResponseContentAsync<ResponseData<GetRolesDto>>(response);

            // Assert
            response.EnsureSuccessStatusCode();
            responseContent.Items.Count().ShouldBe(3);
        }

        [Fact]
        public async Task Get_obtener_lista_roles_anonimo_Forbidden()
        {
        }

        [Fact]
        public async Task Get_obtener_lista_roles_requiere_superuser_staff_Forbidden()
        {
        }
    }
}
