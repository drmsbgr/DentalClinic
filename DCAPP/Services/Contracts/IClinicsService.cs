using DCAPPLIB.Entities;

namespace DCAPP.Services.Contracts;

public interface IClinicsService
{
    Task<Clinical> GetClinic(int id);
    List<Clinical> GetClinicals();
}
