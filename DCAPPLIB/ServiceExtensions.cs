using DCAPPLIB.Repositories;
using DCAPPLIB.Repositories.Contracts;
using DCAPPLIB.Services;
using DCAPPLIB.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace DCAPPLIB;

public static class ServiceExtensions
{
    public static IServiceCollection AddDCServices(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryManager, RepositoryManager>();
        services.AddScoped<IDentistRepository, DentistRepository>();
        services.AddScoped<IClinicalRepository, ClinicalRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        services.AddScoped<IServiceManager, ServiceManager>();
        services.AddScoped<IDentistService, DentistManager>();
        services.AddScoped<IClinicalService, ClinicalManager>();
        services.AddScoped<ICustomerService, CustomerManager>();

        return services;
    }
}