using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using WingsOn.Domain.Interfaces;
using WingsOn.Domain.Services;

namespace WingsOn.Domain.Configuration;

[ExcludeFromCodeCoverage(Justification = "Configuration settings")]
public static class ConfigureServices
{
    public static IServiceCollection ConfigureDomain(this IServiceCollection services)
    {
        services.AddTransient<IPersonService, PersonService>();
        services.AddTransient<IFlightService, FlightService>();

        return services;
    }
}