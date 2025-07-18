namespace DCAPPLIB.Entities.Dtos.Customer;

public record CustomerDto
{
    public int Id { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Email { get; init; }
}