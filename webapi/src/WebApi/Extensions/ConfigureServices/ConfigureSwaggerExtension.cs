using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace NetClock.WebApi.Extensions.ConfigureServices
{
    public static class ConfigureSwaggerExtension
    {
        public static IServiceCollection ConfigureSwagger(
            this IServiceCollection services,
            IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                // Register the Swagger services.
                services.AddOpenApiDocument(configure =>
                {
                    configure.Title = "NetClock API";
                    configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                    {
                        Type = OpenApiSecuritySchemeType.ApiKey,
                        Name = "Authorization",
                        In = OpenApiSecurityApiKeyLocation.Header,
                        Description = "Type into the text box: Bearer {your JWT token}."
                    });

                    configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
                });
            }

            return services;
        }
    }
}
