using BooksWeb.ViewModels;
using ConceptArchitect.BookManagement;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BooksWeb.Controllers
{
    public class UserController:Controller
    {
        IUserManager manager;
        public UserController(IUserManager manager)
        {
            this.manager = manager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginUser user = new LoginUser();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUser user)
        {
            if (ModelState.IsValid)
            {
                var loggedInUser = await manager.Login(user.Email, user.Password);
                if (loggedInUser == null)
                    ModelState.AddModelError("*", "Invalid Credential");
                else
                {
                    //do the login here. Now we should login
                    Claim[] personalClaims = {
                        new Claim("Name", loggedInUser.Name),
                        new Claim("Email", loggedInUser.Email)
                    };

                    ClaimsIdentity booksWebAuthority = new ClaimsIdentity(personalClaims, "BooksWebIdentity");
                    ClaimsPrincipal principal = new ClaimsPrincipal(new[] { booksWebAuthority });

                    await HttpContext.SignInAsync(principal);
                }
            }
            if(!ModelState.IsValid)
            {
                return View();
            } else
            {
                return RedirectToAction("Index", "Home");
            }
        }






        public IActionResult Register()
        {
            return View();
        }

    }
}
