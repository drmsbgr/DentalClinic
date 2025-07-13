namespace DCAPPLIB.Entities.Dtos.Dentist;

public record DentistDto
{
    public int Id { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public int ClinicalId { get; init; }
}