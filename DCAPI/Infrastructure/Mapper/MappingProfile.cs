using AutoMapper;
using DCAPPLIB.Entities;
using DCAPPLIB.Entities.Dtos.Clinic;
using DCAPPLIB.Entities.Dtos.Customer;
using DCAPPLIB.Entities.Dtos.Dentist;
using DCAPPLIB.Entities.Dtos.User;

namespace DCAPI.Infrastructure.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<DentistDtoForInsertion, Dentist>();
        CreateMap<DentistDtoForUpdate, Dentist>().ReverseMap();

        CreateMap<Clinical, ClinicDto>();
        CreateMap<ClinicDtoForInsertion, Clinical>();
        CreateMap<ClinicDtoForUpdate, Clinical>().ReverseMap();

        CreateMap<Clinical, ClinicExtendedDto>().ForMember(dest => dest.Dentists, opt => opt.MapFrom(src => src.Dentists));
        CreateMap<Dentist, DentistWithClinicDto>().ForMember(dest => dest.Clinic, opt => opt.MapFrom(src => src.Clinical));

        CreateMap<CustomerDtoForInsertion, Customer>();
        CreateMap<CustomerDtoForUpdate, Customer>().ReverseMap();

        CreateMap<UserDtoForRegistration, User>().ReverseMap();
        CreateMap<UserDtoForAuth, User>().ReverseMap();
    }
}