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
    }
}
