using AutoMapper;
using NorthWindTraders.Application.DTOs.Order;
using NorthWindTraders.Domain.Entities;

namespace NorthWindTraders.Application.MappingProfiles
{
    class OrderDetailProfile : Profile
    {
        public OrderDetailProfile()
        {
            CreateMap<OrderDetail, OrderDetailDto>();
        }
    }
}
