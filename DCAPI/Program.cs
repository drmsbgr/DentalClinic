using System.ComponentModel.DataAnnotations;
using DCAPI;
using DCAPI.Exceptions;
using DCAPI.Models;
using DCAPPLIB;
using DCAPPLIB.Entities.Dtos;
using DCAPPLIB.Entities.Dtos.Clinic;
using DCAPPLIB.Entities.Dtos.Customer;
using DCAPPLIB.Entities.Dtos.Dentist;
using DCAPPLIB.Entities.Dtos.User;
using DCAPPREPO.Repositories;
using DCAPPREPO.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.ConfigureIdentity();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "Dental Clinic API",
        Version = "v1",
        Description = "",
        License = new(),
        Contact = new()
        {
            Email = "drmsbgr@gmail.com",
            Name = "Buğra DURMUŞ",
        }
    });
    c.AddSecurityDefinition("Bearer", new()
    {
        In = ParameterLocation.Header,
        Description = "Lütfen token girin.",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme{
                Reference = new OpenApiReference{
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            []
        }
    });
});

builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDCServices();
builder.Services.AddDbContext<RepositoryContext>(o =>
{
    o.UseSqlite(builder.Configuration.GetConnectionString("sqliteConn"),
     b => b.MigrationsAssembly("DCAPI"));
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        context.Response.ContentType = "application/json";

        var contextFeature = context
            .Features
            .Get<IExceptionHandlerFeature>();

        if (contextFeature is not null)
        {
            context.Response.StatusCode = contextFeature.Error switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                ValidationException => StatusCodes.Status422UnprocessableEntity,
                ArgumentOutOfRangeException => StatusCodes.Status400BadRequest,
                ArgumentException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError,
            };

            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = contextFeature.Error.Message,
            }.ToString()
            );
        }
    });
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/getDentistsByClinic/{id:int}", (int id, IServiceManager serviceManager) =>
{
    return serviceManager.DentistService.GetAllDentists(false).Where(x => x.Clinic!.Id == id);
});

app.MapGet("/api/getClinicById/{id:int}", (int id, IServiceManager serviceManager) =>
{
    return serviceManager.ClinicalService.GetAllClinicals(false).FirstOrDefault(x => x.Id == id);
});

#region DENTISTS

app.MapGet("api/dentists", (IServiceManager manager, int currentPage = 1, int pageSize = 5) =>
{
    if (currentPage < 1) currentPage = 1;
    if (pageSize <= 0) pageSize = 5;

    return manager.DentistService.GetAllDentists(false).OrderBy(x => x.FirstName + " " + x.LastName!.ToUpper()).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
}).WithTags("Dentist CRUD", "GETs");

app.MapGet("api/allDentists", (IServiceManager manager) =>
{
    return manager.DentistService.GetAllDentists(false).OrderBy(x => x.FirstName + " " + x.LastName!.ToUpper()).ToList();
}).WithTags("Dentist CRUD", "GETs");

app.MapGet("api/dentistsCount", (IServiceManager manager) =>
{
    return manager.DentistService.GetAllDentists(false).Count();
}).WithTags("Dentist CRUD", "GETs");

app.MapGet("api/dentists/{id:int}", (int id, IServiceManager manager) =>
{
    var found = manager.DentistService.GetAllDentists(false).FirstOrDefault(x => x.Id == id)
    ?? throw new DentistNotFoundException();
    return found;
}).WithTags("Dentist CRUD", "GETs");

app.MapPost("api/dentists/", [Authorize(Roles = "Admin")] (DentistDtoForInsertion newDentist, IServiceManager manager) =>
{
    var vResults = new List<ValidationResult>();
    var vContext = new ValidationContext(newDentist);
    var isValid = Validator.TryValidateObject(newDentist, vContext, vResults, true);
    if (!isValid)
        return Results.UnprocessableEntity(vResults);

    manager.DentistService.CreateDentist(newDentist);

    return Results.Created($"/api/dentists/{newDentist.Id}", newDentist);
}).WithTags("Dentist CRUD", "POSTs");

app.MapDelete("api/dentists/{id:int}", [Authorize(Roles = "Admin")] (IServiceManager manager, int id) =>
{
    var result = manager.DentistService.DeleteDentistById(id);
    if (result)
        return Results.Ok();
    else
        throw new DentistNotFoundException();
})
.WithTags("Dentist CRUD", "DELETEs")
.RequireAuthorization()
.Produces(StatusCodes.Status200OK)
.Produces<ErrorDetails>(StatusCodes.Status404NotFound);

#endregion

#region CLINICS

app.MapGet("api/clinics", (int currentPage, int pageSize, IServiceManager manager) =>
{
    if (currentPage < 1) currentPage = 1;
    if (pageSize <= 0) pageSize = 5;

    return manager.ClinicalService.GetAllClinicals(false).Include(r => r.Dentists).OrderByDescending(x => x.Dentists.Count).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
})
.WithTags("Clinics CRUD", "GETs");

app.MapGet("api/clinicsCount", (IServiceManager manager) =>
{
    return manager.ClinicalService.GetAllClinicals(false).Count();
})
.WithTags("Clinics CRUD", "GETs");

app.MapGet("api/clinics/{id:int}", (int id, IServiceManager manager) =>
{
    var found = manager.ClinicalService.GetAllClinicals(false).Include(r => r.Dentists).Include(r => r.Customers).FirstOrDefault(x => x.Id == id)
    ?? throw new ClinicNotFoundException();
    return found;
}).WithTags("Clinics CRUD", "GETs");

app.MapPost("api/clinics", [Authorize(Roles = "Admin")] (ClinicDtoForInsertion newClinical, IServiceManager manager) =>
{
    var vResults = new List<ValidationResult>();
    var vContext = new ValidationContext(newClinical);
    var isValid = Validator.TryValidateObject(newClinical, vContext, vResults, true);
    if (!isValid)
        return Results.UnprocessableEntity(vResults);

    manager.ClinicalService.CreateClinical(newClinical);

    return Results.Created($"/api/clinics/{newClinical.Id}", newClinical);
})
.WithTags("Clinics CRUD", "POSTs")
.RequireAuthorization();

app.MapDelete("api/clinics/{id:int}", [Authorize(Roles = "Admin")] (IServiceManager manager, int id) =>
{
    var result = manager.ClinicalService.DeleteClinicalById(id);
    if (result)
        return Results.Ok();
    else
        throw new ClinicNotFoundException();
})
.WithTags("Clinics CRUD", "DELETEs")
.RequireAuthorization()
.Produces(StatusCodes.Status200OK)
.Produces<ErrorDetails>(StatusCodes.Status404NotFound);

#endregion

#region CUSTOMERS

app.MapGet("api/customers", (IServiceManager manager, int currentPage = 1, int pageSize = 5) =>
{
    if (currentPage < 1) currentPage = 1;
    if (pageSize <= 0) pageSize = 5;

    return manager.CustomerService.GetAllCustomers(false).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
})
.WithTags("Customers CRUD", "GETs");

app.MapGet("api/customersCount", (IServiceManager manager) =>
{
    return manager.CustomerService.GetAllCustomers(false).Count();
})
.WithTags("Customers CRUD", "GETs");

app.MapPost("api/customers", [Authorize(Roles = "Admin")] (CustomerDtoForInsertion newCustomer, IServiceManager manager) =>
{
    var vResults = new List<ValidationResult>();
    var vContext = new ValidationContext(newCustomer);
    var isValid = Validator.TryValidateObject(newCustomer, vContext, vResults, true);
    if (!isValid)
        return Results.UnprocessableEntity(vResults);

    manager.CustomerService.CreateCustomer(newCustomer);

    return Results.Created($"/api/customers/{newCustomer.Id}", newCustomer);
})
.WithTags("Customers CRUD", "POSTs")
.RequireAuthorization();

app.MapDelete("api/customers/{id:int}", [Authorize(Roles = "Admin")] (IServiceManager manager, int id) =>
{
    var result = manager.CustomerService.DeleteCustomerById(id);
    if (result)
        return Results.Ok();
    else
        throw new CustomerNotFoundException();
})
.WithTags("Customers CRUD", "DELETEs")
.RequireAuthorization()
.Produces(StatusCodes.Status200OK)
.Produces<ErrorDetails>(StatusCodes.Status404NotFound);

#endregion

#region AUTH

app.MapPost("/api/auth", async (UserDtoForRegistration userDto, IAuthService authService) =>
{
    var result = await authService.RegisterUserAsync(userDto);

    return result.Succeeded
    ? Results.Ok(result)
    : Results.BadRequest(result.Errors);
})
.Produces<IdentityResult>(StatusCodes.Status200OK)
.Produces<ErrorDetails>(StatusCodes.Status400BadRequest)
.WithTags("Auth");

app.MapPost("/api/login", async (UserDtoForAuth userDto,
    IAuthService authService) =>
{
    if (!await authService.ValidateUserAsync(userDto))
        return Results.Unauthorized(); // 401
    return Results.Ok(new
    {
        Token = await authService.CreateTokenAsync(true)
    });
})
.Produces(StatusCodes.Status200OK)
.Produces<ErrorDetails>(StatusCodes.Status401Unauthorized)
.WithTags("Auth");

app.MapPost("/api/refresh", async (TokenDto tokenDto,
    IAuthService authService) =>
{
    var tokenDtoToReturn = await authService
        .RefreshTokenAsync(tokenDto);

    return Results.Ok(tokenDtoToReturn);
})
.Produces(StatusCodes.Status200OK)
.WithTags("Auth");

#endregion

app.Run();