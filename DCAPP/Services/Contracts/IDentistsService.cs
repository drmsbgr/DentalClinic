using DCAPPLIB.Entities.Dtos.Dentist;

namespace DCAPP.Services.Contracts;

public interface IDentistsService
{
    Task<DentistWithClinicDto> GetDentist(int id);
    List<DentistWithClinicDto> GetDentists();
}