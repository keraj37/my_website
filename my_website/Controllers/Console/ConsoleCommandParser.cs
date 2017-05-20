using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace my_website.Controllers.Console
{
    public class ConsoleCommandParser
    {
        public static string Parse(string cmd, ProjectsController controller, ref string toAction)
        {
            if (string.IsNullOrEmpty(cmd))
                return null;

            string[] ss = cmd.Split(" "[0]);

            if(ss.Length >= 2)
            {
                switch (ss[0])
                {
                    case "db":
                        if(controller.User.IsInRole(Users.Users.Roles.ADMIN))
                            return new DataBaseCommand().Execute(ss);
                        else
                            return "You are not authorized to use that command";
                    case "goto":
                        return "So, you want to go to " + ss[1];
                    case "pass":
                        if(ss[1] == ConfigurationManager.AppSettings[@"bigpointpass"])
                        {
                            if (controller.IsBigpointPassSet && !controller.IsBigpoint)
                            {
                                controller.Session["bigpoint"] = true;
                                toAction = "AS3TOCS";
                                return "Welcome Bigpoint employee. Now you can use AS3TOCS.\nYou are now being redirected to AS3TOCS...";
                            }
                            else
                            {
                                return "You are already authorized...";
                            }
                        }                       
                        else
                        {
                            return "Unknown password...";
                        }
                    default:
                        return "I don't understand.";
                }
            }

            return null;
        }
    }
}