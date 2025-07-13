namespace DCAPPLIB.Services.Contracts;

public interface IServiceManager
{
    IDentistService DentistService { get; }
    IClinicalService ClinicalService { get; }
    ICustomerService CustomerService { get; }
}