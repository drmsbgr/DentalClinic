using System.Text.Json;

namespace DCAPI.Models;

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public string? AtOccured => DateTime.Now.ToLongDateString();

    public override string ToString() => JsonSerializer.Serialize(this);
}