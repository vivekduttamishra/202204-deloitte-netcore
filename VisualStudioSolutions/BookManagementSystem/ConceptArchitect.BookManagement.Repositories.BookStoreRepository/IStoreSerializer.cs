using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement.Repositories.BookStoreRepository
{
    public interface IStoreSerializer
    {
        Task<BookStore> Get();
        Task Save();
    }
}
