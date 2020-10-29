using AutoMapper;
using HappyFarmer.API.DTOs;
using HappyFarmer.API.Extensions;
using HappyFarmer.Business.Abstract;
using HappyFarmer.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetAll()
        {
            var products = _productService.GetAll();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = _productService.GetById(id);
            return Ok(_mapper.Map<FarmerProductDTO>(product));
        }

        [HttpGet("Category/{id}")]
        public async Task<IActionResult> GetByIdWithCategories(int id)
        {
            var category = _productService.GetByIdWithCategories(id);
            return Ok(category);
        }

        [HttpPost("Product")]
        public async Task<IActionResult> AddProduct(FarmerProductDTO farmerProduct)
        {
            _productService.Create(_mapper.Map<FarmerProduct>(farmerProduct));
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(FarmerProduct farmerProduct)
        {
            var id = new int[] { 1 };
            _productService.Update(farmerProduct, id);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(FarmerProductDTO farmerProduct)
        {
            var products = _productService.GetById(farmerProduct.Id);
            _productService.Delete(_mapper.Map<FarmerProduct>(products));
            return Ok();
        }

        [HttpPost("GetProductByCategory")]
        public async Task<IActionResult> GetProductByCategory(string category, int page, int pageSize)
        {
            if(page == 0)
                page = 1;

            pageSize = 3;
            var response = _productService.GetProductByCategory(category, page, pageSize);
            return Ok(response);
        }

        [HttpGet("Products/{userId}")]
        public async Task<IActionResult> GetByIdUser(int userId)
        {
            var response = _productService.GetByIdUser(userId);
            return Ok(response);
        }

        [HttpPost("GetCustomerDeclares")]
        public async Task<IActionResult> GetCustomerDeclares(List<int> id)
        {
            return Ok(_productService.GetCustomerDeclares(id));
        }

        [HttpGet("GetPopularProduct")]
        public async Task<IActionResult> GetPopularProduct()
        {
            return Ok(_productService.GetPopularProduct());
        }


    }
}
