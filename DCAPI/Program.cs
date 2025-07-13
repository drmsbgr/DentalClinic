using System.ComponentModel.DataAnnotations;
using DCAPI.Exceptions;
using DCAPI.Models;
using DCAPPLIB;
using DCAPPLIB.Entities.Dtos.Clinical;
using DCAPPLIB.Entities.Dtos.Dentist;
using DCAPPLIB.Repositories;
using DCAPPLIB.Services.Contracts;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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

#region DENISTS

app.MapGet("api/dentists", (IServiceManager manager, int currentPage = 1, int pageSize = 5) =>
{
    if (currentPage < 1) currentPage = 1;
    if (pageSize < 5) pageSize = 5;

    return manager.DentistService.GetAllDentists(false).Include(r => r.Clinical).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
}).WithTags("Dentist CRUD");

app.MapGet("api/dentists/{id:int}", (int id, IServiceManager manager) =>
{
    var found = manager.DentistService.GetAllDentists(false).Include(r => r.Clinical).FirstOrDefault(x => x.Id == id)
    ?? throw new DentistNotFoundException();
    return found;
}).WithTags("Dentist CRUD");

app.MapPost("api/dentists/", (DentistDtoForInsertion newDentist, IServiceManager manager) =>
{
    var vResults = new List<ValidationResult>();
    var vContext = new ValidationContext(newDentist);
    var isValid = Validator.TryValidateObject(newDentist, vContext, vResults, true);
    if (!isValid)
        return Results.UnprocessableEntity(vResults);

    manager.DentistService.CreateDentist(newDentist);

    return Results.Created($"/api/dentists/{newDentist.Id}", newDentist);
}).WithTags("Dentist CRUD");

#endregion

#region Clinics

app.MapGet("api/clinics", (int currentPage, int pageSize, IServiceManager manager) =>
{
    if (currentPage < 1) currentPage = 1;
    if (pageSize < 5) pageSize = 5;

    return manager.ClinicalService.GetAllClinicals(false).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
}).WithTags("Clinics CRUD");

app.MapPost("api/clinics", (ClinicalDtoForInsertion newClinical, IServiceManager manager) =>
{
    var vResults = new List<ValidationResult>();
    var vContext = new ValidationContext(newClinical);
    var isValid = Validator.TryValidateObject(newClinical, vContext, vResults, true);
    if (!isValid)
        return Results.UnprocessableEntity(vResults);

    manager.ClinicalService.CreateClinical(newClinical);

    return Results.Created($"/api/dentists/{newClinical.Id}", newClinical);
}).WithTags("Clinics CRUD");

#endregion

app.MapGet("api/customers", (int currentPage, int pageSize, IServiceManager manager) =>
{
    if (currentPage < 1) currentPage = 1;
    if (pageSize < 5) pageSize = 5;

    return manager.CustomerService.GetAllCustomers(false).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
}).WithTags("Customers CRUD");

app.Run();