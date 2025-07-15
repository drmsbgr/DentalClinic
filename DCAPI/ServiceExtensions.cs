using System.Text;
using DCAPPLIB.Entities;
using DCAPPREPO.Repositories;
using DCAPPREPO.Repositories.Contracts;
using DCAPPREPO.Services;
using DCAPPREPO.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace DCAPI;

public static class ServiceExtensions
{
    public static void ConfigureJwt(this IServiceCollection services, IConfiguration config)
    {
        var jwtSettings = config.GetSection("JwtSettings");
        var secretKey = jwtSettings["secretKey"];
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["validIssuer"],
                ValidAudience = jwtSettings["validAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
            };
        });
    }

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