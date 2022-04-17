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

        public async Task<IActionResult> Delete(string id)
        {
            await Task.Yield();
            await bookService.RemoveBook(id);
            return Content($"Book with id {id} deleted");
        }

        public IActionResult Create()
        {
            var book = new Book();
            return View(book);
        }

        public async Task<IActionResult> AddBook(Book book)
        {
            if (ModelState.IsValid)
            {
                await bookService.AddBook(book);
                return RedirectToAction("Details", new { Id = book.Isbn });
            }
            else
            {
                return View("Create");
            }

        }
    }
}
