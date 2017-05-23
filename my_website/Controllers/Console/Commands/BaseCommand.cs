using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace my_website.Controllers.Console.Commands
{
    public abstract class BaseCommand
    {
        protected Controller Controller { get; set; }

        public BaseCommand (Controller controller = null)
        {
            Controller = controller;
        }

        public virtual ConsoleReturnVo Execute(string[] cmd)
        {
            return new ConsoleReturnVo("Command message: ");
        }
    }
}