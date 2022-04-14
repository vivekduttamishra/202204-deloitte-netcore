using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HelloMVC.Models;

namespace HelloMVC.Controllers
{
    public class MathController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ViewResult Table(int id, int? times)
        {
            List<string> tablevalue = new List<string>();
            int time = times.HasValue ? times.Value : 10;
            for (int i = 1; i <= time; i++)
            {
                tablevalue.Add($"{id.ToString()} * {i}={id * i}");
            }
            return View(tablevalue);
        }
    }
}
