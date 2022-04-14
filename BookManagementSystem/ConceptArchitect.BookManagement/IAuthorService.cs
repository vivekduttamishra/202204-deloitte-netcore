using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public interface IAuthorService
    {
        Task AddAuthor(Author author);
        Task<IList<Author>> GetAllAuthors();
        Task<Author> GetAuthorById(string id);
    }
}