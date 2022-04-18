using System;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public interface IUserRepository
    {
        Task<User> Add(User user);
        Task<User> Find(Func<User, bool> p);
    }
}