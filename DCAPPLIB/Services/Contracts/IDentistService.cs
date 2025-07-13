using DCAPPLIB.Entities;
using DCAPPLIB.Entities.Dtos.Dentist;

namespace DCAPPLIB.Services.Contracts;

public interface IDentistService
{
    IQueryable<Dentist> GetAllDentists(bool trackChanges);
    Dentist? GetDentistById(int id, bool trackChanges);
    void CreateDentist(DentistDtoForInsertion dentist);
    void UpdateDentist(DentistDtoForUpdate dentist);
    void DeleteDentistById(int id);
}