using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConceptSathish.BookManagement
{
    public interface IAuthenticate
    {
        Task<string> AddUser(UserRegister register);
        Task<IList<UserRegister>> GetAllUsers();
       Task<UserRegister> GetUserById(string email);
    }
}
