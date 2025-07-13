using DCAPPLIB.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCAPPLIB.Repositories.Configs;

public class CustomerConfig : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.LastName).IsRequired();
        builder.Property(x => x.Email).IsRequired();

        builder.HasMany(x => x.Appointments).WithOne(x => x.Customer);
        builder.HasMany(x => x.Clinicals).WithMany(x => x.Customers);

        builder.HasData(
            new Customer { Id = 1, FirstName = "Ali", LastName = "Yılmaz", Email = "ali1@example.com" },
            new Customer { Id = 2, FirstName = "Veli", LastName = "Kara", Email = "veli2@example.com" },
            new Customer { Id = 3, FirstName = "Ayşe", LastName = "Demir", Email = "ayse3@example.com" },
            new Customer { Id = 4, FirstName = "Fatma", LastName = "Çelik", Email = "fatma4@example.com" },
            new Customer { Id = 5, FirstName = "Mehmet", LastName = "Ak", Email = "mehmet5@example.com" },
            new Customer { Id = 6, FirstName = "Can", LastName = "Şahin", Email = "can6@example.com" },
            new Customer { Id = 7, FirstName = "Zeynep", LastName = "Koç", Email = "zeynep7@example.com" },
            new Customer { Id = 8, FirstName = "Mert", LastName = "Uçar", Email = "mert8@example.com" },
            new Customer { Id = 9, FirstName = "Elif", LastName = "Güneş", Email = "elif9@example.com" },
            new Customer { Id = 10, FirstName = "Kemal", LastName = "Bulut", Email = "kemal10@example.com" },
            new Customer { Id = 11, FirstName = "Hakan", LastName = "Aydın", Email = "hakan11@example.com" },
            new Customer { Id = 12, FirstName = "Seda", LastName = "Yıldız", Email = "seda12@example.com" },
            new Customer { Id = 13, FirstName = "Ahmet", LastName = "Bozkurt", Email = "ahmet13@example.com" },
            new Customer { Id = 14, FirstName = "Nur", LastName = "Kurt", Email = "nur14@example.com" },
            new Customer { Id = 15, FirstName = "Burak", LastName = "Deniz", Email = "burak15@example.com" }
        );
    }
}