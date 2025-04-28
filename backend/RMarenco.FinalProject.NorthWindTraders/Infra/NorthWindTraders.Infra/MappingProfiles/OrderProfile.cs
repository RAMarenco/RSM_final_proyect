using AutoMapper;
using Entity = NorthWindTraders.Domain.Entities;
using Model = NorthWindTraders.Infra.Persistence.Models;

namespace NorthWindTraders.Infra.MappingProfiles
{
    class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Model.Order, Entity.Order>()
                .ForMember(dest => dest.Shipper, opt => opt.MapFrom(src => src.ShipViaNavigation))
                .ReverseMap()
                .ForMember(dest => dest.ShipViaNavigation, opt => opt.MapFrom(src => src.Shipper));
        }
    }
}
