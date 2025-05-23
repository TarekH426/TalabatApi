using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Dtos;
using Talabat.Core.Dtos.Products;
using Talabat.Core.Entities;

namespace Talabat.Core.Mapping.Products
{
    public class ProductProfile : Profile
    {

        public ProductProfile(IConfiguration configuration) 
        {
            CreateMap<Product, ProductDto>()
             .ForMember(d=>d.BrandName,options => options.MapFrom(s => s.Brand.Name))
              .ForMember(d => d.CategoryName, options => options.MapFrom(s => s.Category.Name))
              .ForMember(d=>d.PictureUrl,options=>options.MapFrom(S=> $"{configuration["baseUrl"]}{ S.PictureUrl}"));
             
            CreateMap<ProductBrand, CategoryBrandDto>();
            CreateMap<ProductCategory, CategoryBrandDto>();

            
        }
    }
}
