using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Dtos;
using Talabat.Core.Dtos.Products;
using Talabat.Core.Service.Contract;
using TalabatAPIs.Attributes;
using TalabatAPIs.Errors;

namespace TalabatAPIs.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }


        [Authorize]
        [Cache(100)]
        [HttpGet] // Get: BaseUrl/api/Products
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts([FromQuery]string? sort, [FromQuery]int? brandId, [FromQuery] int? typeId, [FromQuery] int? pageSize, [FromQuery] int? pageIndex) // name- priceAsc - priceDsc
        {

            var Products = await _productService.GetAllProductsAsync(sort,brandId,typeId,pageSize,pageIndex);

            return Ok(Products);
        }
        [ProducesResponseType(typeof(IEnumerable<CategoryBrandDto>),StatusCodes.Status200OK)]
        [HttpGet("brands")] // Get: BaseUrl/api/Products/brands
        public async Task<ActionResult<IEnumerable<CategoryBrandDto>>> GetAllBrands()
        {
            var brands = await _productService.GetAllBrandAsync();

            return Ok(brands);
        }

        [ProducesResponseType(typeof(IEnumerable<CategoryBrandDto>), StatusCodes.Status200OK)]
        [HttpGet("categories")] // Get: BaseUrl/api/Products/categories
        public async Task<ActionResult<IEnumerable<CategoryBrandDto>>> GetAllCategories()
        {
            var categories = await _productService.GetAllCategoriesAsync();

            return Ok(categories);
        }

        [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int? id)
        {
            if (id is null) return BadRequest("invalid Id");

            var product = await _productService.GetProductById(id.Value);

            if (product is null) return NotFound(new {Message="Not Found", StatusCode = 404 });

            return Ok(product);
        }
    }
}
