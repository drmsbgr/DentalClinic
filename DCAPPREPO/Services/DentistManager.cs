using AutoMapper;
using AutoMapper.QueryableExtensions;
using DCAPPLIB.Entities;
using DCAPPLIB.Entities.Dtos.Dentist;
using DCAPPREPO.Repositories.Contracts;
using DCAPPREPO.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DCAPPREPO.Services;

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

    public IQueryable<DentistWithClinicDto> GetAllDentists(bool trackChanges)
    {
        var dentists = _manager.Dentist.GetAllDentists(trackChanges).Include(r => r.Clinical);
        return dentists.ProjectTo<DentistWithClinicDto>(_mapper.ConfigurationProvider);
    }

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