using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp01.Authentication
{
    public static class AuthenticationServiceInjector
    {
        public static void AddAuthenticationService(this IServiceCollection services)
        {
            services.AddSingleton<IUserService, InMemoryUserService>();
        }
    }
}
