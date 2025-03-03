﻿using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Services;

namespace Talabat.APIs.Controllers
{

    public class OrdersController : APIBaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService,IMapper mapper)
        {
            this._orderService = orderService;
            this._mapper = mapper;
        }
        //create order
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(Order),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var MappedAddress = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);
            var order = await _orderService.CreateOrderAsync(BuyerEmail,orderDto.BasketId,orderDto.DeliveryMethodId,MappedAddress);
            if (order is null) return BadRequest(new ApiResponse(400,"There is a problem with your order"));
            return Ok(order);
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<Order>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrdersForUser() {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var Orders = await _orderService.GetOrdersForSpecificUserAsync(BuyerEmail);
            if (Orders is null) return NotFound(new ApiResponse(404, "There is no Orders for this user."));
            return Ok(Orders);
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Order>> GetOrderByIdForUser(int id)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var Order = await _orderService.GetOrderByIdForSpecificUserAsync(BuyerEmail, id);
            if (Order is null) return NotFound(new ApiResponse(404, $"There is no order with {id} for this user."));
            return Ok(Order);
        }
    }
}
