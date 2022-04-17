using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public interface IAuthorRepository
    {
        Task Add(Author author);
        Task Save();
        Task<IList<Author>> GetAll();
        Task<Author> GetById(string id);
        Task Remove(string id);
        Task<IList<Author>> Search(Func<Author, bool> condition);
    }
}
