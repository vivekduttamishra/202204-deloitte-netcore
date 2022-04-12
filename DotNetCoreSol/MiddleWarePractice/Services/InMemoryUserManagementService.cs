using MiddleWarePractice.Constants;
using MiddleWarePractice.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiddleWarePractice.Services
{
    public class InMemoryUserManagementService : IUserManagementService
    {
        private Dictionary<string, string> _users = new Dictionary<string,string>();
        private Dictionary<string, string> _tokens = new Dictionary<string, string>();

        public InMemoryUserManagementService()
        {
            _users.Add("rakesh","rakesh");
            _tokens.Add("rakesh","token");
        }

        public bool DeleteToken(string username)
        {
            if (_tokens.ContainsKey(username))
            {
                _tokens.Remove(username);
            }
            return true;
        }

        public string GetToken(string username)
        {
            string token = null;
            if (!_tokens.ContainsKey(username))
            {
                Random random = new Random();
                token = new string(Enumerable.Repeat(Contants.RandomCharacters, 10)
                   .Select(s => s[random.Next(s.Length)]).ToArray());
                _tokens.Add(username, token);
            }
            else
            {
                token = _tokens[username];
            }
            return token;   
        }

        public bool login(string username, string password)
        {
            if(_users.ContainsKey(username) && _users[username] == password){
                return true;   
            }
            else
            {
                return false;
            }
        }

        public bool Register(string username, string password)
        {
            return _users.TryAdd(username, password);
        }

        public bool ValidateToken(string token)
        {
           return _tokens.Select(x => x.Value).Any(tok => tok == token);
        }
    }
}
