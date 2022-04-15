using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConceptSathish.BookManagement;
using Microsoft.AspNetCore.Mvc;

namespace BooksWeb.Controllers
{
    public class AuthorController : Controller
    {
        IAuthorService iauth;
        public AuthorController(IAuthorService authorService)
        {
            this.iauth = authorService;
        }
        public async Task<IActionResult> Index()
        {
            await Task.Yield(); 
            var authors = await iauth.GetAllAuthors();
            return View(authors);
        }
        public async Task<IActionResult> Details(string id)
        {
            var author = await iauth.GetAuthorById(id);
            return View(author);
        }
      
    }

}
