namespace DCAPPLIB.Entities;

public class Dentist
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public int ClinicalId { get; set; }
    public Clinical? Clinical { get; } = null;
}