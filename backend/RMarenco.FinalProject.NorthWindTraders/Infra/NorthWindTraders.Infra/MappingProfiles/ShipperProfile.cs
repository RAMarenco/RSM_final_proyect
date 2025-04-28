using AutoMapper;
using Entity = NorthWindTraders.Domain.Entities;
using Model = NorthWindTraders.Infra.Persistence.Models;

namespace NorthWindTraders.Infra.MappingProfiles
{
    class ShipperProfile : Profile
    {
        public ShipperProfile()
        {
            CreateMap<Model.Shipper, Entity.Shipper>();
        }
    }
}
