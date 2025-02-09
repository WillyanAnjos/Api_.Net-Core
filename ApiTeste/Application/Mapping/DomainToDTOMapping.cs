using ApiTeste.Domain.DTOs;
using ApiTeste.Models;
using AutoMapper;

namespace ApiTeste.Application.Mapping;

public class DomainToDTOMapping : Profile
{
    public DomainToDTOMapping()
    {
        CreateMap<Employee, EmployeeDTO>()
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name));
    }

}
