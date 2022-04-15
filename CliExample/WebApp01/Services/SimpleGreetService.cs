using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp01.Services
{
    public class SimpleGreetService : IGreetService
    {
        public SimpleGreetService()
        {
            Console.WriteLine($"SimpleGreetService created with id {GetHashCode()}");
        }
        public string Greet(string name)
        {
            return $"Simple Greeter {GetHashCode()}: Hello {name}, Welcome to our Service";
        }
    }
}
