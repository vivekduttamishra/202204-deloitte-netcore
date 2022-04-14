using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public interface IBookService
    {
        Task AddBook(Book book);
        Task<IList<Book>> GetAllBooks();
        Task<Book> GetBookById(string id);
        Task<IList<Book>> GetBookByAuthor(string id);
    }
}