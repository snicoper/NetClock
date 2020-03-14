using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Hosting;

namespace NetClock.WebApi.Extensions.Configure
{
    public static class ConfigureByEnvironmentExtension
    {
        private static IApplicationBuilder _application;

        public static void ConfigureByEnvironment(this IApplicationBuilder app, IHostEnvironment environment)
        {
            _application = app;
            var hasConfigure = false;

            if (environment.IsProduction())
            {
                hasConfigure = true;
                ConfigureProduction();
            }

            if (environment.IsStaging())
            {
                hasConfigure = true;
                ConfigureStaging();
            }

            if (environment.IsDevelopment())
            {
                hasConfigure = true;
                ConfigureDevelopment();
            }

            if (environment.IsEnvironment("Test"))
            {
                hasConfigure = true;
                ConfigureTest();
            }

            if (hasConfigure is false)
            {
                throw new NotImplementedException();
            }
        }

        private static void ConfigureProduction()
        {
            _application.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            _application.UseHsts();
        }

        private static void ConfigureStaging()
        {
            ConfigureProduction();
        }

        private static void ConfigureDevelopment()
        {
            _application.UseDeveloperExceptionPage();
            _application.UseDatabaseErrorPage();
        }

        private static void ConfigureTest()
        {
            ConfigureDevelopment();
        }
    }
}
