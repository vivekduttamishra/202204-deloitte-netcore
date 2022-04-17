using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp01.Authentication
{
    public class InMemoryUserService : IUserService
    {
        Dictionary<string, User> users;
        Dictionary<string, User> activeTokens = new Dictionary<string, User>();
        public InMemoryUserService()
        {
            users = new Dictionary<string, User>();
            AddUser(new User() { Name = "Vivek Dutta Mishra", Email = "vivek@conceptarchitect.in", Password = "p@ss1" });
            AddUser(new User() { Name = "Sanjay", Email = "sanjay@conceptarchitect.in", Password = "p@ss1" });
        }

        public async Task AddUser(User user)
        {
            await Task.Yield();

            if (string.IsNullOrEmpty(user.Name))
                throw new ArgumentException($"Name is missing");

            if (string.IsNullOrEmpty(user.Email))
                throw new ArgumentException($"Email is missing");

            if (string.IsNullOrEmpty(user.Password))
                throw new ArgumentException($"Password is missing");

            var email = user.Email.ToLower();
            if (users.ContainsKey(email))
                throw new DuplicateUserException($"the email id {email} is already registered");

            users[email] = user;
        }

        public async Task<string> GetToken(string email, string password)
        {
            await Task.Yield();

            if (string.IsNullOrEmpty(email))
                throw new ArgumentException($"email is missing");
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException($"password is missing");

            email = email.ToLower();
            if (users.ContainsKey(email))
            {
                var user = users[email];
                if (user.Password == password)
                {

                    
                    foreach(var v in activeTokens )
                    {
                        if (v.Value.Equals(user))
                            return v.Key;
                    }
                    
                    var token=Guid.NewGuid().ToString(); //we just need a unique token
                    //store it is service
                    activeTokens[token] = user;
                    //return it
                    return token;
                }
            }

            throw new InvalidCredentialsException("invalid credentials");
        }

        public async Task<User> ValidateToken(string token)
        {
            await Task.Yield();
            if (activeTokens.ContainsKey(token))
            {
                return activeTokens[token];
            }
            //else
            throw new InvalidCredentialsException($"Invalid/Expired Token");
        }

        public async Task DeleteToken(string token)
        {
            await Task.Yield();
            if (activeTokens.ContainsKey(token))
                activeTokens.Remove(token);
        }

    }
}
