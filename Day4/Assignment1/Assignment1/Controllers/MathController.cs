using Assignment1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Assignment1.Controllers
{
    public class MathController : Controller
    {
        public IActionResult Table(int Id)
        {
            var multiplicationTable = new List<MultiplicationTableRow>();
            
            for(int i = 1; i <= 10; i++)
            {
                multiplicationTable.Add(new MultiplicationTableRow()
                {
                    Num = i,
                    Multiplicator = Id,
                    Result = i * Id
                });
            }
            return View(multiplicationTable);
        }
    }
}
