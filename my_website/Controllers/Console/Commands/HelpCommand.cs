using my_website.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using my_website.Controllers.Console.Commands.Attributes;

namespace my_website.Controllers.Console.Commands
{
    [ConsoleCommandDescription(Name = "help", Description = "General help of using this console", Priority = 0)]
    public class HelpCommand : BaseCommand
    {
        private const string HELP = "help_cache";

        private List<ConsoleCommandDescriptionAttribute> attribuesList;

        public static List<ConsoleCommandDescriptionAttribute> GetAllCommandsDesciptions(string search = null)
        {
            List<ConsoleCommandDescriptionAttribute> result = new List<ConsoleCommandDescriptionAttribute>();
            Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.Namespace == COMMANDS_NS).ToList().ForEach(delegate (Type t)
            {
                ConsoleCommandDescriptionAttribute descAttr = t.GetCustomAttribute<ConsoleCommandDescriptionAttribute>();
                if (descAttr != null && (search != null ? descAttr.Name.StartsWith(search) : true))
                    result.Add(descAttr);
            });

            return result;
        }

        public HelpCommand(Controller controller = null) : base(controller)
        {
            attribuesList = GetAllCommandsDesciptions().OrderBy(x => x.Priority).ToList();
        }

        public override ConsoleReturnVo Execute(string[] cmd)
        {
            ConsoleReturnVo result = base.Execute(cmd);
            attribuesList.ForEach(x => result.Message += "\n" + x.ToString());
            return result;
        }
    }
}