using System.Threading.Tasks;

namespace WebApp01.Services
{
    public interface IUserAthenticationService
    {
        Task<string> Register(User user);

        Task<string> Login(User user);

        Task<string> Logout(User user);

        Task<bool> IsValidToken(User user);
    }
}
