using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using SimpleMVCApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SimpleMVCApp.Controllers
{
    public class MathController : Controller
    {

        public ContentResult Table1(int id)
        {
            string html = $"<html><head><title>Table of {id}</title></head>" +
                            $"<body><h1>Table of {id}</h1>";


            for (int i = 1; i <= 10; i++)
                html += $"<p>{id} x {i} = {id * i}";

            html += "</body</html>";

            return Content(html, "text/html");

        }

        public ViewResult Table2(int id)
        {
            string html =$"<h1>Table of {id}</h1>";

            for (int i = 1; i <= 10; i++)
                html += $"<p>{id} x {i} = {id * i}";

            var str = new HtmlString(html);

            return View(str);

        }


        public ViewResult Table3(int id)
        {
            return View(id);
        }

        public ViewResult Table(int? number1,int? number2)
        {
            if(!number1.HasValue)
            {
                Response.StatusCode = 400;
                return View("ErrorView", (object)"No Number Supplied");
            }

            var table = new MultiplicationTable()
            {
                Number = number1.Value
            };

            if (number2.HasValue)
                table.HightestMultiplier = number2.Value;

            table.Generate(); //knows which method of model call. Doesn't know internal logic

            return View(table);


        }

    }
}
