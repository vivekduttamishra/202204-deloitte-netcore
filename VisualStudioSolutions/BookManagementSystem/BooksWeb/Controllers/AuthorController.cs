using ConceptArchitect.BookManagement;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksWeb.Controllers
{
    public class AuthorController:Controller
    {
        IAuthorService authorService;

        public AuthorController(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        public async Task<IActionResult> Index()
        {
            var authors =await authorService.GetAllAuthors();
            return View(authors);
        }

        public async Task<IActionResult> Details(string id)
        {
            var author = await authorService.GetAuthorById(id);
            return View(author);
        }

        public async Task<IActionResult> Delete(string id)
        {
            await Task.Yield();
            return Content($"Author with id {id} deleted");
        }

        [HttpGet]
        public IActionResult Create()
        {
            var author = new Author();
            return View(author);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Author author)
        {
            if(ModelState.IsValid)
            {
                await authorService.AddAuthor(author);
                return RedirectToAction("Index", new { Id= author.Id });
            }
            else
            {
                //go back to the same page and say there is an error
                return View();
            }
            //return Json(author);
           
        }
    }
}
