using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace my_website.Controllers.Console
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class ConsoleCommandDescriptionAttribute : Attribute
    {
        public int Priority { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return Name + ": " + Description;
        }
    }
}