using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp01.Services
{
    public class UserService : IUserAthenticationService
    {
        private readonly List<User> users;
        public UserService()
        {
            this.users = new List<User>();
        }
        public async Task<string> Register(User user)
        {
            if (users.Count == 0 || (users.Count > 0 && users.FirstOrDefault(x => x.Email.Equals(user.Email)).Token == null))
            {
                user.Token = GetHashCode().ToString();
                users.Add(user);
                return user.Token;
            }
            else
            {
                return $"{user.Email} already registered";
            }
        }

        public async Task<string> Login(User user)
        {
            if (users.Count > 0 )
            {
                User user1 = users.FirstOrDefault(x => x.Email.Equals(user.Email) && x.Password.Equals(user.Password));
                if (user1 == null)
                {
                    return "Please enter valid email and password";
                }
                else
                {
                    if (user1.Token == null)
                    {
                        user1.Token= GetHashCode().ToString();
                        users.FirstOrDefault(x => x.Email.Equals(user.Email)).Token = user1.Token;
                    }
                    return user1.Token;
                }
                
            }
            else
            {
                return $"Please register first and then try.";
            }
        }

        public async Task<string> Logout(User user)
        {
            if (users.Count > 0 && users.FirstOrDefault(x => x.Email.Equals(user.Email)).Token != null)
            {
                users.FirstOrDefault(x => x.Email.Equals(user.Email)).Token = null;
                return $"{user.Email} has been logged out successfully.";
            }
            return $"{user.Email} not logged in.";
        }

        public async Task<User> GetUser(string email)
        {
            return users.FirstOrDefault(x => x.Email.Equals(email));
        }

        public async Task<bool> IsValidToken(User user)
        {
            if(users.Count > 0)
            {
                User user1 = users.FirstOrDefault(x => x.Email.Equals(user.Email));
                if (user1 != null && user1.Token.Equals(user.Token))
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
