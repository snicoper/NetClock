using System.Net.Http.Headers;
using System.Threading.Tasks;
using NetClock.Application.Common.Constants;
using NetClock.Application.Common.Utils;
using NetClock.WebApi.IntegrationTests.Helpers;
using Shouldly;
using Xunit;

namespace NetClock.WebApi.IntegrationTests.Controllers.LocalizationController
{
    public class GetCurrentCultureTest : BaseControllerTest
    {
        public GetCurrentCultureTest(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
            BaseUrl = Utilities.ComposeUri("localization/current");
        }

        [Fact]
        public async Task Get_culture_actual_Ok()
        {
            // Arrange
            Client.DefaultRequestHeaders.Clear();
            Client.DefaultRequestHeaders.Add("Accept-Language", Cultures.DefaultCulture.Name);
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Act
            var response = await Client.GetAsync(BaseUrl);
            var responseContent = await SerializerUtils.GetResponseContentAsync<string>(response);

            // Assert
            response.EnsureSuccessStatusCode();
            responseContent.ShouldBe(Cultures.DefaultCulture.Name);
        }
    }
}
