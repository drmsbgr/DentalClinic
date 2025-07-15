using DCAPPLIB.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCAPPREPO.Repositories.Configs;

public class ClinicalConfig : IEntityTypeConfiguration<Clinical>
{
    public void Configure(EntityTypeBuilder<Clinical> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired();
        builder.HasMany(x => x.Dentists).WithOne(x => x.Clinical);
        builder.HasMany(x => x.Customers).WithMany(x => x.Clinicals);

        builder.HasData(
            new Clinical() { Id = 1, Name = "Klinik 1" },
            new Clinical() { Id = 2, Name = "Klinik 2" },
            new Clinical() { Id = 3, Name = "Klinik 3" },
            new Clinical() { Id = 4, Name = "Klinik A1" },
            new Clinical() { Id = 5, Name = "Klinik A2" },
            new Clinical() { Id = 6, Name = "Klinik B1" },
            new Clinical() { Id = 7, Name = "Klinik B2" },
            new Clinical() { Id = 8, Name = "Klinik C1" },
            new Clinical() { Id = 9, Name = "Klinik C2" }
        );
    }
}