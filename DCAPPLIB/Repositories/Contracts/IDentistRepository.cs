using DCAPPLIB.Entities;

namespace DCAPPLIB.Repositories.Contracts;

public interface IDentistRepository : IRepositoryBase<Dentist>
{
    IQueryable<Dentist> GetAllDentists(bool trackChanges);
    Dentist? GetDentistById(int id, bool trackChanges);
    void CreateDentist(Dentist dentist);
    void DeleteDentist(Dentist dentist);
    void UpdateDentist(Dentist dentist);
}
