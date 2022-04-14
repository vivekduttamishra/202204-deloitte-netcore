using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApp01
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public Table Welcome()
        {
            return new Table
            {
                name = "Debaruna"
            };
        }

        public ViewResult Display()
        {
            return View();
        }
    }
}
