using HappyFarmer.Business.Abstract;
using HappyFarmer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HappyFarmer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService) =>
            _userService = userService;

        [HttpGet]
        public IActionResult GetAll() => Ok(_userService.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok(_userService.GetById(id));

        [HttpPost]
        public IActionResult Add(FarmerUser user)
        {
            _userService.Create(user);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(FarmerUser user)
        {
            _userService.Update(user);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(FarmerUser user)
        {
            _userService.Delete(user);
            return Ok();
        }

        [HttpPost("PasswordSignIn")]
        public IActionResult PasswordSignIn(FarmerUser user)
        {
            var userEmail = user.Email;
            var userPassword = user.Password;
            var userType = user.UserType;
            var response = _userService.PasswordSignIn(userEmail, userPassword, userType);

            if (!response)
                return NotFound();
            else
                return Ok(response);
        }
    }
}
