using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Models;
using Services.Interfaces;
using System;
using System.Diagnostics;
using System.Web;
using Microsoft.AspNetCore.Session;
using Project.Helpers;
using Project.Filters;

namespace Project.Controllers
{
    [ExceptionFilter]
    public class UserController : Controller
    {
        IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {

            if (ModelState.IsValid && IsUserExcist(model.Username, model.Password))
                HttpContext.SetUserName(model.Username);
            return RedirectToAction("Index", "Home");

        }

        public IActionResult Logout()
        {
            HttpContext.DeleteUsername();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Registration()
        {

            var name = HttpContext.Request.Cookies["username"];
            var model = string.IsNullOrEmpty(name) ? new UserModel() : _service.GetUserByUserName(name);
            return View(model);

        }

        [HttpPost]
        public IActionResult RegistrationSumbit(UserModel model)
        {

            if (ModelState.IsValid)
            {
                if (CheckIfLogin())
                    _service.UpdateUser(model);
                else
                {
                    HttpContext.SetUserName(model.UserName);
                    _service.AddUser(model);
                }
                return RedirectToAction("Index", "Home");
            }
            return View("/Views/User/Registration.cshtml", model);

        }

        public bool IsUserExcist(string username, string Password)
        {
            return _service.IsUserExcist(username, Password);

        }



        public bool CheckUserNameAvalaible(string username)
        {

            if (CheckIfLogin()) return true;
            return _service.IsValidUser(username);

        }

        private bool CheckIfLogin()
        {
            return HttpContext.CheckIfUserLogin();
        }
    }
}
