using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
namespace ConceptSathish.BookManagement
{
    public class Authenticate : IAuthenticate
    {
        /// Dictionary<string, UserRegister> users;
        public IList<UserRegister> RegisterdUsers { get; set; } = new List<UserRegister>();
        public async Task<string> AddUser(UserRegister user)
        {
            //users = new Dictionary<string, UserRegister>();
            await Task.Yield();
            bool userExists = RegisterdUsers.Any(item => item.Email == user.Email);
            if (string.IsNullOrEmpty(user.Email))
            {
                return "Email address is empty";
            }
            else if (userExists)
            {
                return "User Added Exist";
            }
            else
            {
                Guid g = Guid.NewGuid();
                user.ID = g.ToString();
                RegisterdUsers.Add(user);
                return "User Added Successfully";
            }


        }

        public async Task<IList<UserRegister>> GetAllUsers()
        {
            await Task.Yield();
            return RegisterdUsers;
        }
        public async Task<UserRegister> GetUserById(string emailid)
        {
            await Task.Yield();
            var user = RegisterdUsers.FirstOrDefault(item => item.Email == emailid);
            return user;
        }

    }
}
