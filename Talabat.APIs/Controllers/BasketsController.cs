using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;

namespace Talabat.APIs.Controllers
{
    public class BasketsController : APIBaseController
    {
        private readonly IBasketRepository _basketRepository;

        public BasketsController(IBasketRepository basketRepository)
        {
            this._basketRepository = basketRepository;
        }
        //Get Basket or Recreate
        [HttpGet("{basketId}")]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string basketId)
        {
            var Basket = await _basketRepository.GetBasketAsync(basketId);
            if (Basket is null) return new CustomerBasket(basketId);
            else return Ok(Basket);
        }

        // Update  or Create New Basket
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {
            var CreatedOrUpdatedBasket = await _basketRepository.UpdateBasketAsync(basket);
            if (CreatedOrUpdatedBasket is null) return BadRequest(new ApiResponse(400));
            else return Ok(CreatedOrUpdatedBasket);
        }

        // Delete
        [HttpDelete]
        public async Task<ActionResult<bool>> DeletBasket(string basketId)
        {
            return await _basketRepository.DeleteBasketAsync(basketId);
            
        }
    }
}
