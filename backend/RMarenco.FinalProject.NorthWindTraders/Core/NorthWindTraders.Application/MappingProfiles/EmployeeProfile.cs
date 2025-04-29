using AutoMapper;
using NorthWindTraders.Application.DTOs.Employee;
using NorthWindTraders.Domain.Entities;

namespace NorthWindTraders.Application.MappingProfiles
{
    class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>();
        }
    }
}
