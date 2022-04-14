using SimpleMVCApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspnetFull.Controllers
{
    public class InfoController : Controller
    {

        public Person Contact()
        {
            return new Person()
            {
                Name = "Vivek Dutta Mishra",
                Email = "vivek@booksweb.com"
            };
        }


        public string Greet()
        {
            return "Hello MVC App";
        }

        public DateTime Time()
        {
            return DateTime.Now;
        }


        public string AdminContact()
        {
            var admin = new Person()
            {
                Name = "Admin",
                Email = "admin@booksweb.com"
            };

            var html = $"<html><head><title>Admin Contact</title></head>" +
                      $"<body><h1>Admin Contact</h1>" +
                      $"<p>Name=<strong>{admin.Name}</strong></p>" +
                      $"<p>Email=<strong>{admin.Email}</strong></p>" +
                      $"</body></html>";

            return html;
        }


        public ContentResult Admin2()
        {
            var admin = new Person()
            {
                Name = "Admin",
                Email = "admin@booksweb.com"
            };

            var html = $"<html><head><title>Admin Contact</title></head>" +
                      $"<body><h1>Admin Contact</h1>" +
                      $"<p>Name=<strong>{admin.Name}</strong></p>" +
                      $"<p>Email=<strong>{admin.Email}</strong></p>" +
                      $"</body></html>";

            //return new ContentResult()
            //{
            //    Content = html,
            //    ContentType = "text/html"
            //};

            return Content(html, "text/html");
        }

        public ViewResult Home()
        {
            return View();
        }


    }
}