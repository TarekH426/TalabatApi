using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Dtos;
using Talabat.Core.Dtos.Products;
using Talabat.Core.Entities;

namespace Talabat.Core.Service.Contract
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync(string? sort,int? brandId,int? typeId,int? pageSize,int? pageIndex);

        Task<IEnumerable<CategoryBrandDto>> GetAllCategoriesAsync();
        Task<IEnumerable<CategoryBrandDto>> GetAllBrandAsync();
        Task<ProductDto> GetProductById(int id);
    }

}
