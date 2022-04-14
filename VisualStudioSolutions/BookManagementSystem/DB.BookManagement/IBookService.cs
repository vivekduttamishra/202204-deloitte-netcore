using System.Collections.Generic;
using System.Threading.Tasks;

namespace DB.BookManagement
{
    public interface IBookService
    {
        public Task<IList<Books>> GetAllBooks();
        public Task<Books> GetBookById(string id);
    }
}