using System.Threading.Tasks;

namespace WebApp01.Authentication
{
    public interface IUserService
    {
        Task AddUser(User user);
        Task DeleteToken(string token);
        Task<string> GetToken(string email, string password);
        Task<User> ValidateToken(string token);
    }
}