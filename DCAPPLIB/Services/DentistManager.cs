using AutoMapper;
using DCAPPLIB.Entities;
using DCAPPLIB.Entities.Dtos.Dentist;
using DCAPPLIB.Repositories.Contracts;
using DCAPPLIB.Services.Contracts;

namespace DCAPPLIB.Services;

public class DentistManager(IRepositoryManager manager, IMapper mapper) : IDentistService
{
    private readonly IRepositoryManager _manager = manager;
    private readonly IMapper _mapper = mapper;

    public void CreateDentist(DentistDtoForInsertion dentistDto)
    {
        Dentist dentist = _mapper.Map<Dentist>(dentistDto);
        _manager.Dentist.CreateDentist(dentist);
        _manager.Save();
    }

    public bool DeleteDentistById(int id)
    {
        var dentist = GetDentistById(id, false);
        if (dentist is not null)
        {
            _manager.Dentist.DeleteDentist(dentist);
            _manager.Save();
            return true;
        }

        return false;
    }

    public IQueryable<Dentist> GetAllDentists(bool trackChanges) => _manager.Dentist.GetAllDentists(trackChanges);

    public DentistDtoForUpdate GetDentistByIdForUpdate(int id, bool trackChanges)
    {
        var dentist = GetDentistById(id, trackChanges);
        var dentistDto = _mapper.Map<DentistDtoForUpdate>(dentist);
        return dentistDto;
    }

    public Dentist? GetDentistById(int id, bool trackChanges)
    {
        var dentist = _manager.Dentist.GetDentistById(id, trackChanges) ?? throw new Exception("Böyle bir diş hekimi bulunamadı!");
        return dentist;
    }

    public void UpdateDentist(DentistDtoForUpdate dentistDto)
    {
        var dentist = _mapper.Map<Dentist>(dentistDto);
        _manager.Dentist.UpdateDentist(dentist);
        _manager.Save();
    }
}