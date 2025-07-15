namespace DCAPPLIB.Entities.Dtos.Dentist;

public record DentistDto : BasicDentistDto
{
    public int ClinicalId { get; init; }
}