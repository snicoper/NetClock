using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using NetClock.Application.Common.Constants;

namespace NetClock.WebApi.Extensions.Configure
{
    public static class ConfigureCultureExtension
    {
        public static void UseConfigureCulture(this IApplicationBuilder app)
        {
            // Localization.
            // @see: https://docs.microsoft.com/es-es/openspecs/windows_protocols/ms-lcid/a9eac961-e77d-41a6-90a5-ce1a8b0cdb9c
            var localizationOptions = new RequestLocalizationOptions
            {
                SupportedCultures = Cultures.Supported,
                SupportedUICultures = Cultures.Supported,
                DefaultRequestCulture = new RequestCulture(Cultures.Default)
            };

            app.UseRequestLocalization(localizationOptions);
        }
    }
}
