using AutoMapper;
using HappyFarmer.API.DTOs;
using HappyFarmer.Business.Abstract;
using HappyFarmer.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HappyFarmer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public AccountsController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(_mapper.Map<List<FarmerUserDTO>>(users));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var users = _userService.GetById(id);
            return Ok(_mapper.Map<FarmerUserDTO>(users));
        }

        [HttpPost]
        public IActionResult Create(FarmerUserDTO farmerUserDTO)
        {
            _userService.Create(_mapper.Map<FarmerUser>(farmerUserDTO));

            return Ok();
        }

        [HttpPut]
        public IActionResult Update(FarmerUserDTO farmerUserDTO)
        {
            _userService.Update(_mapper.Map<FarmerUser>(farmerUserDTO));

            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(FarmerUserDTO farmerUserDTO)
        {
            _userService.Delete(_mapper.Map<FarmerUser>(farmerUserDTO));
            return Ok();
        }

        [HttpGet("Customer")]
        public IActionResult GetAllCustomer()
        {
            var customers = _userService.GetAllCustomer();
            return Ok(_mapper.Map<List<FarmerUserDTO>>(customers));
        }

        [HttpGet("Carrier")]
        public IActionResult GetAllCarrier()
        {
            var customers = _userService.GetAllCarrier();
            return Ok(_mapper.Map<List<FarmerUserDTO>>(customers));
        }

        [HttpGet("OnlyCustomer")]
        public IActionResult GetAllOnlyCustomer()
        {
            var customers = _userService.GetAllOnlyCustomer();
            return Ok(_mapper.Map<List<FarmerUserDTO>>(customers));
        }

        [HttpPost("Login")]
        public IActionResult PasswordSignIn(string userEmail, string userPassword, int userType)
        {
            var customers = _userService.PasswordSignIn(userEmail,userPassword,userType);
            return Ok(customers);
        }

        [HttpPost("Find")]
        public IActionResult FindByEmail(string email)
        {
            var users = _userService.FindByEmail(email);

            return Ok(_mapper.Map<List<FarmerUserDTO>>(users));
        }

        [HttpGet("SoldProduct/{id}")]
        public IActionResult GetUserSoldProduct(int id)
        {
            var users = _userService.GetUserSoldProduct(id);
            return Ok(_mapper.Map<List<FarmerUserDTO>>(users));
        }
    }
}
