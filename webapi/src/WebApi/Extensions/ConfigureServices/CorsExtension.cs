using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NetClock.WebApi.Extensions.ConfigureServices
{
    public static class CorsExtension
    {
        private static IServiceCollection _services;
        private static string _corsName;

        public static IServiceCollection AddDefaultCors(
            this IServiceCollection services,
            IWebHostEnvironment environment,
            string corsName)
        {
            _services = services;
            _corsName = corsName;

            if (environment.IsProduction())
            {
                return ConfigureCorsProduction();
            }

            if (environment.IsStaging())
            {
                return ConfigureCorsStaging();
            }

            if (environment.IsDevelopment())
            {
                return ConfigureCorsDevelopment();
            }

            if (environment.IsEnvironment("Test"))
            {
                return ConfigureCorsTest();
            }

            throw new NotImplementedException();
        }

        private static IServiceCollection ConfigureCorsProduction()
        {
            _services.AddCors(options =>
            {
                options.AddPolicy(_corsName, builder =>
                {
                    builder
                        .WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            return _services;
        }

        private static IServiceCollection ConfigureCorsStaging()
        {
            _services.AddCors(options =>
            {
                options.AddPolicy(_corsName, builder =>
                {
                    builder
                        .WithOrigins("http://localhost:4210")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            return _services;
        }

        private static IServiceCollection ConfigureCorsDevelopment()
        {
            _services.AddCors(options =>
            {
                options.AddPolicy(_corsName, builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            return _services;
        }

        private static IServiceCollection ConfigureCorsTest()
        {
            return ConfigureCorsDevelopment();
        }
    }
}
