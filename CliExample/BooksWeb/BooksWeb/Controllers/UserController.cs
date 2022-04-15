using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConceptSathish.BookManagement;

namespace BooksWeb.Controllers
{
    public class UserController : Controller 
    {
        IAuthenticate iauth;
        public UserController(IAuthenticate authorService)
        {
            this.iauth = authorService;
        }
        //public IActionResult Login()
        //{
        //    return View();
        //}
        public IActionResult Logout()
        {
            return View();
        }
        public async Task<IActionResult> Register(string email,string Username,string pass)
        {
            UserRegister reg = new UserRegister();
            reg.Email = email;
            reg.Name = Username;
            reg.Pass = pass;
            var result = await iauth.AddUser(reg);
            return Content(result.ToString());
            
        }
        public async Task<IActionResult> Login(string email,string pass)
        {
            var user = await iauth.GetUserById(email);
            if(user != null)
            return View(user);
            else
            {
                return View("Error");
            }

        }
        public async Task<IActionResult> GetUsers()
        {
            var author = await iauth.GetAllUsers();
            return View(author);
        }
    }
}
