using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp01.Services
{
    public class ConfigurableGreetService : IGreetService
    {
        private ILogger<ConfigurableGreetService> logger;
        string prefix;
        string suffix;
        public ConfigurableGreetService(IConfiguration config,ILogger<ConfigurableGreetService> logger)
        {
            this.logger = logger;
            prefix = config["prefix"];
            suffix = config["suffix"];

            logger.LogInformation($"ConfigurableGreetService #{GetHashCode()} created with prefix ='{prefix}' and suffix='{suffix}'");
        }
        public string Greet(string name)
        {

           var message= $"{prefix} {name}{suffix}";
           logger.LogInformation($"service #{GetHashCode()}:{message}");

            return message;
        }
    }
}
