using CliWebApp.Model;
using Microsoft.AspNetCore.Mvc;

namespace CliWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(uint num1, uint num2)
        {
            var tabData = new TableData()
            {
                Multiple = num1,
                Multiplier = num2
            };

            return View(tabData);
        }
    }
}
