using HappyFarmer.Business.Abstract;
using HappyFarmer.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HappyFarmer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private readonly IMenuService _menuService;
        public MenusController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = _menuService.GetAll();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = _menuService.GetById(id);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(FarmerMenu farmerMenu)
        {
            _menuService.Update(farmerMenu);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Add(FarmerMenu farmerMenu)
        {
            _menuService.Create(farmerMenu);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(FarmerMenu farmerMenu)
        {
            _menuService.Delete(farmerMenu);
            return Ok();
        }
    }
}
