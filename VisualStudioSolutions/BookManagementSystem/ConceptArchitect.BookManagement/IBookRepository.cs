using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public interface IBookRepository
    {
        Task Add(Book book);
        Task Save();
        Task<IList<Book>> GetAll();
        Task<Book> GetById(string id);
        Task Remove(string id);
        Task<IList<Book>> Search(Func<Book, bool> condition);
    }
}
