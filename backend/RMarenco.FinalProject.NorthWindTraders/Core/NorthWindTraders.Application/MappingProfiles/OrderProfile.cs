using AutoMapper;
using NorthWindTraders.Application.DTOs.Order;
using NorthWindTraders.Domain.Entities;

namespace NorthWindTraders.Application.MappingProfiles
{
    class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<CreateOrderDto, Order>()
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.Employee, opt => opt.Ignore())
                .ForMember(dest => dest.Shipper, opt => opt.Ignore());
            CreateMap<Order, OrderDto>();
        }
    }
}
