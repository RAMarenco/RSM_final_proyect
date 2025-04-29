using AutoMapper;
using NorthWindTraders.Application.DTOs.Shipper;
using NorthWindTraders.Domain.Entities;

namespace NorthWindTraders.Application.MappingProfiles
{
    class ShipperProfile : Profile
    {
        public ShipperProfile()
        {
            CreateMap<Shipper, ShipperDto>();
        }
    }
}
