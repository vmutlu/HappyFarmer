using AutoMapper;
using HappyFarmer.API.DTOs;
using HappyFarmer.Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HappyFarmer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public CartsController(ICartService cartService, IOrderService orderService, IProductService productService, IUserService userService, IMapper mapper)
        {
            _cartService = cartService;
            _orderService = orderService;
            _productService = productService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("ActiveUser")]
        public IActionResult GetCartByUserId(string activeCustomerId)
        {
            var cart = _cartService.GetCartByUserId(activeCustomerId);

            return Ok(_mapper.Map<FarmerCartDTO>(cart));
        }

        [HttpGet("GetOrders/{userId}")]
        public IActionResult GetOrders(string userId)
        {
            var user = _orderService.GetOrders(userId);

            return Ok(_mapper.Map<List<FarmerOrderDTO>>(user));
        }
    }
}
