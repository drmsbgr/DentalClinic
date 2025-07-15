using DCAPPLIB.Entities;

namespace DCAPPREPO.Repositories.Contracts;

public interface IClinicalRepository : IRepositoryBase<Clinical>
{
    IQueryable<Clinical> GetAllClinicals(bool trackChanges);
    Clinical? GetClinicalById(int id, bool trackChanges);
    void CreateClinical(Clinical clinical);
    void DeleteClinical(Clinical clinical);
    void UpdateClinical(Clinical clinical);
}