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
        #region Fields

        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        #endregion

        public AccountsController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// tüm kullanıcıları getirme endpointi
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll() => Ok(_mapper.Map<List<FarmerUserDTO>>(_userService.GetAll()));

        /// <summary>
        /// id 'ye göre kullanıcı getirme endpointi
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok(_mapper.Map<FarmerUserDTO>(_userService.GetById(id)));

        /// <summary>
        /// kullanıcı ekleme endpointi
        /// </summary>
        /// <param name="farmerUserDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(FarmerUserDTO farmerUserDTO)
        {
            _userService.Create(_mapper.Map<FarmerUser>(farmerUserDTO));

            return Ok();
        }

        /// <summary>
        /// kullanıcı güncelleme endpointi
        /// </summary>
        /// <param name="farmerUserDTO"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update(FarmerUserDTO farmerUserDTO)
        {
            _userService.Update(_mapper.Map<FarmerUser>(farmerUserDTO));

            return NoContent();
        }

        /// <summary>
        /// kullanıcı silme endpointi
        /// </summary>
        /// <param name="farmerUserDTO"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(FarmerUserDTO farmerUserDTO)
        {
            _userService.Delete(_mapper.Map<FarmerUser>(farmerUserDTO));
            return Ok();
        }

        /// <summary>
        /// sistemde kayotlı tüm müşteriler
        /// </summary>
        /// <returns></returns>
        [HttpGet("Customer")]
        public IActionResult GetAllCustomer() => Ok(_mapper.Map<List<FarmerUserDTO>>(_userService.GetAllCustomer()));

        /// <summary>
        /// sistemde kayıtlı tüm nakliyeciler
        /// </summary>
        /// <returns></returns>
        [HttpGet("Carrier")]
        public IActionResult GetAllCarrier() => Ok(_mapper.Map<List<FarmerUserDTO>>(_userService.GetAllCarrier()));

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("OnlyCustomer")]
        public IActionResult GetAllOnlyCustomer() => Ok(_mapper.Map<List<FarmerUserDTO>>(_userService.GetAllOnlyCustomer()));

        /// <summary>
        /// kullanıcı giriş dogrulama endpointi
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="userPassword"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public IActionResult PasswordSignIn(string userEmail, string userPassword, int userType) => Ok(_userService.PasswordSignIn(userEmail, userPassword, userType));

        /// <summary>
        /// kulllanıcı mail adresi sistemde varmı endpointi
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost("Find")]
        public IActionResult FindByEmail(string email) => Ok(_mapper.Map<List<FarmerUserDTO>>(_userService.FindByEmail(email)));

        [HttpGet("SoldProduct/{id}")]
        public IActionResult GetUserSoldProduct(int id) => Ok(_mapper.Map<List<FarmerUserDTO>>(_userService.GetUserSoldProduct(id)));
    }
}
