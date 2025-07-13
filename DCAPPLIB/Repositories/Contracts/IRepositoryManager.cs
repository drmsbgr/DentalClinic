namespace DCAPPLIB.Repositories.Contracts;

public interface IRepositoryManager
{
    IDentistRepository Dentist { get; }
    IClinicalRepository Clinical { get; }
    ICustomerRepository Customer { get; }
    //IAppointmentRepository Appointment { get; }
    void Save();
}
