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
                .ForMember(dest => dest.Shipper, opt => opt.MapFrom(src => src.ShipViaNavigation));

            CreateMap<Entity.Order, Model.Order>()
                .ForMember(dest => dest.ShipViaNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.Employee, opt => opt.Ignore());
        }
    }
}
