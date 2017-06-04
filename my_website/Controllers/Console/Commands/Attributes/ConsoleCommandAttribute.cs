using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace my_website.Controllers.Console.Commands.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class ConsoleCommandAttribute : Attribute
    {
        public string Role { get; set; }
    }
}