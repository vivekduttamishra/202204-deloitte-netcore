using System.Collections.Generic;
using System.Threading.Tasks;

namespace DB.BookManagement
{
    public interface IAuthorService
    {
        public Task<IList<Authors>> GetAllAuthors();

        public Task<Authors> GetAuthorById(string id);
    }
}