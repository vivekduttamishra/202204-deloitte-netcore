namespace MiddleWarePractice.Interfaces
{
    public interface IUserManagementService
    {
        bool Register(string username, string password);
        bool login(string username, string password);
        string GetToken(string username);
        bool DeleteToken(string token);
        bool ValidateToken(string token);
    }
}
