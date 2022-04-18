using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public class PersistentUserManager : IUserManager
    {
        IUserRepository users;
        public PersistentUserManager(IUserRepository users)
        {
            this.users = users;
        }
        public async Task<User> Register(User user)
        {
            User result = await users.Add(user);
            return result;
        }

        public async Task<User> Login(string email, string password)
        {
            User user = await users.Find(user => user.Email.ToLower() == email.ToLower() && user.Password == password);
            return user;
        }
    }
}
