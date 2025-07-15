using DCAPPLIB.Entities.Dtos.Customer;
using DCAPPLIB.Entities.Dtos.Dentist;

namespace DCAPPLIB.Entities.Dtos.Clinic;

public record ClinicExtendedDto : ClinicDto
{
    public ICollection<BasicDentistDto> Dentists { get; init; } = [];
    //public ICollection<CustomerDto> Customers { get; init; } = [];
}