using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HappyFarmer.Business.Abstract;
using HappyFarmer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HappyFarmer.UI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IBannerService _bannerService;
        private readonly ICityService _cityService;
        public CategoryController(ICategoryService categoryService, IProductService productService, IBannerService bannerService, ICityService cityService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _bannerService = bannerService;
            _cityService = cityService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CategoryWithProduct(string type, int? lowPrice, int? topPrice, string? City, string? Country, string? Neighborhood)
        {
            #region Kategoriye göre veri tabanında o kategoriye ait kaç ürün var 

             ViewBag.CategoryWithCount = _productService.GetCategoryWithCount();

            #endregion

            ViewBag.AnimalsProduct = _productService.GetAll();
            ViewBag.ProductCities = _productService.GetCityProduct();

            var activeUserId = HttpContext.Session.GetString("ActiveUserType");

            ViewBag.LowerBanner = _bannerService.GetLowerAll();
            ViewBag.CityWithFilter = _cityService.GetAll();

            if (lowPrice > -1 && topPrice > -1)
            {
                var search = _productService.FilterByPrice((int)lowPrice, (int)topPrice, type);
                ViewBag.Type = type;
                return View(search);
            }

            #region il, ilçe ve mahalleye göre arama

            if (City != null && Country != null && Neighborhood != null)
                return View(_productService.FilterByRegion(type, City, Country, Neighborhood));
            if (City != null && Country != null)
                return View(_productService.FilterByRegion(type, City, Country, null));
            if (City != null && Country == null && Neighborhood == null)
                return View(_productService.FilterByRegion(type, City, null, null));

            #endregion

            else
            {
                ViewBag.Type = type;
                return View(_productService.GetProductByType(type, Convert.ToInt32(activeUserId)));
            }
        }

        [HttpPost]
        public IActionResult FindForProductsByCategoryType(string type, string searchText)
        {
            #region Kategoriye göre veri tabanında o kategoriye ait kaç ürün var 

            ViewBag.CategoryWithCount = _productService.GetCategoryWithCount();

            #endregion

            ViewBag.LowerBanner = _bannerService.GetLowerAll();
            ViewBag.AnimalsProduct = _productService.GetAll();
            ViewBag.CityWithFilter = _cityService.GetAll();

            var activeUserId = HttpContext.Session.GetString("ActiveUserType");
            if (type != "Kategori Seçiniz" && !string.IsNullOrEmpty(type))
                return View(_productService.FindForProductsByCategoryType(type, searchText));

            else if (type != "Kategori Seçiniz" && string.IsNullOrEmpty(searchText))
                return View(_productService.GetProductByType(type, Convert.ToInt32(activeUserId)));

            else if (type == "Kategori Seçiniz" && string.IsNullOrEmpty(searchText) || type == "Kategori Seçiniz" && !string.IsNullOrEmpty(searchText))
                return View(_productService.GetAll());

            else
                        ViewBag.Error = "Listelenecek Ürün Bulunamadı !!!";
            return View();
        }

    }
}
