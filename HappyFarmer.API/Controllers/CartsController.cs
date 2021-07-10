using AutoMapper;
using HappyFarmer.API.DTOs;
using HappyFarmer.Business.Abstract;
using HappyFarmer.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HappyFarmer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        #region Fields

        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        #endregion
        public CartsController(ICartService cartService, IOrderService orderService, IProductService productService, IUserService userService, IMapper mapper)
        {
            _cartService = cartService;
            _orderService = orderService;
            _productService = productService;
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Tüm order tablosu listeleme endpoindi
        /// </summary>
        /// <param name="activeCustomerId"></param>
        /// <returns></returns>
        [HttpGet("ActiveUser")]
        public IActionResult GetCartByUserId(string activeCustomerId) => Ok(_mapper.Map<FarmerCartDTO>(_cartService.GetCartByUserId(activeCustomerId)));

        /// <summary>
        /// id ' ye göre sipariş getirme endpoindi
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("GetOrders/{userId}")]
        public IActionResult GetOrders(string userId) => Ok(_mapper.Map<List<FarmerOrderDTO>>(_orderService.GetOrders(userId)));

        /// <summary>
        /// Sipariş ekleme endpointi
        /// </summary>
        /// <param name="farmerOrder"></param>
        /// <returns></returns>
        [HttpPost("Carts")]
        public IActionResult Add(FarmerOrder farmerOrder)
        {
            _orderService.Create(farmerOrder);
            return Ok();
        }
    }
}
