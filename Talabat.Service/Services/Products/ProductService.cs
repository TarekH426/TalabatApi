using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Dtos;
using Talabat.Core.Dtos.Products;
using Talabat.Core.Entities;
using Talabat.Core.Service.Contract;
using Talabat.Core.Specfications;

namespace Talabat.Service.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync(string? sort, int? brandId, int? typeId, int? pageSize, int? pageIndex)
        {
            var spec = new ProductSpecifications(sort,brandId,typeId,pageSize.Value,pageIndex.Value);
          return  _mapper.Map<IEnumerable<ProductDto>>(await _unitOfWork.Repository<Product, int>().GetAllWithSpecAsync(spec));
          
        }
        public async Task<IEnumerable<CategoryBrandDto>> GetAllBrandAsync()
        {
          var brands = await _unitOfWork.Repository<ProductBrand,int>().GetAllAsync();

          var mappedBrands =  _mapper.Map<IEnumerable<CategoryBrandDto>>(brands);

            return mappedBrands;
        }

        public async Task<IEnumerable<CategoryBrandDto>> GetAllCategoriesAsync()
        {
            return _mapper.Map<IEnumerable<CategoryBrandDto>>(await _unitOfWork.Repository<ProductCategory,int>().GetAllAsync());
            
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            var spec = new ProductSpecifications(id);

            var product = await _unitOfWork.Repository<Product, int>().GetByIdWithSpecAsync(spec);

            var mappedProduct = _mapper.Map<ProductDto>(product);

            return mappedProduct;
        }

       
    }
}
