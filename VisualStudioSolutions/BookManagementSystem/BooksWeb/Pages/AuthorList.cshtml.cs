using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConceptArchitect.BookManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BooksWeb.Pages
{
    public class AuthorListModel : PageModel
    {
        IAuthorService service;
        public IList<Author> Authors { get; set; }
        public AuthorListModel(IAuthorService service)
        {
            this.service = service;
        }
        public async Task OnGet()
        {
            Authors = await service.GetAllAuthors();
        }
    }
}
