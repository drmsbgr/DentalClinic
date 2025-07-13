using DCAPPLIB.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCAPPLIB.Repositories.Configs;

public class DentistConfig : IEntityTypeConfiguration<Dentist>
{
    public void Configure(EntityTypeBuilder<Dentist> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.LastName).IsRequired();
        builder.HasOne(x => x.Clinical).WithMany(x => x.Dentists);
        builder.HasData(
            new Dentist() { Id = 1, FirstName = "Umut Berk", LastName = "Demir", ClinicalId = 3 },
            new Dentist() { Id = 2, FirstName = "Elif Naz", LastName = "Yıldız", ClinicalId = 1 },
            new Dentist() { Id = 3, FirstName = "Mert Can", LastName = "Koç", ClinicalId = 5 },
            new Dentist() { Id = 4, FirstName = "Zeynep", LastName = "Aydın", ClinicalId = 2 },
            new Dentist() { Id = 5, FirstName = "Ali Eren", LastName = "Şahin", ClinicalId = 4 },
            new Dentist() { Id = 6, FirstName = "Ayşe", LastName = "Kara", ClinicalId = 1 },
            new Dentist() { Id = 7, FirstName = "Emirhan", LastName = "Yılmaz", ClinicalId = 5 },
            new Dentist() { Id = 8, FirstName = "Melisa", LastName = "Çelik", ClinicalId = 3 },
            new Dentist() { Id = 9, FirstName = "Berkay", LastName = "Arslan", ClinicalId = 2 },
            new Dentist() { Id = 10, FirstName = "İlayda", LastName = "Öztürk", ClinicalId = 4 },
            new Dentist() { Id = 11, FirstName = "Deniz", LastName = "Aksoy", ClinicalId = 2 },
            new Dentist() { Id = 12, FirstName = "Kaan", LastName = "Bozkurt", ClinicalId = 1 }
            );
    }
}