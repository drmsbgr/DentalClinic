using System.Reflection;
using DCAPPLIB.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DCAPPLIB.Repositories;

public class RepositoryContext(DbContextOptions<RepositoryContext> options) : IdentityDbContext<User>(options)
{
    public DbSet<Dentist> Dentists { get; set; }
    public DbSet<Clinical> Clinicals { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Appointment> Appointments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = "2726d97d-2ae1-4706-b841-91444c0f51a7",
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Id = "57aa6858-cb26-4153-9450-f3fd4b6eb8bf",
                Name = "Customer",
                NormalizedName = "CUSTOMER"
            }
        );
    }
}