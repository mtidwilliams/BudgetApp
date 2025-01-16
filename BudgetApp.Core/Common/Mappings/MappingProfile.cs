using AutoMapper;
using BudgetApp.Core.Common.DTOs;
using BudgetApp.Domain.Entities;

namespace BudgetApp.Core.Mappings;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Address, AddressDTO>();

        CreateMap<Person, PersonDTO>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
            
        CreateMap<User, UserDTO>()
            .ForMember(dest => dest.Person, opt => opt.MapFrom(src => src.Person));
    }
}
