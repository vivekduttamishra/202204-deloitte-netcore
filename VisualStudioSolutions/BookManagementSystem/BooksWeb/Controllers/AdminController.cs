using ConceptArchitect.BookManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksWeb.Controllers
{
    public class AdminController:Controller
    {
        DataSeeder seeder;
        public AdminController(DataSeeder seeder)
        {
            this.seeder = seeder;
        }

        [Authorize]
        public async Task<IActionResult> Seed()
        {
            await seeder.Seed();
            return RedirectToAction("Index", "Author");
        }
    }
}
