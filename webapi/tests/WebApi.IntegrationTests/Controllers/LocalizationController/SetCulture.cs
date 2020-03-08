using System.Net;
using System.Threading.Tasks;
using NetClock.Application.Localization.Commands.SetCulture;
using NetClock.WebApi.IntegrationTests.Helpers;
using Shouldly;
using Xunit;

namespace NetClock.WebApi.IntegrationTests.Controllers.LocalizationController
{
    public class SetCulture : BaseControllerTest
    {
        public SetCulture(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
            BaseUrl = Utilities.ComposeUri("localization");
        }

        [Fact]
        public async Task Post_cambiar_culture_usuario_anonimo_204Ok()
        {
            // Arrange
            var data = new SetCultureCommand("es-ES");
            var requestContent = Utilities.GetRequestContent(data);

            // Act
            var response = await Client.PostAsync(BaseUrl, requestContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Theory]
        [InlineData("es-ES")]
        [InlineData("ca-ES")]
        [InlineData("en-GB")]
        public async Task Post_cambiar_culture_codigos_validos_204Ok(string culture)
        {
            // Arrange
            var data = new SetCultureCommand(culture);
            var requestContent = Utilities.GetRequestContent(data);

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
        public async Task Post_cambiar_culture_codigos_invalidos_400BadRequest(string culture)
        {
            // Arrange
            var data = new SetCultureCommand(culture);
            var requestContent = Utilities.GetRequestContent(data);

            // Act
            var response = await Client.PostAsync(BaseUrl, requestContent);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
    }
}
