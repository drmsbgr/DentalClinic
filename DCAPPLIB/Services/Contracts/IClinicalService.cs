using DCAPPLIB.Entities;
using DCAPPLIB.Entities.Dtos.Clinical;

namespace DCAPPLIB.Services.Contracts;

public interface IClinicalService
{
    IQueryable<Clinical> GetAllClinicals(bool trackChanges);
    Clinical? GetClinicalById(int id, bool trackChanges);
    void CreateClinical(ClinicalDtoForInsertion clinical);
    void UpdateClinical(ClinicalDtoForUpdate clinical);
    void DeleteClinicalById(int id);
}
