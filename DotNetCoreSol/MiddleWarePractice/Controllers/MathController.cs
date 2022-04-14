using Microsoft.AspNetCore.Mvc;

namespace MiddleWarePractice.Controllers
{
    public class MathController : Controller
    {
        public IActionResult Table(int start, int? end)
        {
            end = end ?? 10;
            Multiply multiply = new Multiply { End = end, Start = start };
            return View(multiply);
        }
    }

    public class Multiply
    {
        public int Start { get; set; }
        public int? End { get; set; }
    }
}
