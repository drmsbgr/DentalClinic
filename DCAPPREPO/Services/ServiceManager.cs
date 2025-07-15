using DCAPPREPO.Services.Contracts;

namespace DCAPPREPO.Services;

public class ServiceManager(IDentistService dentistService, IClinicalService clinicalService, ICustomerService customerService) : IServiceManager
{
    private readonly IDentistService _dentistService = dentistService;
    private readonly IClinicalService _clinicalService = clinicalService;
    private readonly ICustomerService _customerService = customerService;
    public IDentistService DentistService => _dentistService;
    public IClinicalService ClinicalService => _clinicalService;
    public ICustomerService CustomerService => _customerService;
}