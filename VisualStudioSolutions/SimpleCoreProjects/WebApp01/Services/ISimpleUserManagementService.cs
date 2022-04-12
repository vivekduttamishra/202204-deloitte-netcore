

namespace WebApp01.Services
{
    public interface ISimpleUserManagementService
    {
        string Register(string userName, string email, string password);
        string Login(string userName, string password);
        string getToken(string userName);
        void Logout(string userName);
        bool TokenAuthentication(string token);
    }
}
