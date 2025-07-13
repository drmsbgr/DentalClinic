using DCAPPLIB.Entities;
using DCAPPLIB.Entities.Dtos.Clinic;

namespace DCAPPLIB.Services.Contracts;

public interface IClinicalService
{
    IQueryable<Clinical> GetAllClinicals(bool trackChanges);
    Clinical? GetClinicalById(int id, bool trackChanges);
    void CreateClinical(ClinicDtoForInsertion clinical);
    void UpdateClinical(ClinicDtoForUpdate clinical);
    bool DeleteClinicalById(int id);
}
