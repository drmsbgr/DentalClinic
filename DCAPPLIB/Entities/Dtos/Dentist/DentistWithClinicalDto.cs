using DCAPPLIB.Entities.Dtos.Clinic;

namespace DCAPPLIB.Entities.Dtos.Dentist;

public record DentistWithClinicDto
{
    public int Id { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public ClinicDto? Clinic { get; init; }
}