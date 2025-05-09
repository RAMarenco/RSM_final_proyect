﻿using AutoMapper;
using Entity = NorthWindTraders.Domain.Entities;
using Model = NorthWindTraders.Infra.Persistence.Models;

namespace NorthWindTraders.Infra.MappingProfiles
{
    class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Model.Product, Entity.Product>();
        }
    }
}
