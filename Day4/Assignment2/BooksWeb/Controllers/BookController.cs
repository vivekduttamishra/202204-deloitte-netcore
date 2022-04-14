using ConceptArchitect.BookManagement;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksWeb.Controllers
{
    public class BookController: Controller
    {
        IBookService _bookService;
        public BookController(IBookService bookSevices)
        {
            _bookService = bookSevices;
        }
        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooks();
            return View(books);
        }
    }
}
