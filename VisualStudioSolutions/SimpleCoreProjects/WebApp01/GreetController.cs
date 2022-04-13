using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp01
{
    public class GreetController:Controller
    {

        public string Welcome()
        {
            return "Hello Visitor, Welcome";
        }
    }
}
