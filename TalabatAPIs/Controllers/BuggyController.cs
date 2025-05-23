using Microsoft.AspNetCore.Mvc;
using Talabat.Repository.Data.Contexts;
using TalabatAPIs.Errors;

namespace TalabatAPIs.Controllers
{
    public class BuggyController: BaseApiController
    {

        private readonly StoreContext _dbContext;

        public BuggyController(StoreContext dbContext)
        {
            _dbContext = dbContext; 
        }

        [HttpGet("notfound")] // Get : /api/Byggy/notfound
        public async Task<ActionResult> GetNotFoundRequest()
        {
         var brand = await _dbContext.ProductBrands.FindAsync(100);

            if(brand is null)
                return NotFound(new ApiResponse(404));

            return Ok(brand);
        }

        [HttpGet("servererror")] // Get : /api/Byggy/servererror
        public async Task<ActionResult> GetServerError()
        {
            var brand = await _dbContext.ProductBrands.FindAsync(100);

            var brandToReturn = brand.ToString(); // will throw Exp [null ref]

            return Ok(brandToReturn);
        }



        [HttpGet("badrequest")] // Get : /api/Byggy/badrequest
        public async Task<ActionResult> GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")] // Get : /api/Byggy/badrequest/Five
        public async Task<ActionResult> GetValidationError(int id)
        {
            return Ok();
        }

        [HttpGet("unauthorized")] // Get : /api/Byggy/unauthorized
        public async Task<ActionResult> GetUnauthoriedError(int id)
        {
            return Unauthorized(new ApiResponse(401));
        }
    }
}
