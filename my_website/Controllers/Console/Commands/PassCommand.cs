using my_website.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace my_website.Controllers.Console.Commands
{
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
                        Controller.Session["bigpoint"] = true;
                        result.ToAction = "AS3TOCS";
                        result.Message += "Welcome Bigpoint employee. Now you can use AS3TOCS.\nYou are now being redirected to AS3TOCS...";
                        return result;
                    }
                    else
                    {
                        result.Message += "You are already authorized...";
                        return result;
                    }
                }
                else
                {
                    result.Message += "Unknown password...";
                    return result;
                }
            }
            else
            {
                result.Message += "Empty password...";
                return result;
            }
        }
    }
}