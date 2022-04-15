using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConceptSathish.BookManagement
{
    public interface IAuthorService
    {
        Task AddAuthor(Author author);
        Task<IList<Author>> GetAllAuthors();
        Task<Author> GetAuthorById(string id);
    }
}
