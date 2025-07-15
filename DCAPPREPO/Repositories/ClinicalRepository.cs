using DCAPPLIB.Entities;
using DCAPPREPO.Repositories.Contracts;

namespace DCAPPREPO.Repositories;

public class ClinicalRepository(RepositoryContext context) : RepositoryBase<Clinical>(context), IClinicalRepository
{
    public void CreateClinical(Clinical clinical) => Create(clinical);
    public void DeleteClinical(Clinical clinical) => Delete(clinical);
    public void UpdateClinical(Clinical clinical) => Update(clinical);
    public IQueryable<Clinical> GetAllClinicals(bool trackChanges) => GetAll(trackChanges);
    public Clinical? GetClinicalById(int id, bool trackChanges) => FindByCondition(x => x.Id == id, trackChanges);
}