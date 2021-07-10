using AutoMapper;
using HappyFarmer.API.DTOs;
using HappyFarmer.Business.Abstract;
using HappyFarmer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HappyFarmer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        #region Fields

        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBannerService _bannerService;
        private readonly IAdminMessageService _adminMessageService;
        private readonly ISliderService _sliderService;
        private readonly IAboutUsService _aboutUsService;
        private readonly IUserService _userService;
        private readonly ISecurityInformationService _securityInformationService;
        private readonly IMapper _mapper;

        #endregion

        public AdminsController(IProductService productService, IMapper mapper, ICategoryService categoryService, IBannerService bannerService, IAdminMessageService adminMessageService, ISliderService sliderService, IAboutUsService aboutUsService, IUserService userService, ISecurityInformationService securityInformationService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _mapper = mapper;
            _bannerService = bannerService;
            _adminMessageService = adminMessageService;
            _sliderService = sliderService;
            _aboutUsService = aboutUsService;
            _userService = userService;
            _securityInformationService = securityInformationService;
        }

        #region Products Methods

        [HttpGet("Product")]
        public IActionResult GetAllProducts() => Ok(_mapper.Map<List<FarmerProductDTO>>(_productService.GetAll()));

        [HttpGet("Product/{id}")]
        public IActionResult GetByIdProducts(int id) => Ok(_mapper.Map<FarmerProductDTO>(_productService.GetById(id)));

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
        public IActionResult GetProductWithCategories(string category, int page, int pageSize)
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
        public IActionResult GetProductByType(string type, int userId) => Ok(_mapper.Map<List<FarmerProductDTO>>(_productService.GetProductByType(type, userId)));

        [HttpGet("Product/FindProduct/{type}/{searchText}")]
        public IActionResult FindForProductsByCategoryType(string type, string searchText) => Ok(_mapper.Map<List<FarmerProductDTO>>(_productService.FindForProductsByCategoryType(type, searchText)));

        [HttpGet("Product/User/{userId}")]
        public IActionResult GetByIdUser(int userId) => Ok(_mapper.Map<List<FarmerProductDTO>>(_productService.GetByIdUser(userId)));

        [HttpPost("Product/CustomerDeclares")]
        public IActionResult GetCustomerDeclares(List<int> type) => Ok(_mapper.Map<List<FarmerProductDTO>>(_productService.GetCustomerDeclares(type)));

        [HttpGet("Product/PopularProduct/{pageStandOut}/{pageSize}")]
        public IActionResult GetPopularProduct(int pageStandOut, int pageSize) => Ok(_mapper.Map<List<FarmerProductDTO>>(_productService.GetPopularProduct(pageStandOut, pageSize)));

        [HttpGet("Product/FilterByProduct/{lowPrice}/{topPrice}/{type}")]
        public IActionResult FilterByPrice(int lowPrice, int topPrice, string type) => Ok(_mapper.Map<List<FarmerProductDTO>>(_productService.FilterByPrice(lowPrice, topPrice, type)));

        [HttpGet("Product/FilterByRegion/{type}/{city}/{country}/{neighboard}")]
        public IActionResult FilterByRegion(string type, string city, string country, string neighboard) => Ok(_mapper.Map<List<FarmerProductDTO>>(_productService.FilterByRegion(type, city, country, neighboard)));


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
        public IActionResult GetCategoryWithCount() => Ok(_mapper.Map<List<FarmerProductDTO>>(_productService.GetCategoryWithCount()));

        [HttpGet("Product/GetCityProduct")]
        public IActionResult GetCityProduct() => Ok(_productService.GetCityProduct());

        #endregion

        #region Categories Methods

        [HttpGet("Category")]
        public IActionResult GetAllCategories() => Ok(_mapper.Map<List<FarmerCategoryDTO>>(_categoryService.GetAll()));

        [HttpGet("Category/{id}")]
        public IActionResult GetByIdCategories(int id) => Ok(_mapper.Map<FarmerCategoryDTO>(_categoryService.GetById(id)));

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
        public IActionResult GetAllBanner() => Ok(_mapper.Map<List<FarmerBannerDTO>>(_bannerService.GetAll()));

        [HttpGet("Banner/{id}")]
        public IActionResult GetByIdBanner(int id) => Ok(_mapper.Map<FarmerBannerDTO>(_bannerService.GetById(id)));

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
        public IActionResult GetAdminBanner() => Ok(_mapper.Map<List<FarmerBannerDTO>>(_bannerService.GetAdminBanner()));

        [HttpGet("Banner/GetLower")]
        public IActionResult GetLowerAll() => Ok(_mapper.Map<List<FarmerBannerDTO>>(_bannerService.GetLowerAll()));

        #endregion

        #region AdminMessages Methods

        [HttpGet("AdminMessage")]
        public IActionResult GetAllAdminMessages() => Ok(_mapper.Map<List<FarmerAdminMessageDTO>>(_adminMessageService.GetAll()));

        [HttpGet("AdminMessage/{id}")]
        public IActionResult GetByIdAdminMessages(int id) => Ok(_mapper.Map<FarmerAdminMessageDTO>(_adminMessageService.GetById(id)));

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
        public IActionResult GetAllSliders() => Ok(_mapper.Map<List<FarmerSliderDTO>>(_sliderService.GetAll()));

        [HttpGet("Slider/{id}")]
        public IActionResult GetByIdSliders(int id) => Ok(_mapper.Map<FarmerSliderDTO>(_bannerService.GetById(id)));

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

        #region AboutUs Methods

        [HttpGet("AboutUs")]
        public IActionResult GetAllAboutUs() => Ok(_mapper.Map<List<FarmerAboutUsDTO>>(_aboutUsService.GetAll()));

        [HttpGet("AboutUs/{id}")]
        public IActionResult GetByIdAboutUs(int id)
        {
            var aboutUs = _aboutUsService.GetById(id);
            if (aboutUs == null)
                throw new NullReferenceException($"{id} id'sine sahip hakkımızda kaydı bulunamadı.");

            return Ok(_mapper.Map<FarmerAboutUsDTO>(aboutUs));
        }

        [HttpPost("AboutUs")]
        public IActionResult CreateAboutUs(FarmerAboutUsDTO farmerAboutUsDTO)
        {
            _aboutUsService.Create(_mapper.Map<FarmerAboutUs>(farmerAboutUsDTO));

            return Ok();
        }

        [HttpPut("AboutUs")]
        public IActionResult UpdateAboutUs(FarmerAboutUsDTO farmerAboutUsDTO)
        {
            var aboutUs = _aboutUsService.GetById(farmerAboutUsDTO.Id);
            if (aboutUs == null)
                throw new NullReferenceException($"{farmerAboutUsDTO.Id} id'sine sahip hakkımızda kaydı bulunamadı.");

            _aboutUsService.Update(_mapper.Map<FarmerAboutUs>(farmerAboutUsDTO));

            return NoContent();
        }

        [HttpDelete("AboutUs")]
        public IActionResult DeleteAboutUs(FarmerAboutUsDTO farmerAboutUsDTO)
        {
            var aboutUs = _aboutUsService.GetById(farmerAboutUsDTO.Id);
            if (aboutUs == null)
                throw new NullReferenceException($"{farmerAboutUsDTO.Id} id'sine sahip hakkımızda kaydı bulunamadı.");

            _aboutUsService.Delete(_mapper.Map<FarmerAboutUs>(farmerAboutUsDTO));

            return Ok();
        }

        #endregion

        #region Carriers Methods

        [HttpGet("Carrier")]
        public IActionResult GetAllCarrier() => Ok(_mapper.Map<List<FarmerUserDTO>>(_userService.GetAllCarrier()));

        [HttpPost("Carrier")]
        public async Task<IActionResult> CreateCarrier(FarmerUserDTO farmerUserDTO, IFormFile file)
        {
            var farmerUser = _mapper.Map<FarmerUser>(farmerUserDTO);
            _userService.Create(await _userService.CreateCarrier(farmerUser, file));
            return Ok();
        }
        #endregion

        #region Security Information Methods

        [HttpGet("SecurityInformation")]
        public IActionResult GetAllSecurityInformation() => Ok(_mapper.Map<List<FarmerSecurityInformationDTO>>(_securityInformationService.GetAll()));

        [HttpGet("SecurityInformation/{id}")]
        public IActionResult GetByIdSecurityInformation(int id)
        {
            var security = _securityInformationService.GetById(id);
            if (security == null)
                throw new ArgumentNullException($"{id} id'sine sahip data bulunamadı !!!");

            return Ok(_mapper.Map<FarmerSecurityInformationDTO>(security));
        }

        [HttpPost("SecurityInformation")]
        public IActionResult CreateSecurityInformation(FarmerSecurityInformationDTO farmerSecurityInformationDTO)
        {
            _securityInformationService.Create(_mapper.Map<FarmerSecurityInformation>(farmerSecurityInformationDTO));
            return Ok();
        }

        [HttpPut("SecurityInformation")]
        public IActionResult UpdateSecurityInformation(FarmerSecurityInformationDTO farmerSecurityInformationDTO)
        {
            var result = _securityInformationService.GetById(farmerSecurityInformationDTO.Id);
            if (result == null)
                throw new ArgumentNullException($"{farmerSecurityInformationDTO.Id} id'sine sahip data bulunamadı !!!");

            _securityInformationService.Update(_mapper.Map<FarmerSecurityInformation>(farmerSecurityInformationDTO));
            return Ok();
        }

        [HttpDelete("SecurityInformation")]
        public IActionResult DeleteSecurityInformation(FarmerSecurityInformationDTO farmerSecurityInformationDTO)
        {
            var result = _securityInformationService.GetById(farmerSecurityInformationDTO.Id);
            if (result == null)
                throw new ArgumentNullException($"{farmerSecurityInformationDTO.Id} id'sine sahip data bulunamadı !!!");

            _securityInformationService.Delete(_mapper.Map<FarmerSecurityInformation>(farmerSecurityInformationDTO));
            return Ok();
        }

        #endregion

        #region Popular Products Methods

        [HttpPut("EditPopularUrun")]
        public IActionResult UpdateEditPopularUrun(int id, string active, string type)
        {
            var product = _productService.GetById(id);
            if (product == null)
                throw new ArgumentNullException($"{id} id'li ürün bulunamadı !");

            if (active == "yes")
                product.Situation = "EVET";

            else if (active == "no")
                product.Situation = "HAYIR";

            _productService.Update(product);

            return NoContent();
        }

        #endregion

    }
}
