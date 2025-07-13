using AutoMapper;
using DCAPPLIB.Entities;
using DCAPPLIB.Entities.Dtos.Clinical;
using DCAPPLIB.Entities.Dtos.Customer;
using DCAPPLIB.Entities.Dtos.Dentist;

namespace DCAPI.Infrastructure.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<DentistDtoForInsertion, Dentist>();
        CreateMap<DentistDtoForUpdate, Dentist>().ReverseMap();

        CreateMap<ClinicalDtoForInsertion, Clinical>();
        CreateMap<ClinicalDtoForUpdate, Clinical>().ReverseMap();

        CreateMap<CustomerDtoForInsertion, Customer>();
        CreateMap<CustomerDtoForUpdate, Customer>().ReverseMap();
    }
}