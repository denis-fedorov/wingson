using Microsoft.OpenApi.Models;

namespace WingsOn.WebApi.Configuration;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureWebApiServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "WingsOn API",
                Description = "WingsOn .NET Core WebAPI"
            });
            
            options.EnableAnnotations();
        });

        return services;
    }
}