using HappyFarmer.Business.Abstract;
using HappyFarmer.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HappyFarmer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlobalMessagesController : ControllerBase
    {
        #region Fields
        private readonly IGlobalMessageService _globalMessageService;
        #endregion
        public GlobalMessagesController(IGlobalMessageService globalMessageService)
        {
            _globalMessageService = globalMessageService;
        }

        /// <summary>
        /// Global yorumları listeleyen endpointim
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = _globalMessageService.GetAll();
            return Ok(response);
        }

        /// <summary>
        /// İd 'ye göre global mesaj getiren endpointim
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = _globalMessageService.GetById(id);
            return Ok(response);
        }

        /// <summary>
        /// Belirlenen id sahip mesajı atan kullanıcı getiren endpointim
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("User/{id}")]
        public async Task<IActionResult> GetNameById(int id)
        {
            var response = _globalMessageService.GetNameById(id);
            return Ok(response);
        }

        /// <summary>
        /// global mesaj ekleyen endpointim
        /// </summary>
        /// <param name="globalMessage"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(FarmerGlobalMessage globalMessage)
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
        public async Task<IActionResult> Update(FarmerGlobalMessage globalMessage)
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
        public async Task<IActionResult> Delete(FarmerGlobalMessage globalMessage)
        {
            _globalMessageService.Delete(globalMessage);
            return Ok();
        }
    }
}
