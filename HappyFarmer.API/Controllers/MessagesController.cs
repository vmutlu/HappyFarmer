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
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;
        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = _messageService.GetAll();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = _messageService.GetById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add(FarmerMessage message)
        {
            _messageService.Create(message);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(FarmerMessage message)
        {
            _messageService.Update(message);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(FarmerMessage message)
        {
            _messageService.Delete(message);
            return Ok();
        }
    }
}
