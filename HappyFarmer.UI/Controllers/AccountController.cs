using HappyFarmer.Business.Abstract;
using HappyFarmer.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HappyFarmer.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;       
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        #region ReCapctcha

        private async Task<bool> CheckCaptcha()
        {
            var postData = new List<KeyValuePair<string, string>>()
                 {
                  new KeyValuePair<string, string>("secret", "6LdauNsZAAAAANMIdH2x27vHca7X87PjhlHgGGUk"),
                  new KeyValuePair<string, string>("remoteip", HttpContext.Connection.RemoteIpAddress.ToString()),
                  new KeyValuePair<string, string>("response", HttpContext.Request.Form["g-recaptcha-response"])
                 };

            var client = new HttpClient();
            var response = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", new FormUrlEncodedContent(postData));

            var o = (JObject)JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

            return (bool)o["success"];
        }

        #endregion

        [HttpGet]
        public IActionResult Index(string message)
        {            
            ViewBag.Message = message;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginModel loginModel)
        {
            var captchaImage = HttpContext.Request.Form["g-recaptcha-response"];
            if (string.IsNullOrEmpty(captchaImage))
            {
                ViewBag.Captcha = "Captcha doğrulanmamış";
                return View();
            }

            var verified = await CheckCaptcha();
            if (!verified)
            {
                ViewBag.Captcha = "Captcha yanlış doğrulanmış";
                return View();
            }

            if (ModelState.IsValid)
            {

                var user = _userService.FindByEmail(loginModel.Email);
                var activeUserId = 0;
                var activeUserType = 0;
                foreach (var item in user)
                {
                    activeUserId = item.Id;
                    activeUserType = item.UserType;
                }

                if (user == null)
                {
                    ModelState.AddModelError("", "Bu Email adresi ile eşleşen bir Email Adresi Bulunamadı");
                }

                var userState = 1; //admin
                var userLogin = _userService.PasswordSignIn(loginModel.Email, loginModel.Password, userState);

                if (userLogin)
                {
                    HttpContext.Session.SetString("ActiveUser", loginModel.Email);
                    HttpContext.Session.SetString("ActiveUserId", activeUserId.ToString());
                    HttpContext.Session.SetString("ActiveUserType", activeUserType.ToString());
                    return RedirectToAction("Index", "Admin");
                }

                else
                {
                    ModelState.AddModelError("", "Email veya Şifre Hatalı !");
                    ViewBag.Message = "Email veya Şifre Hatalı !";
                    return View("Index");
                }
            }

            return View();
        }

        //public async Task<IActionResult> Login(LoginModel loginModel)
        //{
        //    var captchaImage = HttpContext.Request.Form["g-recaptcha-response"];
        //    if (string.IsNullOrEmpty(captchaImage))
        //    {
        //        ViewBag.Captcha = "Captcha doğrulanmamış";
        //        return View();
        //    }

        //    var verified = await CheckCaptcha();
        //    if (!verified)
        //    {
        //        ViewBag.Captcha = "Captcha yanlış doğrulanmış";
        //        return View();
        //    }

        //    if (ModelState.IsValid)
        //    {

        //        var user = _userService.FindByEmail(loginModel.Email);
        //        var activeUserId = 0;
        //        var activeUserType = 0;
        //        foreach (var item in user)
        //        {
        //            activeUserId = item.Id;
        //            activeUserType = item.UserType;
        //        }

        //        if(user == null)
        //        {
        //            ModelState.AddModelError("", "Bu Email adresi ile eşleşen bir Email Adresi Bulunamadı");
        //        }

        //        var userState = 1; //admin
        //        var userLogin = _userService.PasswordSignIn(loginModel.Email, loginModel.Password,userState);

        //        if (userLogin)
        //        {
        //            HttpContext.Session.SetString("ActiveUser", loginModel.Email);
        //            HttpContext.Session.SetString("ActiveUserId", activeUserId.ToString());
        //            HttpContext.Session.SetString("ActiveUserType", activeUserType.ToString());
        //            return RedirectToAction("Index", "Admin");
        //        }

        //        else
        //        {
        //            ModelState.AddModelError("", "Email veya Şifre Hatalı !");
        //            ViewBag.Message = "Email veya Şifre Hatalı !";
        //            return View("Index");
        //        }
        //    }

        //    return View();
        //}

        public IActionResult Logout()
        {
           // HttpContext.Session.Clear();
            HttpContext.Session.Remove("ActiveUser");
            HttpContext.Session.Remove("ActiveUserId");

            //foreach (var cookie in Request.Cookies.Keys)
            //{
            //    Response.Cookies.Delete(cookie);
            //}

            return RedirectToAction("Index");
        }
    }
}
