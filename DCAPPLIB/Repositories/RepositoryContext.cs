using System.Reflection;
using DCAPPLIB.Entities;
using Microsoft.EntityFrameworkCore;

namespace DCAPPLIB.Repositories;

public class RepositoryContext(DbContextOptions<RepositoryContext> options) : DbContext(options)
{
    public DbSet<Dentist> Dentists { get; set; }
    public DbSet<Clinical> Clinicals { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Appointment> Appointments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}