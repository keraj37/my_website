using my_website.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using my_website.Controllers.Console.Commands.Attributes;

namespace my_website.Controllers.Console.Commands
{
    [ConsoleCommandDescription(Name = "clear", Description = "Clears console output", Priority = 1)]
    public class ClearCommand : BaseCommand
    {
        public ClearCommand(Controller controller = null):base(controller)
        {
        }

        public override ConsoleReturnVo Execute(string[] cmd)
        {
            Controller.Session[ProjectsController.CONSOLE] = null;
            return new ConsoleReturnVo(string.Empty);
        }
    }
}