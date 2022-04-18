using ConceptArchitect.BookManagement;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksWeb.Controllers
{
    public class BookController:Controller
    {
        //TODO: controller shouldn't know repository. Add the service layer
        IBookRepository repository;
        public BookController(IBookRepository repository) 
        {
            this.repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var books = await repository.GetAll();
            return Ok(books); //returning data as JSON
        }
    }
}
