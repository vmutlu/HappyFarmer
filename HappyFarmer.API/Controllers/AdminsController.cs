using HappyFarmer.API.DTOs;
using HappyFarmer.Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HappyFarmer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {

        private readonly IProductService _productService;
        public AdminsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _productService.GetAll();

            var productDTO = products != null ?
                (from i in products
                 select new FarmerProductDTO
                 {
                     AnimalAge = i.AnimalAge,
                     AnnouncementDate = i.AnnouncementDate,
                     City = i.City,
                     Country = i.Country,
                     Description = i.Country,
                     FarmerDeclareType = i.FarmerDeclareType,
                     Gender = i.Gender,
                     Guarantee = i.Guarantee,
                     Id = i.Id,
                     ImageUrl = i.ImageUrl,
                     Name = i.Name,
                     Neighborhood = i.Neighborhood,
                     PermissionToSell = i.PermissionToSell,
                     Price = i.Price,
                     Situation = i.Situation,
                     StockQty = i.StockQty,
                     Swap = i.Swap,
                     Title = i.Title,
                     UserEMail = i.UserEMail,
                     UserId = i.UserId,
                     UserName = i.UserName,
                     UserPhoneNumber = i.UserPhoneNumber,
                     UserSurname = i.UserSurname
                 }).ToList() : null;

            return Ok(productDTO);
        }

        [HttpGet("{id}")]
        public IActionResult GetByIdProducts(int id)
        {
            var products = _productService.GetById(id);

            var productDTO =  new FarmerProductDTO
                 {
                     AnimalAge = products.AnimalAge,
                     AnnouncementDate = products.AnnouncementDate,
                     City = products.City,
                     Country = products.Country,
                     Description = products.Country,
                     FarmerDeclareType = products.FarmerDeclareType,
                     Gender = products.Gender,
                     Guarantee = products.Guarantee,
                     Id = products.Id,
                     ImageUrl = products.ImageUrl,
                     Name = products.Name,
                     Neighborhood = products.Neighborhood,
                     PermissionToSell = products.PermissionToSell,
                     Price = products.Price,
                     Situation = products.Situation,
                     StockQty = products.StockQty,
                     Swap = products.Swap,
                     Title = products.Title,
                     UserEMail = products.UserEMail,
                     UserId = products.UserId,
                     UserName = products.UserName,
                     UserPhoneNumber = products.UserPhoneNumber,
                     UserSurname = products.UserSurname
                 };

            return Ok(productDTO);
        }
    }
}
