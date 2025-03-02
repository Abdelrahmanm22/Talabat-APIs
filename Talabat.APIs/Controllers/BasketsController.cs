using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;

namespace Talabat.APIs.Controllers
{
    public class BasketsController : APIBaseController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketsController(IBasketRepository basketRepository,IMapper mapper)
        {
            this._basketRepository = basketRepository;
            this._mapper = mapper;
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
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var MappedBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
            var CreatedOrUpdatedBasket = await _basketRepository.UpdateBasketAsync(MappedBasket);
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
