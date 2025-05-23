using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; }=null!;

        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }

        // Product & Brand
        public int? BrandId { get; set; } // ForeignKey Column => ProductBrand
        public ProductBrand Brand { get; set; } = null!; // Navigtional Property [One]

        // Product & Category

        public int? CategoryId { get; set; } // ForeignKey Column => ProductCategory
        public ProductCategory Category { get; set; } = null!; // Navigtional Property [One]

    }
}
