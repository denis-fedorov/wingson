using Microsoft.Extensions.DependencyInjection;
using WingsOn.Dal.Repositories;
using WingsOn.Domain.Entities;
using WingsOn.Domain.Interfaces;

namespace WingsOn.Dal.Configuration;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureDal(this IServiceCollection services)
    {
        services.AddSingleton<IRepository<Airline>, AirlineRepository>();
        services.AddSingleton<IRepository<Airport>, AirportRepository>();
        services.AddSingleton<IRepository<Booking>, BookingRepository>();
        services.AddSingleton<IRepository<Flight>, FlightRepository>();
        services.AddSingleton<IRepository<Person>, PersonRepository>();

        return services;
    }
}