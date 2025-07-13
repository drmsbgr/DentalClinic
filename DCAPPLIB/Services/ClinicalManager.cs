using AutoMapper;
using DCAPPLIB.Entities;
using DCAPPLIB.Entities.Dtos.Clinical;
using DCAPPLIB.Repositories.Contracts;
using DCAPPLIB.Services.Contracts;

namespace DCAPPLIB.Services;

public class ClinicalManager(IRepositoryManager manager, IMapper mapper) : IClinicalService
{
    private readonly IRepositoryManager _manager = manager;
    private readonly IMapper _mapper = mapper;

    public void CreateClinical(ClinicalDtoForInsertion clinicalDto)
    {
        Clinical clinical = _mapper.Map<Clinical>(clinicalDto);
        _manager.Clinical.CreateClinical(clinical);
        _manager.Save();
    }

    public void DeleteClinicalById(int id)
    {
        var clinical = GetClinicalById(id, false);
        if (clinical is not null)
        {
            _manager.Clinical.DeleteClinical(clinical);
            _manager.Save();
        }
    }

    public IQueryable<Clinical> GetAllClinicals(bool trackChanges) => _manager.Clinical.GetAllClinicals(trackChanges);

    public ClinicalDtoForUpdate GetClinicalByIdForUpdate(int id, bool trackChanges)
    {
        var clinical = GetClinicalById(id, trackChanges);
        var clinicalDto = _mapper.Map<ClinicalDtoForUpdate>(clinical);
        return clinicalDto;
    }

    public Clinical? GetClinicalById(int id, bool trackChanges)
    {
        var clinical = _manager.Clinical.GetClinicalById(id, trackChanges) ?? throw new Exception("Böyle bir klinik bulunamadı!");
        return clinical;
    }

    public void UpdateClinical(ClinicalDtoForUpdate clinicalDto)
    {
        var clinical = _mapper.Map<Clinical>(clinicalDto);
        _manager.Clinical.UpdateClinical(clinical);
        _manager.Save();
    }
}