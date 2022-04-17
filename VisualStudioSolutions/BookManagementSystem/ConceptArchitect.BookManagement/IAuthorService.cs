using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public interface IAuthorService
    {
        Task AddAuthor(Author author);
        Task<IList<Author>> GetAllAuthors();
        Task<Author> GetAuthorById(string id);

        Task<IList<Author>> Search(string term);

        Task<IList<Book>> GetBooksByAuthor(string id);

        Task RemoveAuthor(string id);

        Task UpdateAuthor(Author author);
    }
}