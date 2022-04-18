using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public interface IUserManager
    {
        Task<User> Login(string email, string password);
        Task<User> Register(User user);
    }
}