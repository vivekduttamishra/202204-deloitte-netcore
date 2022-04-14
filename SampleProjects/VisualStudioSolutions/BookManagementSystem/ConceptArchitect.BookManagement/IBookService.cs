using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public interface IBookService
    {
        Task<IList<Book>> GetAllBooks();
        Task<Book> GetBookByTitle(string title);
    }
}
