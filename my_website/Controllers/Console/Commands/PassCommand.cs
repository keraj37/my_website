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
    [ConsoleCommandDescription(Name = "pass", Description = "Gives session authorization after entering correct password", Priority = 2)]
    public class PassCommand : BaseCommand
    {
        public PassCommand(Controller controller = null):base(controller)
        {
        }

        public override ConsoleReturnVo Execute(string[] cmd)
        {
            ConsoleReturnVo result = base.Execute(cmd);

            if(cmd.Length > 1)
            {
                if (cmd[1] == ConfigurationManager.AppSettings[@"bigpointpass"])
                {
                    if ((Controller as ProjectsController).IsBigpointPassSet && !(Controller as ProjectsController).IsBigpoint)
                    {
                        Controller.Session[ProjectsController.BIGPOINT] = true;
                        result.ToAction = "AS3TOCS";
                        result.Message += "\nWelcome Bigpoint employee. Now you can use AS3TOCS.\nYou are now being redirected to AS3TOCS...";
                        return result;
                    }
                    else
                    {
                        result.Message += "\nYou are already authorized...";
                        return result;
                    }
                }
                else
                {
                    result.Message += "\nUnknown password...";
                    return result;
                }
            }
            else
            {
                result.Message += "\nEmpty password...";
                return result;
            }
        }
    }
}