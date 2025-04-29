using AutoMapper;
using NorthWindTraders.Application.DTOs.Customer;
using NorthWindTraders.Domain.Entities;

namespace NorthWindTraders.Application.MappingProfiles
{
    class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>();
        }
    }
}
