using AutoMapper;
using HappyFarmer.Business.Abstract;
using HappyFarmer.Entities;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HappyFarmer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarriersController : ControllerBase
    {
        #region Fields

        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        #endregion

        public CarriersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// User tablosundaki tüm kullanıcıları getirir.Admin, Normal Kullanıcılar ve Nakliyecileri
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll() => Ok(_userService.GetAll());

        /// <summary>
        /// User tablosundaki kullanıcıları Id 'lerine göre getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok(_userService.GetById(id));

        /// <summary>
        /// Kullanıcı Ekleme endpointi. Eklenecek kullanıcı nesnesinin FarmerUserDTO alır ve FarmerUser nesnesine mapler.
        /// </summary>
        /// <param name="farmer"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(FarmerUser farmer)
        {
            _userService.Create(farmer);
            return Ok();
        }

        /// <summary>
        /// Kullanıcı Güncelleme endpointi. Güncellenecek kullanıcı nesnesinin FarmerUserDTO alır ve FarmerUser nesnesine mapler.
        /// </summary>
        /// <param name="farmer"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update(FarmerUser farmer)
        {
            if (farmer.Id <= 0)
                throw new Exception("Güncelleme işlemi yapabilmek için lütfen kullanıcı Id değerini gönderiniz");

            _userService.Update(farmer);
            return Ok();
        }

        /// <summary>
        /// Kullanıcı silme işlemi için FarmerUserDTO Nesnesi alır. Nesne içerisinde Id degeri gönderilmezse istisna fırlatılır.
        /// </summary>
        /// <param name="farmer"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(FarmerUser farmer)
        {
            if (farmer.Id <= 0)
                throw new Exception("Silme işlemi yapabilmek için lütfen kullanıcı Id değerini gönderiniz");

            _userService.Delete(farmer);
            return Ok();
        }

        /// <summary>
        /// Kullanıcıları Email adresine göre arama işlemi. Kullanıcılar kayıt işleminde girdikleri email adresleri vt kayıtlı ise aynı email adresi ile tekrar kayıt olamaması için.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("Carrier")]
        public IActionResult FindByEmail(FarmerUser user)
        {
            string email = user.Email;
            return Ok(_userService.FindByEmail(email));
        }

        /// <summary>
        /// User tablosunda sadece kullanıcıları getiren endpoint. Admini ve nakliyeci kişileri getirmez.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllCustomer")]
        public IActionResult GetAllCustomer() => Ok(_userService.GetAllCustomer());

        /// <summary>
        /// User tablosunda sadece nakliyecileri getirir. Normal kullanıcı ve adminleri ge
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllCarrier")]
        public IActionResult GetAllCarrier() => Ok(_userService.GetAllCarrier());

        /// <summary>
        /// User tablosundan sadece admin olanları getirir.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllOnlyCustomer")]
        public IActionResult GetAllOnlyCustomer() => Ok(_userService.GetAllOnlyCustomer());
    }
}
