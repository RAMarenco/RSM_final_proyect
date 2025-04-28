using AutoMapper;
using Entity = NorthWindTraders.Domain.Entities;
using Model = NorthWindTraders.Infra.Persistence.Models;

namespace NorthWindTraders.Infra.MappingProfiles
{
    class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Model.Customer, Entity.Customer>();
        }
    }
}
