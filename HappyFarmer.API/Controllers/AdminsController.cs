using AutoMapper;
using HappyFarmer.API.DTOs;
using HappyFarmer.Business.Abstract;
using HappyFarmer.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace HappyFarmer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBannerService _bannerService;
        private readonly IAdminMessageService _adminMessageService;
        private readonly ISliderService _sliderService;
        private readonly IMapper _mapper;
        public AdminsController(IProductService productService, IMapper mapper,ICategoryService categoryService, IBannerService bannerService, IAdminMessageService adminMessageService, ISliderService sliderService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _mapper = mapper;
            _bannerService = bannerService;
            _adminMessageService = adminMessageService;
            _sliderService = sliderService;
        }

        #region Products Methods

        [HttpGet("Product")]
        public IActionResult GetAllProducts()
        {
            var products = _productService.GetAll();
            return Ok(_mapper.Map<List<FarmerProductDTO>>(products));
        }

        [HttpGet("Product/{id}")]
        public IActionResult GetByIdProducts(int id)
        {
            var products = _productService.GetById(id);
            return Ok(_mapper.Map<FarmerProductDTO>(products));
        }

        [HttpPost("Product")]
        public IActionResult AddProducts(FarmerProductDTO farmerProductDTO)
        {
            _productService.Create(_mapper.Map<FarmerProduct>(farmerProductDTO));
            return Ok();
        }

        [HttpPut("Product")]
        public IActionResult UpdateProducts(FarmerProductDTO farmerProductDTO)
        {
            var products = _productService.GetById(farmerProductDTO.Id);
            if (products == null)
                throw new ArgumentNullException($"{farmerProductDTO.Id} ye sahip ürün bulunamadı");

            _productService.Update(_mapper.Map<FarmerProduct>(farmerProductDTO));
            return Ok();
        }

        [HttpDelete("Product")]
        public IActionResult DeleteProducts(FarmerProductDTO farmerProductDTO)
        {
            var products = _productService.GetById(farmerProductDTO.Id);
            if (products == null)
                throw new ArgumentNullException($"{farmerProductDTO.Id} ye sahip ürün bulunamadı");

            _productService.Delete(_mapper.Map<FarmerProduct>(farmerProductDTO));
            return Ok();
        }

        [HttpGet("Product/Category/{category}/{page}/{pageSize}")]
        public IActionResult GetProductWithCategories(string category,int page,int pageSize)
        {
            var products = _productService.GetProductByCategory(category, page,
                pageSize);

            if (products == null)
                throw new ArgumentNullException("Aradıgınız kategoriye ait ürün bulunamadı");

            return Ok(_mapper.Map<FarmerProductDTO>(products));
        }

        [HttpGet("Product/Category/{category}")]
        public IActionResult GetCountByCategories(string category)
        {
            var products = _productService.GetCountByCategory(category);

            if (products == 0)
                throw new ArgumentNullException("Aradıgınız kategoriye ait ürün bulunamadı");

            return Ok(products);
        }

        [HttpGet("Product/Category/{id}/Category")] // bu metod hata veriyor
        public IActionResult GetCByIdCategories(int id)
        {
            var products = _productService.GetByIdWithCategories(id);

            if (products == null)
                throw new ArgumentNullException("Aradıgınız kategoriye ait ürün bulunamadı");

            return Ok(_mapper.Map<FarmerProductDTO>(products));
        }

        [HttpGet("Product/{type}/{userId}")]
        public IActionResult GetProductByType(string type, int userId)
        {
            var products = _productService.GetProductByType(type, userId);

            return Ok(_mapper.Map<List<FarmerProductDTO>>(products));
        }

        [HttpGet("Product/FindProduct/{type}/{searchText}")]
        public IActionResult FindForProductsByCategoryType(string type, string searchText)
        {
            var products = _productService.FindForProductsByCategoryType(type, searchText);
            return Ok(_mapper.Map<List<FarmerProductDTO>>(products));
        }

        [HttpGet("Product/User/{userId}")]
        public IActionResult GetByIdUser(int userId)
        {
            var products = _productService.GetByIdUser(userId);
            return Ok(_mapper.Map<List<FarmerProductDTO>>(products));
        }

        [HttpPost("Product/CustomerDeclares")]
        public IActionResult GetCustomerDeclares(List<int> type)
        {
            var products = _productService.GetCustomerDeclares(type);
            return Ok(_mapper.Map<List<FarmerProductDTO>>(products));
        }

        [HttpGet("Product/PopularProduct/{pageStandOut}/{pageSize}")]
        public IActionResult GetPopularProduct(int pageStandOut,int pageSize)
        {
            var products = _productService.GetPopularProduct(pageStandOut, pageSize);
            return Ok(_mapper.Map<List<FarmerProductDTO>>(products));
        }

        [HttpGet("Product/FilterByProduct/{lowPrice}/{topPrice}/{type}")]
        public IActionResult FilterByPrice(int lowPrice,int topPrice,string type)
        {
            var products = _productService.FilterByPrice(lowPrice, topPrice, type);
            return Ok(_mapper.Map<List<FarmerProductDTO>>(products));
        }

        [HttpGet("Product/FilterByRegion/{type}/{city}/{country}/{neighboard}")]
        public IActionResult FilterByRegion(string type,string city, string country, string neighboard)
        {
            var products = _productService.FilterByRegion(type, city, country, neighboard);
            return Ok(_mapper.Map<List<FarmerProductDTO>>(products));
        }

        [HttpPost("Product/Comment")]
        public IActionResult AddCommentProducts(FarmerProductCommentDTO farmerProductCommentDTO)
        {
            if (farmerProductCommentDTO.ProductId <= 0)
                throw new ArgumentNullException("Ürün hakkında yorum yapabilmek için lütfen geçerli bir ürün Id gönderdiğinizden emin olunuz");

            if (farmerProductCommentDTO.UserId <= 0)
                throw new ArgumentNullException($"{farmerProductCommentDTO.UserId} 'ye sahip bir kullanıcı bulunamadı");

            _productService.CreateComment(_mapper.Map<ProductComment>(farmerProductCommentDTO));
            return Ok();
        }

        [HttpGet("Product/CategoryWithCount")]
        public IActionResult GetCategoryWithCount()
        {
            var products = _productService.GetCategoryWithCount();
            return Ok(_mapper.Map<List<FarmerProductDTO>>(products));
        }

        [HttpGet("Product/GetCityProduct")]
        public IActionResult GetCityProduct()
        {
            return Ok(_productService.GetCityProduct());
        }
        #endregion

        #region Categories Methods

        [HttpGet("Category")]
        public IActionResult GetAllCategories()
        {
            var products = _categoryService.GetAll();
            return Ok(_mapper.Map<List<FarmerCategoryDTO>>(products));
        }

        [HttpGet("Category/{id}")]
        public IActionResult GetByIdCategories(int id)
        {
            var products = _categoryService.GetById(id);
            return Ok(_mapper.Map<FarmerCategoryDTO>(products));
        }

        [HttpPost("Category")]
        public IActionResult CreateCategories(FarmerCategoryDTO farmerCategoryDTO)
        {
            _categoryService.Create(_mapper.Map<FarmerCategory>(farmerCategoryDTO));
            return Ok();
        }

        [HttpPut("Category")]
        public IActionResult UpdateCategories(FarmerCategoryDTO farmerCategoryDTO)
        {
            var categories = _productService.GetById(farmerCategoryDTO.Id);
            if (categories == null)
                throw new ArgumentNullException($"{farmerCategoryDTO.Id} ye sahip bir kategori bilgisi bulunamadı.");

            _categoryService.Update(_mapper.Map<FarmerCategory>(farmerCategoryDTO));
            return Ok();
        }

        [HttpDelete("Category")]
        public IActionResult DeleteCategories(FarmerCategoryDTO farmerCategoryDTO)
        {
            var categories = _productService.GetById(farmerCategoryDTO.Id);
            if (categories == null)
                throw new ArgumentNullException($"{farmerCategoryDTO.Id} ye sahip bir kategori bilgisi bulunamadı.");

            _categoryService.Delete(_mapper.Map<FarmerCategory>(farmerCategoryDTO));
            return Ok();
        }

        #endregion

        #region Banners Methods

        [HttpGet("Banner")]
        public IActionResult GetAllBanner()
        {
            var bannerService = _bannerService.GetAll();
            return Ok(_mapper.Map<List<FarmerBannerDTO>>(bannerService));
        }

        [HttpGet("Banner/{id}")]
        public IActionResult GetByIdBanner(int id)
        {
            var bannerService = _bannerService.GetById(id);
            return Ok(_mapper.Map<FarmerBannerDTO>(bannerService));
        }

        [HttpPost("Banner")]
        public IActionResult CreateBanner(FarmerBannerDTO farmerBannerDTO)
        {
            _bannerService.Create(_mapper.Map<FarmerBanner>(farmerBannerDTO));
            return Ok();
        }

        [HttpPut("Banner")]
        public IActionResult UpdateBanner(FarmerBannerDTO farmerBannerDTO)
        {
            var result = _bannerService.GetById(farmerBannerDTO.Id);
            if (result == null)
                throw new ArgumentException($"{farmerBannerDTO.Id} 'ye sahip banner kaydı bulunamadı.");

            _bannerService.Update(_mapper.Map<FarmerBanner>(farmerBannerDTO));
            return Ok();
        }

        [HttpDelete("Banner")]
        public IActionResult DeleteBanner(FarmerBannerDTO farmerBannerDTO)
        {
            var result = _bannerService.GetById(farmerBannerDTO.Id);
            if (result == null)
                throw new ArgumentException($"{farmerBannerDTO.Id} 'ye sahip banner kaydı bulunamadı.");

            _bannerService.Delete(_mapper.Map<FarmerBanner>(farmerBannerDTO));

            return Ok();
        }

        [HttpGet("Banner/Admin")]
        public IActionResult GetAdminBanner()
        {
            return Ok(_mapper.Map<List<FarmerBannerDTO>>(_bannerService.GetAdminBanner()));
        }

        [HttpGet("Banner/GetLower")]
        public IActionResult GetLowerAll()
        {
            return Ok(_mapper.Map<List<FarmerBannerDTO>>(_bannerService.GetLowerAll()));
        }

        #endregion

        #region AdminMessages Methods

        [HttpGet("AdminMessage")]
        public IActionResult GetAllAdminMessages()
        {
            var bannerService = _adminMessageService.GetAll();
            return Ok(_mapper.Map<List<FarmerAdminMessageDTO>>(bannerService));
        }

        [HttpGet("AdminMessage/{id}")]
        public IActionResult GetByIdAdminMessages(int id)
        {
            var bannerService = _adminMessageService.GetById(id);
            return Ok(_mapper.Map<FarmerAdminMessageDTO>(bannerService));
        }

        [HttpPost("AdminMessage")]
        public IActionResult CreateAdminMessages(FarmerAdminMessageDTO farmerAdminMessageDTO)
        {
            _adminMessageService.Create(_mapper.Map<FarmerAdminMessage>(farmerAdminMessageDTO));
            return Ok();
        }

        [HttpPut("AdminMessage")]
        public IActionResult UpdateAdminMessages(FarmerAdminMessageDTO farmerAdminMessageDTO)
        {
            var result = _bannerService.GetById(farmerAdminMessageDTO.Id);
            if (result == null)
                throw new ArgumentException($"{farmerAdminMessageDTO.Id} 'ye sahip banner kaydı bulunamadı.");

            _adminMessageService.Update(_mapper.Map<FarmerAdminMessage>(farmerAdminMessageDTO));
            return Ok();
        }

        [HttpDelete("AdminMessage")]
        public IActionResult DeleteAdminMessages(FarmerAdminMessageDTO farmerAdminMessageDTO)
        {
            var result = _bannerService.GetById(farmerAdminMessageDTO.Id);
            if (result == null)
                throw new ArgumentException($"{farmerAdminMessageDTO.Id} 'ye sahip banner kaydı bulunamadı.");

            _adminMessageService.Delete(_mapper.Map<FarmerAdminMessage>(farmerAdminMessageDTO));

            return Ok();
        }

        #endregion

        #region Sliders Methods

        [HttpGet("Slider")]
        public IActionResult GetAllSliders()
        {
            var sliderServices = _sliderService.GetAll();
            return Ok(_mapper.Map<List<FarmerSliderDTO>>(sliderServices));
        }

        [HttpGet("Slider/{id}")]
        public IActionResult GetByIdSliders(int id)
        {
            var sliderService = _bannerService.GetById(id);
            return Ok(_mapper.Map<FarmerSliderDTO>(sliderService));
        }

        [HttpPost("Slider")]
        public IActionResult CreateSliders(FarmerSliderDTO farmerSliderDTO)
        {
            _sliderService.Create(_mapper.Map<FarmerSlider>(farmerSliderDTO));
            return Ok();
        }

        [HttpPut("Slider")]
        public IActionResult UpdateSliders(FarmerSliderDTO farmerSliderDTO)
        {
            var result = _bannerService.GetById(farmerSliderDTO.Id);
            if (result == null)
                throw new ArgumentException($"{farmerSliderDTO.Id} 'ye sahip banner kaydı bulunamadı.");

            _sliderService.Update(_mapper.Map<FarmerSlider>(farmerSliderDTO));
            return Ok();
        }

        [HttpDelete("Slider")]
        public IActionResult DeleteSliders(FarmerSliderDTO farmerSliderDTO)
        {
            var result = _bannerService.GetById(farmerSliderDTO.Id);
            if (result == null)
                throw new ArgumentException($"{farmerSliderDTO.Id} 'ye sahip banner kaydı bulunamadı.");

            _sliderService.Delete(_mapper.Map<FarmerSlider>(farmerSliderDTO));

            return Ok();
        }

        #endregion
    }
}
