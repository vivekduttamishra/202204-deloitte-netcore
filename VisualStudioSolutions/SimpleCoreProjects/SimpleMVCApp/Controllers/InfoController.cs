using Microsoft.AspNetCore.Mvc;
using SimpleMVCApp.Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMVCApp
{
    public class InfoController : Controller
    {
        public string Greet()
        {
            return "Hello MVC App";
        }

        public DateTime Time()
        {
            return DateTime.Now;
        }

        public Person Contact()
        {
            return new Person()
            {
                Name = "Vivek Dutta Mishra",
                Email = "vivek@booksweb.com"
            };
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

        public ViewResult Now()
        {
            ViewData["PageTitle"] = "Time Server";
            ViewBag.Date = DateTime.Now;
            ViewBag.Email = "vivek@infoserver.com";
            return View();
        }


        public ViewResult Today()
        {
            var model = new DateInfo()
            {
                Title = "Today",
                Date = DateTime.Now
            };

            return View("DateTime",model);  //pass model to view.
        }

        public ViewResult Tomorrow()
        {
            var model = new DateInfo()
            {
                Title = "Tomorrow",
                Date = DateTime.Now.AddDays(1)
            };

            return View("DateTime", model);
        }

        public ViewResult After(int days)
        {
            var model = new DateInfo()
            {
                Title = $"After {days} days",
                Date = DateTime.Now.AddDays(days)
            };

            return View("DateTime", model);
        }

        public ActionResult AfterDays(int? id)
        {
            try
            {
                var days = id.Value;
                var model = new DateInfo()
                {
                    Title = $"After {id} days",
                    Date = DateTime.Now.AddDays(days)
                };

                return View("DateTime", model);
            }
            catch(Exception e)
            {
                //choice #1 ---> call today action
                //return Today();

                //choice #2 ---> redirect to today
                //return RedirectToAction("Today");

                //choice #3 ---> show error page
                Response.StatusCode = 400; //Bad Request
                return View("ErrorView", (object)"Missing days");

            }


        }
               
        public ActionResult Welcome(string guests)
        {
            var guestList = guests.Split(",");

            return View(guestList);
        }



    }
}
