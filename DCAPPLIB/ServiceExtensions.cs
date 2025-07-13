using System.Runtime.Serialization;
using DCAPPLIB.Entities;
using DCAPPLIB.Repositories;
using DCAPPLIB.Repositories.Contracts;
using DCAPPLIB.Services;
using DCAPPLIB.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
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

        services.AddScoped<IAuthService, AuthManager>();

        return services;
    }

    public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
    {
        var builder = services.AddIdentityCore<User>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<RepositoryContext>();

        return services;
    }
}