using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Hosting;
using NetClock.Application.Common.Constants;

namespace NetClock.WebApi.Extensions.Configure
{
    public static class ConfigureByEnvironmentExtension
    {
        private static IApplicationBuilder _app;

        public static void UseConfigureByEnvironment(this IApplicationBuilder app, IHostEnvironment environment)
        {
            _app = app;

            if (environment.IsProduction())
            {
                ConfigureProduction();

                return;
            }

            if (environment.IsStaging())
            {
                ConfigureStaging();

                return;
            }

            if (environment.IsDevelopment())
            {
                ConfigureDevelopment();

                return;
            }

            if (!environment.IsEnvironment(CommonConstants.Test))
            {
                throw new NotImplementedException();
            }

            ConfigureTest();
        }

        private static void ConfigureProduction()
        {
            _app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            _app.UseHsts();
        }

        private static void ConfigureStaging()
        {
            ConfigureProduction();
        }

        private static void ConfigureDevelopment()
        {
            _app.UseOpenApi();
            _app.UseSwaggerUi3(settings => { settings.Path = string.Empty; });
            _app.UseReDoc(settings => { settings.Path = "/docs"; });
            _app.UseDeveloperExceptionPage();
        }

        private static void ConfigureTest()
        {
            ConfigureDevelopment();
        }
    }
}
