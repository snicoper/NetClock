using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetClock.Application.Common.Constants;
using NetClock.Application.Common.Utils;
using NetClock.WebApi.IntegrationTests.Helpers;
using Shouldly;
using Xunit;

namespace NetClock.WebApi.IntegrationTests.Controllers.LocalizationController
{
    public class GetSupportedCulturesTest : BaseControllerTest
    {
        public GetSupportedCulturesTest(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
            BaseUrl = Utilities.ComposeUri("localization/supported-cultures");
        }

        [Fact]
        public async Task Get_obtener_lista_culturas_soportadas_Ok()
        {
            // Act
            var response = await Client.GetAsync(BaseUrl);
            var responseContent = await SerializerUtils.GetResponseContentAsync<IEnumerable<string>>(response);

            // Assert
            response.EnsureSuccessStatusCode();
            responseContent.ShouldBe(Cultures.SupportedCultures.Select(c => c.Name));
        }
    }
}
