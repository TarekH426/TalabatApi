using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Dtos.Products
{
    public class ProductDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }

        // Product & Brand
        public int? BrandId { get; set; } // ForeignKey Column => ProductBrand
        public string BrandName { get; set; } = null!; // Navigtional Property [One]

        // Product & Category

        public int? CategoryId { get; set; } // ForeignKey Column => ProductCategory
        public string CategoryName { get; set; } = null!; // Navigtional Property [One]

    }
}
