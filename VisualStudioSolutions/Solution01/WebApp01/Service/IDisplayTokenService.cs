using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp01.Service
{
    public interface IDisplayTokenService
    {
        public string RegisterUser(string name, string email, string password);

        public string LoginUser(string email, string password);

        public string AddTokenToUrl(string url);

        public string CheckIfAuthorized(string url);

        public string LogOut();
    }
}
