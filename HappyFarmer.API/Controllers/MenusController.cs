using HappyFarmer.Business.Abstract;
using HappyFarmer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HappyFarmer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private readonly IMenuService _menuService;
        public MenusController(IMenuService menuService) =>
            _menuService = menuService;

        [HttpGet]
        public IActionResult GetAll() => Ok(_menuService.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok(_menuService.GetById(id));

        [HttpPut]
        public IActionResult Update(FarmerMenu farmerMenu)
        {
            _menuService.Update(farmerMenu);
            return Ok();
        }

        [HttpPost]
        public IActionResult Add(FarmerMenu farmerMenu)
        {
            _menuService.Create(farmerMenu);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(FarmerMenu farmerMenu)
        {
            _menuService.Delete(farmerMenu);
            return Ok();
        }
    }
}
