using System.Threading.Tasks;

namespace DB.BookManagement
{
    public interface IUserService
    {
        public bool LoginUser(Users user);
        public bool RegisterUser(Users user);
    }
}