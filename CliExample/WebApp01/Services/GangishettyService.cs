using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebApp01.Services
{
    public class GangishettyService : IGangishettyService
    {
        public GangishettyService()
        {
            Console.WriteLine($"GangishettyService created with id {GetHashCode()}");
        }
        public string Greet(string name)
        {
            return $"Self Project {GetHashCode()}: Hello {name}, Welcome to our Self Service";
        }
    }
}
