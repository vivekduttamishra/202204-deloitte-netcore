using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp01.Services
{
    public class ConfigurableGreetServiceV2 : IGreetService
    {
        private ILogger<ConfigurableGreetService> logger;
        string prefix;
        string suffix;
        bool timedGreet;
        bool allCapsName;
        TimeUtil time;

        public string Prefix
        {
            get
            {
                if (timedGreet)
                    return time.TimedGreet;
                else
                    return prefix;
            }
        }
        public ConfigurableGreetServiceV2(IConfiguration config,ILogger<ConfigurableGreetService> logger,TimeUtil util)
        {
            this.logger = logger;
            prefix = config["greeter:prefix"];
            suffix = config["greeter:suffix"];
            var tg = config["greeter:timedPrefix"];
            if (!string.IsNullOrEmpty(tg))
                timedGreet = bool.Parse(tg);
            this.time = util;
            var acn = config["greeter:allCapsName"];
            if (!string.IsNullOrEmpty(acn))
                allCapsName = bool.Parse(acn);
            logger.LogInformation($"ConfigurableGreetService #{GetHashCode()} created with prefix ='{prefix}' and suffix='{suffix}' and timedGreet={timedGreet} and allCapsName={allCapsName}");
        }
        public string Greet(string name)
        {
            if (allCapsName)
                name = name.ToUpper();

           var message= $"{Prefix} {name}{suffix}";
           logger.LogInformation($"service #{GetHashCode()}:{message}");

            return message;
        }
    }
}
