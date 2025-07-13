using DCAPPLIB.Entities;
using DCAPPLIB.Repositories.Contracts;

namespace DCAPPLIB.Repositories;

public class DentistRepository(RepositoryContext context) : RepositoryBase<Dentist>(context), IDentistRepository
{
    public void CreateDentist(Dentist dentist) => Create(dentist);
    public void DeleteDentist(Dentist dentist) => Delete(dentist);
    public void UpdateDentist(Dentist dentist) => Update(dentist);
    public IQueryable<Dentist> GetAllDentists(bool trackChanges) => GetAll(trackChanges);
    public Dentist? GetDentistById(int id, bool trackChanges) => FindByCondition(x => x.Id == id, trackChanges);
}