using ConceptArchitect.BookManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksWeb.Controllers
{
    [ApiController] //to configure standard APi /api/controller
    [Route("/api/authors")]
    public class AuthorApiController:Controller
    {
        IAuthorService service;
        public AuthorApiController(IAuthorService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IList<Author>> GetAllAuthors()
        {
            var authors= await service.GetAllAuthors();
            return authors;
        }

        [HttpGet("{id}")]  //---> /api/authors/vivek-duttamishra
        public async Task<IActionResult> GetAuthorById(string id)
        {
            var author = await service.GetAuthorById(id);
            if (author != null)
                return Ok(author); //status 200
            else
                return NotFound(); //status 404

        }

        [Authorize]
        public async Task<IActionResult> AddAuthor([FormBody] Author author)
        {
            if(ModelState.IsValid)
            {
                await service.AddAuthor(author);
                return Created($"/api/author/{author.Id}", author);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
