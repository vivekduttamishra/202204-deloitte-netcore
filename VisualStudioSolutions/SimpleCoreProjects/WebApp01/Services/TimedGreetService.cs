using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp01.Services
{
    public class TimedGreetService : IGreetService
    {
        TimeUtil time = new TimeUtil();
        public TimedGreetService()
        {
            Console.WriteLine($"TimedGreetService created with id {GetHashCode()}");
        }

        
        public string Greet(string name)
        {
            return $" {time.TimedGreet} {name}, Welcome to our Service ";
        }
    }
}

