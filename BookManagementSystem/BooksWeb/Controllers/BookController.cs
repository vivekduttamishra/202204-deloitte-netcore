using ConceptArchitect.BookManagement;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BooksWeb.Controllers
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
            var book = await bookService.GetBookByTitle(id);
            return View(book);
        }
    }
}
