using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Dtos.Basket;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using TalabatAPIs.Errors;

namespace TalabatAPIs.Controllers
{
   
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository,IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        [HttpGet] // Get /api/basket?id=basket1
        public async Task<ActionResult<CustomerBasketDto>> GetBasketById(string? id) 
        {
            if (id is null) 
                return BadRequest(new ApiResponse(400, "Invalid ID"));
       
            var basket = await _basketRepository.GetBasketAsync(id);
            
            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost] // POST: /api/basket
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdateBasket(CustomerBasketDto basket) 
        {
            var mappedBasket = _mapper.Map<CustomerBasket>(basket);

            var CreatedorUpdatedBasket = await _basketRepository.UpdateBasketAsync(mappedBasket);

            if (CreatedorUpdatedBasket is null) return BadRequest(new ApiResponse(400));

            return Ok(CreatedorUpdatedBasket);
        }

        [HttpDelete] // DELETE : /api/basket
        public async Task DeleteBasket(string id)
        {
            await _basketRepository.DeleteBasketAsync(id);
        }
    }
}
