using HappyFarmer.Business.Abstract;
using HappyFarmer.Entities;
using HappyFarmer.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HappyFarmer.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ISecurityInformationService _securityInformationService;
        private readonly IMultipleProductImagesService _multipleProductImagesService;
        public ProductController(IProductService productService, IMultipleProductImagesService multipleProductImagesService, ISecurityInformationService securityInformationService)
        {
            _productService = productService;
            _multipleProductImagesService = multipleProductImagesService;
            _securityInformationService = securityInformationService;
        }

        [HttpGet]
        public IActionResult GetAllProductDetails(int id)
        {
            #region Product Web Site Rules

            var security = _securityInformationService.GetAll();
            ViewBag.Information = security;

            #endregion

            ViewBag.AnimalsProduct = _productService.GetAll();

            List<string> category = new List<string>();
            ViewBag.ProductsMultipleImages = _multipleProductImagesService.GetByIdMultiImages(id); //ürünle ilişkili çoklu resim getir
            var response = _productService.GetById(id);

            var entitys = _productService.GetByIdWithCategories((int)id);

            foreach (var item in entitys.ProductCategories)
                 category.Add(item.Category.Name);

            ViewBag.CategoriesWith = category;
            ViewBag.GetAllProductDetails = response;

            var entity = new ProductModel()
            {
                AnimalAge = response.AnimalAge,
                AnnouncementDate = response.AnnouncementDate,
                City = response.City,
                Country = response.Country,
                Description = response.Description,
                FarmerDeclareType = response.FarmerDeclareType,
                Gender = response.Gender,
                Guarantee = response.Guarantee,
                Id = response.Id,
                ImageURL = response.ImageUrl,
                Name = response.Name,
                Neighborhood = response.Neighborhood,
                PermissionToSell = response.PermissionToSell,
                Price = response.Price,
                Situation = response.Situation,
                StockQty = response.StockQty,
                Swap = response.Swap,
                Title = response.Title,
                UserEMail = response.UserEMail,
                UserId = response.UserId,
                UserName = response.UserName,
                UserPhoneNumber = response.UserPhoneNumber,
                UserSurname = response.UserSurname,
                ProductComments = response.ProductComments != null ?
                (from pc in response.ProductComments
                 select new ProductComment
                 {
                     Comment = pc.Comment,
                     CommentDate = pc.CommentDate,
                     Email = pc.Email,
                     Name = pc.Name,
                     Id = pc.Id,
                     ProductId = response.Id,
                     Surname = pc.Surname,
                     UserId = pc.UserId,
                     WebSite = pc.WebSite
                 }).ToList():null
            };

            return View(entity);
        }

        #region Product Comments Methods

        [HttpPost]
        public IActionResult CreateProductComment(string Comment, string Name, string Surname, string Email, int productId)
        {
            var userId = HttpContext.Session.GetString("ActiveCustomerId");
            var entity = new ProductComment()
            {
                ProductId = productId,
                Comment = Comment,
                Name = Name,
                Surname = Surname,
                Email = Email,
                UserId = Convert.ToInt32(userId),
                CommentDate = DateTime.Now.ToLongDateString()
            };

            _productService.CreateComment(entity);
            return Redirect("/Product/GetAllProductDetails/" + productId);
        }

        #endregion
    }
}
