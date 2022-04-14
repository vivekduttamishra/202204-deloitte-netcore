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
        IBookService bookService;

        public AuthorController(IAuthorService authorService, IBookService bookService)
        {
            this.authorService = authorService;
            this.bookService = bookService;
        }

        public async Task<IActionResult> Index()
        {
            var authors =await authorService.GetAllAuthors();
            return View(authors);
        }

        public async Task<IActionResult> Details(string id)
        {
            var author = await authorService.GetAuthorById(id);
            var books = await bookService.GetBookByAuthor(id);
            author.Books = books;
            return View(author);
        }
    }
}
