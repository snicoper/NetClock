using System.Net;
using System.Threading.Tasks;
using NetClock.Application.Common.Utils;
using NetClock.Application.Localization.Commands.SetCulture;
using NetClock.WebApi.IntegrationTests.Helpers;
using Shouldly;
using Xunit;

namespace NetClock.WebApi.IntegrationTests.Controllers.LocalizationController
{
    public class SetCultureTest : BaseControllerTest
    {
        public SetCultureTest(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
            BaseUrl = Utilities.ComposeUri("localization");
        }

        [Fact]
        public async Task Post_cambiar_culture_usuario_anonimo_Ok()
        {
            // Arrange
            var data = new SetCultureCommand("es-ES");
            var requestContent = SerializerUtils.GetRequestContent(data);

            // Act
            var response = await Client.PostAsync(BaseUrl, requestContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Theory]
        [InlineData("es-ES")]
        [InlineData("ca-ES")]
        [InlineData("en-GB")]
        public async Task Post_cambiar_culture_codigos_validos_Ok(string culture)
        {
            // Arrange
            var data = new SetCultureCommand(culture);
            var requestContent = SerializerUtils.GetRequestContent(data);

            // Act
            var response = await Client.PostAsync(BaseUrl, requestContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Theory]
        [InlineData("es")]
        [InlineData("ES")]
        [InlineData("en")]
        [InlineData("123")]
        public async Task Post_cambiar_culture_codigos_invalidos_BadRequest(string culture)
        {
            // Arrange
            var data = new SetCultureCommand(culture);
            var requestContent = SerializerUtils.GetRequestContent(data);

            // Act
            var response = await Client.PostAsync(BaseUrl, requestContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
    }
}
