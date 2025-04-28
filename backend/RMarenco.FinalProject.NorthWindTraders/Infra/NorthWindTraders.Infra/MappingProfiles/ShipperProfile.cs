using AutoMapper;
using InfraShipper = NorthWindTraders.Infra.Persistence.Models.Shipper;
using DomainShipper = NorthWindTraders.Domain.Entities.Shipper;

namespace NorthWindTraders.Infra.MappingProfiles
{
    class ShipperProfile : Profile
    {
        public ShipperProfile()
        {
            CreateMap<InfraShipper, DomainShipper>();
        }
    }
}
