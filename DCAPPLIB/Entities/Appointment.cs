namespace DCAPPLIB.Entities;

public class Appointment
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int ClinicalId { get; set; }
    public DateTime DateTime { get; set; }

    public Clinical? Clinical { get; } = null;
    public Customer? Customer { get; } = null;
}