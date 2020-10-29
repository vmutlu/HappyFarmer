using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HappyFarmer.Business.Abstract;
using HappyFarmer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HappyFarmer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult>GetAll()
        {
            var response = _userService.GetAll();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>GetById(int id)
        {
            var response = _userService.GetById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add(FarmerUser user)
        {
            _userService.Create(user);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(FarmerUser user)
        {
            _userService.Update(user);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(FarmerUser user)
        {
            _userService.Delete(user);
            return Ok();
        }

        [HttpPost("PasswordSignIn")]
        public async Task<IActionResult> PasswordSignIn(FarmerUser user)
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
