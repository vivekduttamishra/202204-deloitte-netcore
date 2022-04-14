using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp01.Service
{
    public class AuthenticationService : IDisplayTokenService
    {
        public static int token = 100;
        public static List<UserInfo> userInfos = new List<UserInfo>();
        static UserInfo currentUser;

        public class AuthenticationConfiguration
        {
            public string RegisteredSuffix { get; set; }
            public string LogInSuffix { get; set; }
            public string UnSuccessfulLogInSuffix { get; set; }
            public string UnAuthorizedAccessSuffix { get; set; }
            public string LoggedOutSuffix { get; set; }
        }

        AuthenticationConfiguration info;

        public AuthenticationService(IConfiguration config)
        {
            info = new AuthenticationConfiguration();
            config.Bind("register", info);
        }

        public string RegisterUser(string name, string email, string password)
        {
            var userInfo = new UserInfo
            {
                Name = name,
                Email = email,
                Password = password,
                Token = token
            };
            userInfos.Add(userInfo);
            var message = $"{info.RegisteredSuffix} \n 200OK";
            return message;
        }

        public string LoginUser(string email, string password)
        {
            var message = "";
            currentUser = new UserInfo();
            if (userInfos.Any(x => x.Email.Equals(email) && x.Password.Equals(password)))
            {
                currentUser = userInfos.Where(x => x.Email.Equals(email) && x.Password.Equals(password)).FirstOrDefault();
                message = $"{info.LogInSuffix} \n {currentUser.Token}";
            }
            else
            {
                message = $"{info.UnSuccessfulLogInSuffix}";
            }
            return message;
        }

        public string AddTokenToUrl(string url)
        {
            var appendedUrl = "";
            if (currentUser == null)
            {
                appendedUrl = $"{url}&token=";
            }
            else
            {
                appendedUrl = $"{url}&token={currentUser.Token}";
            }
            return appendedUrl;
        }

        public string CheckIfAuthorized(string url)
        {
            var message = "";
            string[] stringSeparators = new string[] { "&token=" };
            string[] token = url.Split(stringSeparators, StringSplitOptions.None);
            if (String.IsNullOrEmpty(token[1]))
            {
                message = $"{info.UnAuthorizedAccessSuffix}";
            }
            else
            {
                message = $"Protected Page\n{token[1]}";
            }
            return message;
        }

        public string LogOut()
        {
            currentUser = null;
            return $"{info.LoggedOutSuffix}";
        }
    }
}

