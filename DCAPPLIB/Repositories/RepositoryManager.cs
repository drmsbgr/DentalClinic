using DCAPPLIB.Repositories.Contracts;

namespace DCAPPLIB.Repositories;

public class RepositoryManager(
    RepositoryContext context,
    IDentistRepository dentistRepository,
    IClinicalRepository clinicalRepository,
    ICustomerRepository customerRepository
    ) : IRepositoryManager
{
    private readonly RepositoryContext _context = context;
    private readonly IDentistRepository _dentistRepository = dentistRepository;
    private readonly IClinicalRepository _clinicalRepository = clinicalRepository;
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public IDentistRepository Dentist => _dentistRepository;
    public IClinicalRepository Clinical => _clinicalRepository;
    public ICustomerRepository Customer => _customerRepository;

    public void Save()
    {
        _context.SaveChanges();
    }
}