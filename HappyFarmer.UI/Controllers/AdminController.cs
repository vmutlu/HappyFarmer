using HappyFarmer.Business.Abstract;
using HappyFarmer.Entities;
using HappyFarmer.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HappyFarmer.UI.Controllers
{
    public class AdminController : Controller
    {
        #region Fields

        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ISliderService _sliderService;
        private readonly IAboutUsService _aboutUsService;
        private readonly IGeneralSettingsService _generalSettingsService;
        private readonly IAdminMessageService _adminMessageService;
        private readonly IUserService _userService;
        private readonly IMultipleProductImagesService _multipleProductImagesService;
        private readonly IGlobalMessageService _globalMessageService;
        private readonly IBlogService _blogService;
        private readonly ISecurityInformationService _securityInformationService;
        private readonly IBannerService _bannerService;

        #endregion

        #region Constructive method
        public AdminController(IProductService productService, ICategoryService categoryService, ISliderService sliderService, IAboutUsService aboutUsService, IGeneralSettingsService generalSettingsService, IAdminMessageService adminMessageService, IUserService userService, IMultipleProductImagesService multipleProductImagesService, IGlobalMessageService globalMessageService, IBlogService blogService, ISecurityInformationService securityInformationService, IBannerService bannerService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _sliderService = sliderService;
            _aboutUsService = aboutUsService;
            _generalSettingsService = generalSettingsService;
            _adminMessageService = adminMessageService;
            _userService = userService;
            _multipleProductImagesService = multipleProductImagesService;
            _globalMessageService = globalMessageService;
            _blogService = blogService;
            _securityInformationService = securityInformationService;
            _bannerService = bannerService;
        }

        #endregion

        public IActionResult Index()
        {
            #region Customer Declares Request

            List<int> type = new List<int>() { 10, 11, 12, 13, 14, 15, 20, 21, 22, 23, 24, 25, 26 };
            var declaresRequest = _productService.GetCustomerDeclares(type);
            ViewBag.CustomerDeclaresCount = declaresRequest.Count();

            #endregion

            #region Active Product Declares

            var entity = _productService.GetAll();
            ViewBag.ProductsCount = entity.Count();

            #endregion

            #region Get All Customer

            var getAllCustomer = _userService.GetAllCustomer();
            ViewBag.GetAllCustomer = getAllCustomer.Count();

            #endregion

            var slider = _sliderService.GetAll();
            if (HttpContext.Session.GetString("ActiveUser") != null)
            {
                ViewBag.User = HttpContext.Session.GetString("ActiveUser");
                TempData["UserId"] = HttpContext.Session.GetString("ActiveUserId");
            }

            //if (HttpContext.Request.Cookies[".HappyFarmer.Session"] == null)
            //{
            //    return RedirectToAction("Index","Account");
            //}

            else
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }
            return View(slider);
        }

        #region Banner Methods

        [HttpGet]
        public IActionResult GetAdminBanner()
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion

            var response = _bannerService.GetAdminBanner();
            var entity = (from i in response
                          select new BannerModel()
                          {
                              Active = i.Active,
                              Id = i.Id,
                              ImageURL = i.ImageURL,
                              LowerUpper = i.LowerUpper,
                              QueueNumber = i.QueueNumber
                          });
            return View(entity);
        }

        [HttpGet]
        public IActionResult CreateBanner()
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBanner(BannerModel model, IFormFile file)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var banner = new FarmerBanner()
            {
                Active = model.Active,
                LowerUpper = model.LowerUpper,
                QueueNumber = model.QueueNumber
            };

            if (file != null)
            {
                Random rastgele = new Random();
                string harfler = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZabcçdefgğhıijklmnoöprsştuüvyz";
                string uret = "";
                for (int i = 0; i < 6; i++)
                {
                    uret += harfler[rastgele.Next(harfler.Length)];
                }

                banner.ImageURL = uret + ".jpg";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\BannerImages", uret + ".jpg");

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream).ConfigureAwait(false);
                }
            }

            _bannerService.Create(banner);
            return Redirect("/Admin/GetAdminBanner");
        }

        [HttpGet]
        public IActionResult EditBanner(int id)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var entity = _bannerService.GetById(id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new BannerModel()
            {
                Id = entity.Id,
                ImageURL = entity.ImageURL,
                QueueNumber = entity.QueueNumber,
                LowerUpper = entity.LowerUpper,
                Active = entity.Active
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditBanner(BannerModel model, IFormFile file)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var entity = _bannerService.GetById(model.Id);
            if (entity == null)
            {
                return NotFound();
            }

            entity.Active = model.Active;
            entity.QueueNumber = model.QueueNumber;
            entity.LowerUpper = model.LowerUpper;

            if (file != null)
            {
                if (entity.ImageURL != null)
                {
                    //güncelleme yaparken önce resmi sil
                    var deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\BannerImages", entity.ImageURL);
                    FileInfo fileInfo = new FileInfo(deletePath);
                    if (fileInfo != null)
                    {
                        System.IO.File.Delete(deletePath);
                        fileInfo.Delete();
                    }
                }

                //daha sonra resim ekle
                Random rastgele = new Random();
                string harfler = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZabcçdefgğhıijklmnoöprsştuüvyz";
                string uret = "";
                for (int i = 0; i < 6; i++)
                {
                    uret += harfler[rastgele.Next(harfler.Length)];
                }

                entity.ImageURL = uret + ".jpg";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\BannerImages", uret + ".jpg");

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            _bannerService.Update(entity);

            return Redirect("/Admin/GetAdminBanner");
        }

        public IActionResult DeleteBanner(int bannerId)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var entity = _bannerService.GetById(bannerId);
            if (entity == null)
            {
                return NotFound();
            }

            if (entity.ImageURL != null)
            {
                //Slideri vt sil ve klasörde yer alan resmini de sil
                var sliderOldImages = entity.ImageURL; //resim yolunu al
                var deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\BannerImages", sliderOldImages);
                FileInfo fileInfo = new FileInfo(deletePath);
                if (fileInfo != null)
                {
                    System.IO.File.Delete(deletePath);
                    fileInfo.Delete();
                }
            }

            _bannerService.Delete(entity);

            return Redirect("/Admin/GetAdminBanner");
        }

        #endregion

        #region General Settings Methods

        public IActionResult GeneralSettings()
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var generalSettings = _generalSettingsService.GetAll();
            return View(generalSettings);
        }

        [HttpGet]
        public IActionResult CreateGeneralSetting()
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateGeneralSetting(GeneralSettingsModel generalSettingsModel, IFormFile file)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var farmerGeneralSettings = new FarmerGeneralSettings()
            {
                Title = generalSettingsModel.Title,
                Description = generalSettingsModel.Description,
                PhoneNumber = generalSettingsModel.PhoneNumber,
                Email = generalSettingsModel.Email,
                City = generalSettingsModel.City,
                District = generalSettingsModel.District,
                Address = generalSettingsModel.Address,
                Facebook = generalSettingsModel.Facebook,
                Instagram = generalSettingsModel.Instagram,
                Twitter = generalSettingsModel.Twitter
            };

            if (file != null)
            {
                Random rastgele = new Random();
                string harfler = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZabcçdefgğhıijklmnoöprsştuüvyz";
                string uret = "";
                for (int i = 0; i < 6; i++)
                {
                    uret += harfler[rastgele.Next(harfler.Length)];
                }

                farmerGeneralSettings.LogoUrl = uret + ".jpg";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\GeneralSettings", uret + ".jpg");

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream).ConfigureAwait(false);
                }
            }
            _generalSettingsService.Create(farmerGeneralSettings);

            return Redirect("/Admin/GeneralSettings");
        }

        [HttpGet]
        public IActionResult EditGeneralSetting(int id)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var entity = _generalSettingsService.GetById(id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new GeneralSettingsModel()
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                LogoUrl = entity.LogoUrl,
                PhoneNumber = entity.PhoneNumber,
                Email = entity.Email,
                City = entity.City,
                District = entity.District,
                Address = entity.Address,
                Facebook = entity.Facebook,
                Instagram = entity.Instagram,
                Twitter = entity.Twitter
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditGeneralSetting(GeneralSettingsModel generalSettingsModel, IFormFile file)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var entity = _generalSettingsService.GetById(generalSettingsModel.Id);
            if (entity == null)
            {
                return NotFound();
            }

            entity.Title = generalSettingsModel.Title;
            entity.Description = generalSettingsModel.Description;
            entity.PhoneNumber = generalSettingsModel.PhoneNumber;
            entity.Email = generalSettingsModel.Email;
            entity.City = generalSettingsModel.City;
            entity.District = generalSettingsModel.District;
            entity.Address = generalSettingsModel.Address;
            entity.Facebook = generalSettingsModel.Facebook;
            entity.Twitter = generalSettingsModel.Twitter;
            entity.Instagram = generalSettingsModel.Instagram;

            if (file != null)
            {
                if (entity.LogoUrl != null)
                {
                    //güncelleme yaparken önce resmi sil
                    var deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\GeneralSettings", entity.LogoUrl);
                    FileInfo fileInfo = new FileInfo(deletePath);
                    if (fileInfo != null)
                    {
                        System.IO.File.Delete(deletePath);
                        fileInfo.Delete();
                    }
                }

                //daha sonra resim ekle
                Random rastgele = new Random();
                string harfler = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZabcçdefgğhıijklmnoöprsştuüvyz";
                string uret = "";
                for (int i = 0; i < 6; i++)
                {
                    uret += harfler[rastgele.Next(harfler.Length)];
                }

                entity.LogoUrl = uret + ".jpg";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\GeneralSettings", uret + ".jpg");

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            _generalSettingsService.Update(entity);

            return Redirect("/Admin/GeneralSettings");
        }

        public IActionResult DeleteGeneralSetting(int id)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var entity = _generalSettingsService.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }

            if (entity.LogoUrl != null)
            {
                var sliderOldImages = entity.LogoUrl; //resim yolunu al
                var deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\GeneralSettings", sliderOldImages);
                FileInfo fileInfo = new FileInfo(deletePath);
                if (fileInfo != null)
                {
                    System.IO.File.Delete(deletePath);
                    fileInfo.Delete();
                }
            }

            _generalSettingsService.Delete(entity);

            return Redirect("/Admin/GeneralSettings");
        }

        #endregion

        #region AdminMessages

        public IActionResult AdminMessages()
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var adminMessages = _adminMessageService.GetAll();
            return View(adminMessages);
        }

        public IActionResult DeleteMessages(int id)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var entity = _adminMessageService.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }

            _adminMessageService.Delete(entity);
            return Redirect("/Admin/AdminMessages");
        }

        #endregion

        #region Slider

        public IActionResult Slider()
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var slider = _sliderService.GetAll(true);
            return View(slider);
        }

        [HttpGet]
        public IActionResult CreateSlider()
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSlider(SliderModel sliderModel, IFormFile file)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var slider = new FarmerSlider()
            {
                Name = sliderModel.Name,
                SequenceNumber = sliderModel.SequenceNumber,
                Situation = true,
                Url = sliderModel.Url,
            };

            if (file != null)
            {
                Random rastgele = new Random();
                string harfler = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZabcçdefgğhıijklmnoöprsştuüvyz";
                string uret = "";
                for (int i = 0; i < 6; i++)
                {
                    uret += harfler[rastgele.Next(harfler.Length)];
                }

                slider.ImagePath = uret + ".jpg";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\SliderImages", uret + ".jpg");

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream).ConfigureAwait(false);
                }
            }
            _sliderService.Create(slider);

            return Redirect("/Admin/Slider");
        }

        [HttpGet]
        public IActionResult EditSlider(int id)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var entity = _sliderService.GetById(id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new SliderModel()
            {
                Name = entity.Name,
                Id = entity.Id,
                SequenceNumber = entity.SequenceNumber,
                ImagePath = entity.ImagePath,
                Situation = entity.Situation,
                Url = entity.Url
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditSlider(SliderModel sliderModel, IFormFile file)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var entity = _sliderService.GetById(sliderModel.Id);
            if (entity == null)
            {
                return NotFound();
            }

            entity.Name = sliderModel.Name;
            entity.SequenceNumber = sliderModel.SequenceNumber;
            entity.Situation = sliderModel.Situation;
            entity.Url = sliderModel.Url;

            if (file != null)
            {
                if (entity.ImagePath != null)
                {
                    //güncelleme yaparken önce resmi sil
                    var deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\SliderImages", entity.ImagePath);
                    FileInfo fileInfo = new FileInfo(deletePath);
                    if (fileInfo != null)
                    {
                        System.IO.File.Delete(deletePath);
                        fileInfo.Delete();
                    }
                }

                //daha sonra resim ekle
                Random rastgele = new Random();
                string harfler = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZabcçdefgğhıijklmnoöprsştuüvyz";
                string uret = "";
                for (int i = 0; i < 6; i++)
                {
                    uret += harfler[rastgele.Next(harfler.Length)];
                }

                entity.ImagePath = uret + ".jpg";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\SliderImages", uret + ".jpg");

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            _sliderService.Update(entity);

            return Redirect("/Admin/Slider");
        }

        public IActionResult DeleteSlider(int sliderId)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var entity = _sliderService.GetById(sliderId);
            if (entity == null)
            {
                return NotFound();
            }

            if (entity.ImagePath != null)
            {
                //Slideri vt sil ve klasörde yer alan resmini de sil
                var sliderOldImages = entity.ImagePath; //resim yolunu al
                var deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\SliderImages", sliderOldImages);
                FileInfo fileInfo = new FileInfo(deletePath);
                if (fileInfo != null)
                {
                    System.IO.File.Delete(deletePath);
                    fileInfo.Delete();
                }
            }

            _sliderService.Delete(entity);

            return Redirect("/Admin/Slider");
        }

        #endregion

        #region AboutUs Methods

        [HttpGet]
        public IActionResult GetAboutUs()
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var aboutUs = _aboutUsService.GetAll();
            return View(aboutUs);
        }

        [HttpGet]
        public IActionResult CreateAboutUs()
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            return View();
        }

        [HttpPost]
        public IActionResult CreateAboutUs(AboutUsModel aboutUsModel)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var clearContent = RemoveHtml(aboutUsModel.Content);
            if (ModelState.IsValid)
            {
                var aboutUs = new FarmerAboutUs()
                {
                    Title = aboutUsModel.Title,
                    Content = clearContent,
                    Vision = aboutUsModel.Vision,
                    Mission = aboutUsModel.Mission,
                    VideoPath = aboutUsModel.VideoPath
                };

                _aboutUsService.Create(aboutUs);

                return Redirect("/Admin/GetAboutUs");
            }

            return View();
        }

        [HttpGet]
        public IActionResult EditAboutUs(int id)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var aboutUs = _aboutUsService.GetById(id);

            if (aboutUs != null)
            {
                var about = new AboutUsModel()
                {
                    Id = aboutUs.Id,
                    Title = aboutUs.Title,
                    Content = aboutUs.Content,
                    Vision = aboutUs.Vision,
                    Mission = aboutUs.Mission,
                    VideoPath = aboutUs.VideoPath
                };

                return View(about);
            }
            return View();
        }

        [HttpPost]
        public IActionResult EditAboutUs(AboutUsModel aboutUsModel)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var aboutUs = _aboutUsService.GetById(aboutUsModel.Id);
            var clearContent = RemoveHtml(aboutUsModel.Content);
            if (aboutUs != null)
            {
                aboutUs.Title = aboutUsModel.Title;
                aboutUs.Content = clearContent;
                aboutUs.Vision = aboutUsModel.Vision;
                aboutUs.Mission = aboutUsModel.Mission;
                aboutUs.VideoPath = aboutUsModel.VideoPath;

                _aboutUsService.Update(aboutUs);

                return Redirect("/Admin/GetAboutUs");
            }
            return View();
        }

        public IActionResult DeleteAboutUs(int id)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var about = _aboutUsService.GetById(id);

            if (about != null)
            {
                _aboutUsService.Delete(about);

                return Redirect("/Admin/GetAboutUs");
            }
            return View();
        }

        #endregion

        #region Products Methods

        [HttpGet]
        public IActionResult AdminProduct(string type)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion

            ViewBag.Type = type;
            var activeUserId = HttpContext.Session.GetString("ActiveUserType");
            if (!string.IsNullOrEmpty(type))
                return View(_productService.GetProductByType(type, Convert.ToInt32(activeUserId)));

            else
                return View();
        }

        [HttpGet]
        public IActionResult CreateProduct(string type)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            ViewBag.Type = type;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductModel productModel, IFormFile file, string type)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var userId = Convert.ToInt32(TempData["UserId"]);

            if (ModelState.IsValid)
            {
                var product = new FarmerProduct()
                {
                    Name = productModel.Name,
                    Description = productModel.Description,
                    City = productModel.City,
                    AnimalAge = productModel.AnimalAge,
                    AnnouncementDate = productModel.AnnouncementDate,
                    Country = productModel.Country,
                    FarmerDeclareType = productModel.FarmerDeclareType,
                    Guarantee = productModel.Guarantee,
                    Neighborhood = productModel.Neighborhood,
                    PermissionToSell = productModel.PermissionToSell,
                    Price = productModel.Price,
                    Situation = productModel.Situation,
                    Swap = productModel.Swap,
                    UserId = userId
                };

                if (file != null)
                {
                    Random rastgele = new Random();
                    string harfler = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZabcçdefgğhıijklmnoöprsştuüvyz";
                    string uret = "";
                    for (int i = 0; i < 6; i++)
                    {
                        uret += harfler[rastgele.Next(harfler.Length)];
                    }

                    product.ImageUrl = uret + ".jpg";
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\ProductImages", uret + ".jpg");

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream).ConfigureAwait(false);
                    }
                }
                _productService.Create(product);
                if (type != null)
                    return Redirect("/Admin/AdminProduct?type=" + type);

                else
                    return View();
            }

            else
                return View();
        }

        [HttpGet]
        public IActionResult EditProduct(int? id, string type)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            ViewBag.Type = type;
            if (id == null)
            {
                return NotFound();
            }

            var entity = _productService.GetByIdWithCategories((int)id);

            if (entity == null)
            {
                return NotFound();
            }
            ViewBag.ProductName = entity.Name;
            var model = new ProductModel()
            {
                Name = entity.Name,
                Id = entity.Id,
                Description = entity.Description,
                ImageURL = entity.ImageUrl,
                Price = (decimal)entity.Price,
                PermissionToSell = entity.PermissionToSell,
                FarmerDeclareType = entity.FarmerDeclareType,
                SelectedCategory = entity.ProductCategories.Select(i => i.Category).ToList()
            };

            ViewBag.Categories = _categoryService.GetAll();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductModel model, int[] categoryIds, IFormFile file, string type)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion

            if (ModelState.IsValid)
            {
                var entity = _productService.GetById(model.Id);
                if (entity == null)
                {
                    return NotFound();
                }

                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.Price = model.Price;
                entity.Situation = model.Situation;
                entity.Swap = model.Swap;
                entity.Guarantee = model.Guarantee;
                entity.FarmerDeclareType = model.FarmerDeclareType;
                entity.AnimalAge = model.AnimalAge;
                entity.AnnouncementDate = model.AnnouncementDate;
                entity.City = model.City;
                entity.Country = model.Country;
                entity.PermissionToSell = model.PermissionToSell;

                if (file != null)
                {
                    //güncelleme yaparken önce resmi sil
                    if (entity.ImageUrl != null)
                    {
                        var deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\ProductImages", entity.ImageUrl);
                        FileInfo fileInfo = new FileInfo(deletePath);
                        if (fileInfo != null)
                        {
                            System.IO.File.Delete(deletePath);
                            fileInfo.Delete();
                        }
                    }

                    Random rastgele = new Random();
                    string harfler = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZabcçdefgğhıijklmnoöprsştuüvyz";
                    string uret = "";
                    for (int i = 0; i < 6; i++)
                    {
                        uret += harfler[rastgele.Next(harfler.Length)];
                    }

                    entity.ImageUrl = uret + ".jpg";
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\ProductImages", uret + ".jpg");

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream).ConfigureAwait(false);
                    }
                }

                _productService.Update(entity, categoryIds);

                ViewBag.Categories = _categoryService.GetAll();

                if (type != null)
                    return Redirect("/Admin/AdminProduct/?type=" + type);

                else
                    return View();
            }

            return View();
        }

        public IActionResult DeleteProduct(int productId, string type)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var entity = _productService.GetById(productId);
            if (entity == null)
            {
                return NotFound();
            }

            if (entity.ImageUrl != null)
            {
                //güncelleme yaparken önce resmi sil
                var deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\ProductImages", entity.ImageUrl);
                FileInfo fileInfo = new FileInfo(deletePath);
                if (fileInfo != null)
                {
                    System.IO.File.Delete(deletePath);
                    fileInfo.Delete();
                }
            }

            _productService.Delete(entity);

            if (type != null)
                return Redirect("/Admin/AdminProduct?type=" + type);

            else
                return View();
        }


        #endregion

        #region Categories Methods

        [HttpGet]
        public IActionResult AdminCategory()
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            return View(new CategoryListModel()
            {
                Categories = _categoryService.GetAll()
            });
        }

        [HttpGet]
        public IActionResult EditCategory(int? id)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            if (id == null)
                return NotFound();

            var category = _categoryService.GetByIdWithProducts((int)id);

            if (category == null)
                return NotFound();

            ViewBag.CategoryName = category.Name;

            return View(new CategoryModel()
            {
                Id = category.Id,
                Name = category.Name,
                Products = category.ProductCategories.Select(i => i.Product).ToList()
            });
        }

        [HttpPost]
        public IActionResult EditCategory(FarmerCategory categoryModel)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var category = _categoryService.GetById(categoryModel.Id);
            if (category == null)
                return NotFound();

            category.Name = categoryModel.Name;

            _categoryService.Update(category);

            ViewBag.Message = "Kategori Güncelleme İşlemi Başarılı";

            return RedirectToAction("AdminCategory");
        }

        public IActionResult DeleteCategory(int categoryId)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var category = _categoryService.GetById(categoryId);
            if (category == null)
                return NotFound();

            _categoryService.Delete(category);

            ViewData["Message"] = "Kategori Silme İşlemi Başarılı";
            return RedirectToAction("AdminCategory");
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(CategoryModel categoryModel)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var category = new FarmerCategory()
            {
                Name = categoryModel.Name
            };

            _categoryService.Create(category);

            return RedirectToAction("AdminCategory");
        }


        [HttpPost]
        public IActionResult DeleteFromCategory(int categoryId, int productId)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            _categoryService.DeleteFromCategory(categoryId, productId);
            return Redirect("/admin/editcategory?id=" + categoryId);
        }
        #endregion

        #region Customer Declares Request

        [HttpGet]
        public IActionResult CustomerDeclareRequest()
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            List<int> type = new List<int>() { 10, 11, 12, 13, 14, 15 };
            return View(_productService.GetCustomerDeclares(type));
        }

        [HttpGet]
        public IActionResult CustomerEditProduct(int Id)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var entity = _productService.GetById(Id);
            if (entity == null)
            {
                return NotFound();
            }

            List<ProductModel> customerEdit = new List<ProductModel>()
            {
                new ProductModel()
                {
                Id = entity.Id,
                AnimalAge = entity.AnimalAge,
                Name = entity.Name,
                AnnouncementDate = entity.AnnouncementDate,
                City = entity.City,
                Country = entity.Country,
                Description = entity.Description,
                FarmerDeclareType = entity.FarmerDeclareType,
                Gender = entity.Gender,
                Guarantee = entity.Guarantee,
                ImageURL = entity.ImageUrl,
                Neighborhood = entity.Neighborhood,
                PermissionToSell = entity.PermissionToSell,
                Price = entity.Price,
                Situation = entity.Situation,
                StockQty = entity.StockQty,
                Title = entity.Title,
                Swap = entity.Swap,
                UserEMail = entity.UserEMail,
                UserName = entity.UserName,
                UserPhoneNumber = entity.UserPhoneNumber,
                UserSurname = entity.UserSurname,
                UserId = entity.UserId
                }
            };

            return View(customerEdit);
        }

        [HttpPost]
        public IActionResult CustomerEditProduct(ProductModel productModel)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var entity = _productService.GetById(productModel.Id);
            if (entity == null)
            {
                return NotFound();
            }

            entity.PermissionToSell = productModel.PermissionToSell;

            _productService.Update(entity);
            return Redirect("/Admin/CustomerDeclareRequest");
        }
        #endregion

        #region Carier Methods

        [HttpGet]
        public IActionResult Carrier()
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var getCarierCustomer = _userService.GetAllCarrier();
            return View(getCarierCustomer);
        }

        [HttpGet]
        public IActionResult AddCarrier()
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCarrier(LoginModel loginModel, IFormFile file)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var userCarier = new FarmerUser()
            {
                Name = loginModel.Name,
                Surname = loginModel.Surname,
                Email = loginModel.Email,
                PhoneNumber = loginModel.PhoneNumber,
                Address = loginModel.Address,
                Password = loginModel.Password,
                UserType = loginModel.UserType,
                City = loginModel.City,
                RecordData = loginModel.RecordData
            };

            if (file != null)
            {
                Random rastgele = new Random();
                string harfler = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZabcçdefgğhıijklmnoöprsştuüvyz";
                string uret = "";
                for (int i = 0; i < 6; i++)
                {
                    uret += harfler[rastgele.Next(harfler.Length)];
                }

                userCarier.ImageURL = uret + ".jpg";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\CustomerImages", uret + ".jpg");

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream).ConfigureAwait(false);
                }
            }
            _userService.Create(userCarier);

            return Redirect("/Admin/Carrier");
        }

        [HttpGet]
        public IActionResult EditCarrier(int id)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var entity = _userService.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }

            var carierUpdate = new LoginModel()
            {
                Name = entity.Name,
                Surname = entity.Surname,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                Address = entity.Address,
                Authority = entity.Authority,
                City = entity.City,
                ImageURL = entity.ImageURL,
                Password = entity.Password,
                RecordData = entity.RecordData,
                UserType = entity.UserType,
                UserState = entity.UserState,
                Id = entity.Id
            };

            return View(carierUpdate);
        }

        [HttpPost]
        public IActionResult EditCarrier(LoginModel loginModel)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var entity = _userService.GetById(loginModel.Id);

            if (entity == null)
            {
                return NotFound();
            }

            entity.UserState = loginModel.UserState; //sadece kullanıyı aktif pasif etme yetkisine sahip

            _userService.Update(entity);

            return Redirect("/Admin/Carrier");
        }

        #endregion

        #region Users Methods

        [HttpGet]
        public IActionResult Users()
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var getAllOnlyCustomer = _userService.GetAllOnlyCustomer();
            return View(getAllOnlyCustomer);
        }

        [HttpGet]
        public IActionResult AddOnlyUser()
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddOnlyUser(LoginModel loginModel, IFormFile file)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var users = new FarmerUser()
            {
                Name = loginModel.Name,
                Surname = loginModel.Surname,
                Email = loginModel.Email,
                PhoneNumber = loginModel.PhoneNumber,
                Address = loginModel.Address,
                Password = loginModel.Password,
                UserType = loginModel.UserType,
                City = loginModel.City,
                RecordData = loginModel.RecordData
            };

            if (file != null)
            {
                Random rastgele = new Random();
                string harfler = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZabcçdefgğhıijklmnoöprsştuüvyz";
                string uret = "";
                for (int i = 0; i < 6; i++)
                {
                    uret += harfler[rastgele.Next(harfler.Length)];
                }

                users.ImageURL = uret + ".jpg";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\CustomerImages", uret + ".jpg");

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream).ConfigureAwait(false);
                }
            }
            _userService.Create(users);

            return Redirect("/Admin/Users");
        }

        [HttpGet]
        public IActionResult EditOnlyUser(int id)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var entity = _userService.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }

            var usersUpdate = new LoginModel()
            {
                Name = entity.Name,
                Surname = entity.Surname,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                Address = entity.Address,
                Authority = entity.Authority,
                City = entity.City,
                ImageURL = entity.ImageURL,
                Password = entity.Password,
                RecordData = entity.RecordData,
                UserType = entity.UserType,
                UserState = entity.UserState,
                Id = entity.Id
            };

            return View(usersUpdate);
        }

        [HttpPost]
        public IActionResult EditOnlyUser(LoginModel loginModel)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var entity = _userService.GetById(loginModel.Id);

            if (entity == null)
            {
                return NotFound();
            }

            entity.UserState = loginModel.UserState; //sadece kullanıyı aktif pasif etme yetkisine sahip

            _userService.Update(entity);

            return Redirect("/Admin/Users");
        }

        #endregion

        #region Admin Multi Product Images

        [HttpGet]
        public IActionResult AdminAddMultipleProducts(int id)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var products = _productService.GetById(id);

            var product = new ProductModel()
            {
                Id = products.Id
            };

            return View(product);
        }

        List<string> list = new List<string>();
        [HttpPost]
        public async Task<IActionResult> AdminAddMultipleProducts(int id, IFormFile[] file, string type)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion

            foreach (var item in file)
            {
                Random rastgele = new Random();
                string harfler = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZabcçdefgğhıijklmnoöprsştuüvyz";
                string uret = "";
                for (int i = 0; i < 6; i++)
                {
                    uret += harfler[rastgele.Next(harfler.Length)];
                }

                list.Add(uret + ".jpg");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\ProductMultipleImages", uret + ".jpg");

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await item.CopyToAsync(stream).ConfigureAwait(false);
                }
            }

            _multipleProductImagesService.Create(id, list);
            return Redirect("/Admin/AdminProduct/?type=" + type);
        }

        [HttpGet]
        public IActionResult AdminGetMultipleProductImages(int id)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var ımages = _multipleProductImagesService.GetByIdMultiImages(id);
            return View(ımages);
        }

        [HttpGet]
        public async Task<IActionResult> AdminDeleteMultiImages(int id)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var images = _multipleProductImagesService.GetById(id);
            if (images == null)
            {
                return NotFound();
            }

            var imagesURL = images.ImageURL;

            var deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\ProductMultipleImages", imagesURL);
            FileInfo fileInfo = new FileInfo(deletePath);
            if (fileInfo != null)
            {
                System.IO.File.Delete(deletePath);
                fileInfo.Delete();
            }

            _multipleProductImagesService.Delete(images);

            return Redirect("/Admin/AdminGetMultipleProductImages/" + images.ProductId);
        }

        #endregion

        #region Popular Products Methods

        [HttpGet]
        public IActionResult EditPopularUrun(int id, string active, string type)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var entity = _productService.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }

            if (active == "yes")
                entity.Situation = "EVET";
            else if (active == "no")
                entity.Situation = "HAYIR";

            _productService.Update(entity);

            return Redirect("/Admin/AdminProduct?type=" + type);
        }

        #endregion

        #region Admin Global Message Methods


        [HttpGet]
        public async Task<IActionResult> AdminGetCarrierGlobalMessages()
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion

            var result = await _globalMessageService.GetCarrierGlobalMessages(true).ConfigureAwait(false); //admin oldugu için tüm kayıtları getir
            ViewBag.Users = _userService.GetAll();
            return View(result);
        }

        [HttpGet]
        public IActionResult AdminGlobalMessagesPermission()
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion

            var result = _globalMessageService.GetAll(true); //admin oldugu için tüm kayıtları getir
            ViewBag.Users = _userService.GetAll();
            return View(result);
        }

        [HttpGet]
        public IActionResult EditGlobalMessagePermission(int id, string active)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion

            var entity = _globalMessageService.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }

            if (active == "yes")
                entity.CheckStatus = true;
            else if (active == "no")
                entity.CheckStatus = false;

            _globalMessageService.Update(entity);

            return Redirect("/Admin/AdminGlobalMessagesPermission");
        }

        public IActionResult DeleteGlobalMessagePermission(int id)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var deleteGlobal = _globalMessageService.GetById(id);
            if (deleteGlobal == null)
            {
                return NotFound();
            }

            _globalMessageService.Delete(deleteGlobal);
            return Redirect("/Admin/AdminGlobalMessagesPermission");
        }
        #endregion

        #region Admin Blog Methods

        [HttpGet]
        public IActionResult Blog()
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var result = _blogService.GetAll();
            ViewBag.CreatedBlogUsers = _userService.GetAll();
            return View(result);
        }

        [HttpGet]
        public IActionResult CreateBlogArticle()
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var userId = HttpContext.Session.GetString("ActiveUserId");
            var Users = _userService.GetById(Convert.ToInt32(userId));
            ViewBag.UserDetails = Users.Name + " " + Users.Surname;
            ViewBag.UserId = Users.Id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlogArticle(BlogModel model, IFormFile file)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var cleatModelContent = RemoveHtml(model.Description);
            var userId = HttpContext.Session.GetString("ActiveUserId");

            if (ModelState.IsValid)
            {
                var farmerBlog = new FarmerBlog()
                {
                    Title = model.Title,
                    Description = cleatModelContent,
                    CreatedDate = model.CreatedDate,
                    QueueNumber = model.QueueNumber,
                    UserId = Convert.ToInt32(userId)
                };

                if (file != null)
                {
                    Random rastgele = new Random();
                    string harfler = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZabcçdefgğhıijklmnoöprsştuüvyz";
                    string uret = "";
                    for (int i = 0; i < 6; i++)
                    {
                        uret += harfler[rastgele.Next(harfler.Length)];
                    }

                    farmerBlog.ImagePath = uret + ".jpg";
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\BlogImages", uret + ".jpg");

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream).ConfigureAwait(false);
                    }
                }

                _blogService.Create(farmerBlog);

                return Redirect("/Admin/Blog");
            }

            else
                return View();
        }

        [HttpGet]
        public IActionResult EditBlogArticle(int id)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion

            var userId = HttpContext.Session.GetString("ActiveUserId");
            var Users = _userService.GetById(Convert.ToInt32(userId));
            ViewBag.UserDetails = Users.Name + " " + Users.Surname;
            ViewBag.UserId = Users.Id;

            if (id == 0)
            {
                return NotFound();
            }

            var entity = _blogService.GetById(id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new BlogModel()
            {
                Id = entity.Id,
                CreatedDate = entity.CreatedDate,
                Description = entity.Description,
                ImagePath = entity.ImagePath,
                QueueNumber = entity.QueueNumber,
                Title = entity.Title,
                UserId = entity.UserId
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditBlogArticle(BlogModel model, IFormFile file)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion

            var cleatModelContent = RemoveHtml(model.Description);
            var entity = _blogService.GetById(model.Id);

            if (entity == null)
            {
                return NotFound();
            }

            entity.Title = model.Title;
            entity.Description = cleatModelContent;
            entity.CreatedDate = model.CreatedDate;
            entity.QueueNumber = model.QueueNumber;
            entity.UserId = model.UserId;

            if (file != null)
            {
                //güncelleme yaparken önce resmi sil
                if (entity.ImagePath != null)
                {
                    var deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\BlogImages", entity.ImagePath);
                    FileInfo fileInfo = new FileInfo(deletePath);
                    if (fileInfo != null)
                    {
                        System.IO.File.Delete(deletePath);
                        fileInfo.Delete();
                    }
                }

                Random rastgele = new Random();
                string harfler = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZabcçdefgğhıijklmnoöprsştuüvyz";
                string uret = "";
                for (int i = 0; i < 6; i++)
                {
                    uret += harfler[rastgele.Next(harfler.Length)];
                }

                entity.ImagePath = uret + ".jpg";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\BlogImages", uret + ".jpg");

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream).ConfigureAwait(false);
                }
            }

            _blogService.Update(entity);

            return Redirect("/Admin/Blog");
        }

        public IActionResult DeleteBlogArticle(int Id)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var entity = _blogService.GetById(Id);

            if (entity == null)
            {
                return NotFound();
            }

            if (entity.ImagePath != null)
            {
                //güncelleme yaparken önce resmi sil
                var deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\BlogImages", entity.ImagePath);
                FileInfo fileInfo = new FileInfo(deletePath);
                if (fileInfo != null)
                {
                    System.IO.File.Delete(deletePath);
                    fileInfo.Delete();
                }
            }

            _blogService.Delete(entity);

            return Redirect("/Admin/Blog");
        }

        #endregion

        #region Security Information Methods

        [HttpGet]
        public IActionResult GeneralInformation()
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            var entity = _securityInformationService.GetAll();
            return View(entity);
        }

        [HttpGet]
        public IActionResult CreateInformation()
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            return View();
        }

        [HttpPost]
        public IActionResult CreateInformation(SecurityInformationModel model)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            if (model != null)
            {
                var clearSSS = RemoveHtml(model.FAQ);
                var clearPrivacyPolicy = RemoveHtml(model.PrivacyPolicy);
                var clearAdversitingRule = RemoveHtml(model.AdversitingRule);
                var clearTermsOfUse = RemoveHtml(model.TermsOfUse);

                var entity = new FarmerSecurityInformation()
                {
                    AdversitingRule = clearAdversitingRule,
                    FAQ = clearSSS,
                    PrivacyPolicy = clearPrivacyPolicy,
                    TermsOfUse = clearTermsOfUse
                };

                _securityInformationService.Create(entity);

                return Redirect("/Admin/GeneralInformation");
            }
            return View();
        }

        [HttpGet]
        public IActionResult EditInformation(int id)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            if (id > 0)
            {
                var entity = _securityInformationService.GetById(id);

                if (entity != null)
                {
                    var about = new SecurityInformationModel()
                    {
                        Id = entity.Id,
                        AdversitingRule = entity.AdversitingRule,
                        FAQ = entity.FAQ,
                        PrivacyPolicy = entity.PrivacyPolicy,
                        TermsOfUse = entity.TermsOfUse
                    };

                    return View(about);
                }
                return View();
            }
            return View();
        }

        [HttpPost]
        public IActionResult EditInformation(SecurityInformationModel model)
        {
            #region Admin Login Kontrolü

            if (HttpContext.Session.GetString("ActiveUser") == null)
            {
                TempData["Message"] = "Erişiminiz kısıtlandı !!! Admin girişi yapmadan admin paneline erişemezsiniz.";
                return RedirectToAction("Index", new RouteValueDictionary(
                    new
                    {
                        Controller = "Account",
                        Action = "Index",
                        message = TempData["Message"]
                    }));
            }

            #endregion
            if (model != null)
            {
                var entity = _securityInformationService.GetById(model.Id);

                var clearSSS = RemoveHtml(model.FAQ);
                var clearPrivacyPolicy = RemoveHtml(model.PrivacyPolicy);
                var clearAdversitingRule = RemoveHtml(model.AdversitingRule);
                var clearTermsOfUse = RemoveHtml(model.TermsOfUse);

                if (entity != null)
                {
                    entity.FAQ = clearSSS;
                    entity.AdversitingRule = clearAdversitingRule;
                    entity.PrivacyPolicy = clearPrivacyPolicy;
                    entity.TermsOfUse = clearTermsOfUse;

                    _securityInformationService.Update(entity);
                    return Redirect("/Admin/GeneralInformation");
                }
                return View();
            }
            return View();
        }

        #endregion

        public static string RemoveHtml(string text)
        {
            return Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
        }
    }
}
