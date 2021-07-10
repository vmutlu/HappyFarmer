using HappyFarmer.Business.Abstract;
using HappyFarmer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HappyFarmer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;
        public MessagesController(IMessageService messageService) =>
            _messageService = messageService;

        [HttpGet]
        public IActionResult GetAll() => Ok(_messageService.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok(_messageService.GetById(id));

        [HttpPost]
        public IActionResult Add(FarmerMessage message)
        {
            _messageService.Create(message);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(FarmerMessage message)
        {
            _messageService.Update(message);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(FarmerMessage message)
        {
            _messageService.Delete(message);
            return Ok();
        }
    }
}
