using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.BookManagement
{
    public class UserService : IUserService
    {

        Dictionary<string, Users> users;
        public UserService()
        {
            Users[] _users =
            {
                new Users()
                {
                    Username="sdebaruna",
                    Email="sdebaruna@deloitte.com",
                    Password="pass1"
                },

                new Users()
                {
                    Username="debsaha",
                    Email="debsaha@deloitte.com",
                    Password="pass2"
                },
                new Users()
                {
                    Username="ddss",
                    Email="ddss@gmail.com",
                    Password="pass3"
                },
            };

            users = new Dictionary<string, Users>();
            AddAll(_users);

        }

        private void AddAll(Users[] _users)
        {
            foreach (var user in _users)
                AddUsers(user);
        }

        public void AddUsers(Users user)
        {
            if (string.IsNullOrEmpty(user.Id))
            {
                var parts = user.Email.ToLower().Split('@');
                user.Id = parts[0];
            }

            users[user.Id] = user;
        }

        public bool LoginUser(Users user)
        {
            if (users.Any(x => x.Value.Email.Equals(user.Email) && x.Value.Password.Equals(user.Password)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RegisterUser(Users user)
        {
            if (users.Any(x => x.Value.Email.Equals(user.Email)))
                return false;
            else
            {
                AddUsers(user);
                return true;
            }
        }
    }
}
