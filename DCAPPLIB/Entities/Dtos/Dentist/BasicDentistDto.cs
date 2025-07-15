namespace DCAPPLIB.Entities.Dtos.Dentist;

public record BasicDentistDto
{
    public int Id { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
}
