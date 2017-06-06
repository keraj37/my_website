using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using my_website.Controllers.Console.Commands.Interfaces;

namespace my_website.Controllers.Console.Commands.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class ConsoleCommandStrategyAttribute : Attribute
    {
        private Type Type { get; set; }

        public ConsoleCommandStrategyAttribute(Type type)
        {
            Type = type;
        }

        public IConsoleCommandStrategy GetInstance()
        {
            return (IConsoleCommandStrategy)Activator.CreateInstance(Type);
        }
    }
}