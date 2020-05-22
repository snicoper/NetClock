using System.Globalization;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using NetClock.Application.Common.Localizations;
using NetClock.Infrastructure.Persistence;
using Newtonsoft.Json;

namespace NetClock.WebApi.Extensions.ConfigureServices
{
    public static class ConfigureApiControllerExtension
    {
        public static IServiceCollection ConfigureApiControllers(this IServiceCollection services)
        {
            services
                .AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Culture = CultureInfo.CurrentCulture;
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd'T'HH:mm:ss.FFFFFF'Z'";
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                })
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ApplicationDbContext>())
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory)
                        => factory.Create(typeof(SharedLocalizer));
                })
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

            return services;
        }
    }
}
