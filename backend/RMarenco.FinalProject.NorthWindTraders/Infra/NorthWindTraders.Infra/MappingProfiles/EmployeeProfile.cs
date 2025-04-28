using AutoMapper;
using Entity = NorthWindTraders.Domain.Entities;
using Model = NorthWindTraders.Infra.Persistence.Models;

namespace NorthWindTraders.Infra.MappingProfiles
{
    class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Model.Employee, Entity.Employee>();
        }
    }
}
