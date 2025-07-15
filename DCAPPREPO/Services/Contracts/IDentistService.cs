using DCAPPLIB.Entities;
using DCAPPLIB.Entities.Dtos.Dentist;

namespace DCAPPREPO.Services.Contracts;

public interface IDentistService
{
    IQueryable<DentistWithClinicDto> GetAllDentists(bool trackChanges);
    Dentist? GetDentistById(int id, bool trackChanges);
    void CreateDentist(DentistDtoForInsertion dentist);
    void UpdateDentist(DentistDtoForUpdate dentist);
    bool DeleteDentistById(int id);
}