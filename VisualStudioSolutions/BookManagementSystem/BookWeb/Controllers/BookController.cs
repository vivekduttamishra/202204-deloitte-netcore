using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DB.BookManagement;
using Microsoft.AspNetCore.Mvc;

namespace BookWeb.Controllers
{
    public class BookController : Controller
    {
        IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public async Task<IActionResult> Index()
        {
            var books = await bookService.GetAllBooks();
            return View(books);
        }

        public async Task<IActionResult> Details(string id)
        {
            var book = await bookService.GetBookById(id);
            return View(book);
        }
    }
}
