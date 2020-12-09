using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace NetClock.WebApi.Extensions.ConfigureServices
{
    public static class ConfigureSwaggerExtension
    {
        public static IServiceCollection AddConfigureSwagger(this IServiceCollection services)
        {
            // Register the Swagger.
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

            return services;
        }
    }
}
