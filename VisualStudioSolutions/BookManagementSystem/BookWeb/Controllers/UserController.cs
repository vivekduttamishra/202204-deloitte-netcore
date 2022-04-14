using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DB.BookManagement;
using Microsoft.AspNetCore.Mvc;

namespace BookWeb.Controllers
{
    public class UserController : Controller
    {
        IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Create(Users user)
        {
            var result = userService.RegisterUser(user);
            if (result)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Register");
            }
        }
        public IActionResult Submit(Users user)
        {
            var result = userService.LoginUser(user);
            if (result)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Login");
            }
        }
    }
}
