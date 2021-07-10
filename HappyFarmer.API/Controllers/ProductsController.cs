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
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_mapper.Map<List<FarmerProductDTO>>(_productService.GetAll()));

        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok(_mapper.Map<FarmerProductDTO>(_productService.GetById(id)));

        [HttpGet("Category/{id}")]
        public IActionResult GetByIdWithCategories(int id) => Ok(_productService.GetByIdWithCategories(id));

        [HttpPost("Product")]
        public IActionResult AddProduct(FarmerProductDTO farmerProduct)
        {
            _productService.Create(_mapper.Map<FarmerProduct>(farmerProduct));
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateProduct(FarmerProduct farmerProduct)
        {
            var id = new int[] { 1 };
            _productService.Update(farmerProduct, id);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteProduct(FarmerProductDTO farmerProduct)
        {
            var products = _productService.GetById(farmerProduct.Id);
            _productService.Delete(_mapper.Map<FarmerProduct>(products));
            return Ok();
        }

        [HttpPost("GetProductByCategory")]
        public IActionResult GetProductByCategory(string category, int page, int pageSize)
        {
            if (page == 0)
                page = 1;

            pageSize = 3;
            return Ok(_productService.GetProductByCategory(category, page, pageSize));
        }

        [HttpGet("Products/{userId}")]
        public IActionResult GetByIdUser(int userId) => Ok(_mapper.Map<List<FarmerProductDTO>>(_productService.GetByIdUser(userId)));

        [HttpPost("GetCustomerDeclares")]
        public IActionResult GetCustomerDeclares(List<int> id) => Ok(_mapper.Map<List<FarmerProductDTO>>(_productService.GetCustomerDeclares(id)));

        [HttpGet("GetPopularProduct")]
        public IActionResult GetPopularProduct() => Ok(_mapper.Map<List<FarmerProductDTO>>(_productService.GetPopularProduct()));

    }
}
