using AutoMapper;
using InfraProduct = NorthWindTraders.Infra.Persistence.Models.Product;
using DomainProduct = NorthWindTraders.Domain.Entities.Product;

namespace NorthWindTraders.Infra.MappingProfiles
{
    class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<InfraProduct, DomainProduct>();
        }
    }
}
