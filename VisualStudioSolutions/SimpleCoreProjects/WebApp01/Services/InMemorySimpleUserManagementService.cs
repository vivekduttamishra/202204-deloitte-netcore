using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp01.Services
{
    public class InMemorySimpleUserManagementService : ISimpleUserManagementService
    {
        public List<Users> users = new List<Users>();

        public string getToken(string userName)
        {
            var user = users.FirstOrDefault(x => x.UserName == userName);

            if(user!= null && user.Token != null)
            {
                return user.Token;
            }
            return  Guid.NewGuid().ToString();
        }

        public string Login(string userName, string password)
        {
            var user = users.FirstOrDefault(x => x.UserName == userName);
            if (user != null)
            {
                if (user.Password == password)
                {
                    if(user.Token == null)
                    {
                        user.Token = getToken(user.UserName);
                    }
                    return user.Token;
                }
            }
            return null;
        }

        public void Logout(string userName)
        {
            var user = users.FirstOrDefault(x => x.UserName == userName);
            if(user!= null)
            {
                user.Token = null;
            }
            
        }

        public string Register(string userName, string email, string password)
        {
            Users user = new Users();
            var userExist = users.FirstOrDefault(x => x.UserName == userName);

            if (userExist != null)
            {
                return null;
            }
            user.Email = email;
            user.UserName = userName;
            user.Password = password;
            user.Token = getToken(userName);

            users.Add(user);
            return user.Token;
        }

        public bool TokenAuthentication(string token)
        {
            var tokens = users.FirstOrDefault(x => x.Token == token);
             if(tokens == null)
            {
                return false;
            }
            return true;
        }
    }
}
