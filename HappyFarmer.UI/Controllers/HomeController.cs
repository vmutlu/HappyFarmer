using HappyFarmer.Business.Abstract;
using HappyFarmer.Entities;
using HappyFarmer.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace HappyFarmer.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly IProductService _productService;
        private readonly IAboutUsService _aboutUsService;
        private readonly IBlogService _blogService;
        private readonly IUserService _userService;
        private readonly ISecurityInformationService _securityInformationService;
        private readonly IBannerService _bannerService;
        private readonly IOrderService _orderService;
        public HomeController(ISliderService sliderService, IProductService productService, IAboutUsService aboutUsService, IBlogService blogService, IUserService userService, ISecurityInformationService securityInformationService, IBannerService bannerService, IOrderService orderService)
        {
            _sliderService = sliderService;
            _productService = productService;
            _aboutUsService = aboutUsService;
            _blogService = blogService;
            _userService = userService;
            _securityInformationService = securityInformationService;
            _bannerService = bannerService;
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            #region Alt Banner

            ViewBag.LowerBanner = _bannerService.GetLowerAll();

            #endregion


            const int pageSize = 6;
            const int page = 1;
            const int pageStandOut = 1; //öne çıkan ürünler
            var slider = _sliderService.GetAll();

            ViewBag.Success = TempData["Success"];
            ViewBag.Products = _productService.GetAll(page, pageSize);
            ViewBag.PopularProducts = _productService.GetPopularProduct(pageStandOut, pageSize);
            ViewBag.Blogs = _blogService.GetAll();
            ViewBag.BlogsUsers = _userService.GetAll();

            ViewBag.Banner = _bannerService.GetAll();

            #region Günün Ürünü

            var todayProduct = _orderService.GetPopularProduct();
            var productDetails = _productService.GetById(todayProduct);
            var liste = new List<FarmerProduct>() { productDetails };
            ViewBag.TodayProduct = liste;

            #endregion


            var entity = _productService.GetAll();
            ViewBag.AnimalsProduct = entity;

            return View(slider);
        }

        [HttpPost]
        public IActionResult Index(int page, int pageStandOut, int? productId = 0)
        {
            #region Alt Banner

            ViewBag.LowerBanner = _bannerService.GetLowerAll();

            #endregion

            const int pageSize = 6;
            var slider = _sliderService.GetAll();

            ViewBag.Products = _productService.GetAll(page, pageSize);

            ViewBag.PageCount = page;
            ViewBag.PageStandOutCount = pageStandOut;
            ViewBag.PopularProducts = _productService.GetPopularProduct(pageStandOut, pageSize);

            ViewBag.Blogs = _blogService.GetAll();
            ViewBag.BlogsUsers = _userService.GetAll();

            ViewBag.Banner = _bannerService.GetAll();

            #region Günün Ürünü

            var todayProduct = _orderService.GetPopularProduct();
            var productDetails = _productService.GetById(todayProduct);
            var liste = new List<FarmerProduct>() { productDetails };
            ViewBag.TodayProduct = liste;

            #endregion

            var entity = _productService.GetAll();
            ViewBag.AnimalsProduct = entity;
            return View(slider);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult About()
        {
            var aboutUsWebSite = _aboutUsService.GetAll();

            var aboutUs = (from i in aboutUsWebSite
                           select new AboutUsModel
                           {
                               Id = i.Id,
                               Content = i.Content,
                               Mission = i.Mission,
                               Title = i.Title,
                               VideoPath = i.VideoPath,
                               Vision = i.Vision
                           }).FirstOrDefault();

            ViewBag.Blogs = _blogService.GetAll();

            return View(aboutUs);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        #region Blog Methods

        [HttpGet]
        public IActionResult GetBlogArticle()
        {
            const int page = 1;
            var blogUserId = 0;
            const int pageSize = 3;
            var result = _blogService.GetAll((int)page, pageSize);

            foreach (var item in result)
                blogUserId = item.UserId;

            var user = _userService.GetById(blogUserId);

            ViewBag.BlogUsers = user.Name + " " + user.Surname;

            return View(result);
        }

        [HttpPost]
        public IActionResult GetBlogArticle(int? page = 1)
        {
            List<FarmerBlog> result = new List<FarmerBlog>();
            var blogUserId = 0;
            const int pageSize = 3;
            result = _blogService.GetAll(page, pageSize);
            ViewBag.PageCount = page;

            foreach (var item in result)
                blogUserId = item.UserId;

            if (blogUserId > 0)
            {
                var user = _userService.GetById(blogUserId);

                ViewBag.BlogUsers = user.Name + " " + user.Surname;
            }

            else
            {
                var value = page--;
                result = _blogService.GetAll(value, pageSize);
            }

            return View(result);
        }

        [HttpGet]
        public IActionResult GetBlogArticleDetails(int id)
        {
            var userId = HttpContext.Session.GetString("ActiveCustomerId");
            var response = _blogService.GetById(id);

            if (response == null)
                return NotFound();

            var entity = new BlogModel()
            {
                Id = response.Id,
                CreatedDate = response.CreatedDate,
                Description = response.Description,
                ImagePath = response.ImagePath,
                QueueNumber = response.QueueNumber,
                Title = response.Title,
                UserId = response.UserId,
                Comments = response.Comments != null ? (from i in response.Comments
                                                        select new FarmerComment
                                                        {
                                                            BlogId = response.Id,
                                                            Email = i.Email,
                                                            Name = i.Name,
                                                            Surname = i.Surname,
                                                            UserId = i.UserId,
                                                            WebSite = i.WebSite,
                                                            CommentDate = i.CommentDate,
                                                            Comment = i.Comment
                                                        }).ToList() : null
            };

            var blogUserId = 0;
            var result = _blogService.GetAll();

            foreach (var item in result)
                blogUserId = item.UserId;

            var user = _userService.GetById(blogUserId);

            ViewBag.BlogUsers = user.Name + " " + user.Surname;
            ViewBag.GetAllBlogs = result;
            ViewBag.ActiveWhoUser = HttpContext.Session.GetString("ActiveCustomerId");
            return View(entity);
        }

        #endregion

        #region Security Information Methods

        [HttpGet]
        public IActionResult GetInformation() => View((from i in _securityInformationService.GetAll()
                                                       select new SecurityInformationModel
                                                       {
                                                           Id = i.Id,
                                                           AdversitingRule = i.AdversitingRule,
                                                           FAQ = i.FAQ,
                                                           PrivacyPolicy = i.PrivacyPolicy,
                                                           TermsOfUse = i.TermsOfUse
                                                       }));

        #endregion

        #region User Blog Comments Methods

        [HttpPost]
        public IActionResult CreateComment(string Comment, string Name, string Surname, string Email, string WebSite, int blogId)
        {
            var userId = HttpContext.Session.GetString("ActiveCustomerId");
            var entity = new FarmerComment()
            {
                BlogId = blogId,
                Comment = Comment,
                Name = Name,
                Surname = Surname,
                Email = Email,
                UserId = Convert.ToInt32(userId),
                WebSite = WebSite,
                CommentDate = DateTime.Now.ToShortDateString()
            };

            _blogService.CreateComment(entity);
            return Redirect("/Home/GetBlogArticleDetails/" + blogId);

        }

        #endregion
    }
}
