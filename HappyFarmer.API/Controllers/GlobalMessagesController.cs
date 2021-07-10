using HappyFarmer.Business.Abstract;
using HappyFarmer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HappyFarmer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlobalMessagesController : ControllerBase
    {
        #region Fields
        private readonly IGlobalMessageService _globalMessageService;
        #endregion
        public GlobalMessagesController(IGlobalMessageService globalMessageService) =>
            _globalMessageService = globalMessageService;

        /// <summary>
        /// Global yorumları listeleyen endpointim
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll() => Ok(_globalMessageService.GetAll());

        /// <summary>
        /// İd 'ye göre global mesaj getiren endpointim
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok(_globalMessageService.GetById(id));

        /// <summary>
        /// Belirlenen id sahip mesajı atan kullanıcı getiren endpointim
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("User/{id}")]
        public IActionResult GetNameById(int id) => Ok(_globalMessageService.GetNameById(id));

        /// <summary>
        /// global mesaj ekleyen endpointim
        /// </summary>
        /// <param name="globalMessage"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add(FarmerGlobalMessage globalMessage)
        {
            _globalMessageService.Create(globalMessage);
            return Ok();
        }

        /// <summary>
        /// global mesajı güncellemeye yarayan endpointim
        /// </summary>
        /// <param name="globalMessage"></param>
        /// <returns></returns>
        [HttpPut]
        public  IActionResult Update(FarmerGlobalMessage globalMessage)
        {
            _globalMessageService.Update(globalMessage);
            return Ok();
        }

        /// <summary>
        /// Global mesajı silmeye yarayan endpointim
        /// </summary>
        /// <param name="globalMessage"></param>
        /// <returns></returns>
        [HttpDelete]
        public  IActionResult Delete(FarmerGlobalMessage globalMessage)
        {
            _globalMessageService.Delete(globalMessage);
            return Ok();
        }
    }
}
