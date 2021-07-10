using HappyFarmer.Business.Abstract;
using HappyFarmer.Entities;
using HappyFarmer.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.RegularExpressions;

namespace HappyFarmer.UI.Controllers
{
    public class CarrierController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;
        public CarrierController(IUserService userService, IMessageService messageService)
        {
            _userService = userService;
            _messageService = messageService;
        }

        public IActionResult Index() => View(_userService.GetAllCarrier());

        [HttpGet]
        public IActionResult GetCarierByDetails(int id)
        {
            var response = _userService.GetById(id);
            var entity = new UserModel()
            {
                Id = response.Id,
                Address = response.Address,
                Authority = response.Authority,
                City = response.City,
                Email = response.Email,
                ImageURL = response.ImageURL,
                Name = response.Name,
                Password = response.Password,
                PhoneNumber = response.PhoneNumber,
                RecordData = response.RecordData,
                Surname = response.Surname,
                TcNo = response.TcNo,
                UserName = response.UserName,
                UserState = response.UserState,
                UserType = response.UserType
            };

            ViewBag.Message = TempData["Messages"];
            ViewBag.ActiveCustomerId = HttpContext.Session.GetString("ActiveCustomerId");
            return View(entity);
        }

        [HttpPost]
        public IActionResult CarierMessageRecevied(UsersMessageModel userMessageModelstring, int carierId)
        {
            var clearMessageContent = RemoveHtml(userMessageModelstring.MessageContent);
            var userSenderId = HttpContext.Session.GetString("ActiveCustomerId");
            var messages = new FarmerMessage()
            {
                MessageContent = clearMessageContent,
                MessageDate = DateTime.Now.ToShortDateString(),
                Subject = userMessageModelstring.Subject,
                UserReceiverId = userMessageModelstring.UserReceiverId,
                UserSenderId = Convert.ToInt32(userSenderId),
                MessageReceiverDelete = true,
                MessageSendDelete = true
            };

            _messageService.Create(messages);
            TempData["Messages"] = "Mesajınız Başarıyla Nakliyeciye Gönderildi...";
            return Redirect("/Carrier/GetCarierByDetails?id=" + carierId);
        }

        public static string RemoveHtml(string text) => Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
    }
}
