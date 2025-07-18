namespace DCAPPLIB.Entities;

public class Customer
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }

    public ICollection<Appointment>? Appointments { get; } = [];
    public ICollection<Clinical> Clinicals { get; } = [];
}