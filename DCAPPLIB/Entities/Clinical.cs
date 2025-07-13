using System.Text.Json.Serialization;

namespace DCAPPLIB.Entities;

public class Clinical
{
    public int Id { get; set; }
    public string? Name { get; set; }

    [JsonIgnore]
    public ICollection<Customer> Customers { get; } = [];
    [JsonIgnore]
    public ICollection<Dentist> Dentists { get; } = [];
}