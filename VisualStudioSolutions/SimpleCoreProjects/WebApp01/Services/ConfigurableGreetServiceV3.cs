using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp01.Services
{
    public enum TextFormat
    {
        None,
        AllCaps,
        NameCaps
    }

    public class GreetConfiguration
    {
        public string Prefix { get; set; }

        public string Suffix { get; set; }

        public bool TimedPrefix { get; set; }

        public TextFormat Format { get; set; }

        public override string ToString()
        {
            return $"Prefix={Prefix}\tSuffix={Suffix}\tTimed={TimedPrefix}\tFormat={Format}";
        }
    }



    public class ConfigurableGreetServiceV3 : IGreetService
    {
        private ILogger<ConfigurableGreetService> logger;
        
        TimeUtil time;
        GreetConfiguration info;

        public string Prefix
        {
            get
            {
                if (info.TimedPrefix)
                    return time.TimedGreet;
                else
                    return info.Prefix;
            }
        }
        public ConfigurableGreetServiceV3(IConfiguration config,ILogger<ConfigurableGreetService> logger,TimeUtil util)
        {
            info = new GreetConfiguration();
            config.Bind("greeter", info);


            this.time = util;
            this.logger = logger;
            logger.LogInformation($"ConfigurableGreetService #{GetHashCode()} created with config: {info}");
        }
        public string Greet(string name)
        {
            if (info.Format==TextFormat.NameCaps)
                name = name.ToUpper();

           var message= $"{Prefix} {name}{info.Suffix}";
            if (info.Format == TextFormat.AllCaps)
                message = message.ToUpper();
           logger.LogInformation($"service #{GetHashCode()}:{message}");

            return message;
        }
    }
}
