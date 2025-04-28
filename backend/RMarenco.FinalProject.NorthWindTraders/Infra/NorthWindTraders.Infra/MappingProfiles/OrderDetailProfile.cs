using AutoMapper;
using Entity = NorthWindTraders.Domain.Entities;
using Model = NorthWindTraders.Infra.Persistence.Models;

namespace NorthWindTraders.Infra.MappingProfiles
{
    class OrderDetailProfile : Profile
    {
        public OrderDetailProfile()
        {
            CreateMap<Model.OrderDetail, Entity.OrderDetail>();
        }
    }
}
