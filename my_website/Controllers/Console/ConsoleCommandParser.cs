using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using my_website.Controllers.Console.Commands;

namespace my_website.Controllers.Console
{
    public class ConsoleCommandParser
    {
        private static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("You are lame programmer!");
            return input.First().ToString().ToUpper() + input.Substring(1);
        }

        public static ConsoleReturnVo Parse(string cmd, ProjectsController controller)
        {
            if (string.IsNullOrEmpty(cmd))
                return new ConsoleReturnVo("?");

            string[] ss = cmd.Split(" "[0]);

            Type commandtype = Type.GetType(BaseCommand.COMMANDS_NS + "." + FirstCharToUpper(ss[0].ToLower()) + "Command");
            if (commandtype != null)
            {
                bool doexecute = true;
                ConsoleCommandAttribute attribute = commandtype.GetCustomAttribute<ConsoleCommandAttribute>();
                if (attribute != null)
                {
                    if (!controller.User.IsInRole(attribute.Role))
                        doexecute = false;
                }

                if (doexecute && !commandtype.IsAbstract)
                    return ((BaseCommand)Activator.CreateInstance(commandtype, controller)).Execute(ss);
                else
                    return new ConsoleReturnVo("You are not authorized to use that command");
            }

            return new ConsoleReturnVo(cmd);
        }
    }
}