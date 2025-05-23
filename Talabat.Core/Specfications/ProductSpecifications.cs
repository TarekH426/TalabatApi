using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specfications
{
    public class ProductSpecifications : BaseSpecifications<Product, int> 
    {
        public ProductSpecifications(int id) : base(P => P.Id == id)
        {
            AddIncludes();
        }

        public ProductSpecifications(string? sort, int? brandId, int? typeId,int pageSize,int pageIndex):base(
            P=>(!brandId.HasValue || brandId==P.BrandId) // true
            &&
            (!typeId.HasValue || typeId == P.CategoryId) // true
        )
        {
            if (!string.IsNullOrEmpty(sort))
            {
                switch(sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc( P => P.Price);
                        break;

                    default:
                        AddOrderBy( P => P.Name);
                        break;
                }
            }
            AddIncludes();

            ApplyPagination(pageSize*(pageIndex-1),pageSize);
        }

        private void AddIncludes() 
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);
        }

      
    }
}
