using my_website.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace my_website.Controllers.Console.Commands
{
    public class HelpCommand : BaseCommand
    {
        private Dictionary<string, string> commands = new Dictionary<string, string>
        {
            { "help", "General help of usuing this console"},
            { "clear", "Clears console output"},
            { "pass", "Gives session authorization after entering correct password"},
            { "database", "Operations on this site's database. You need \"admin\" role to use it"}
        };

        public HelpCommand(Controller controller = null):base(controller)
        {
        }

        public override ConsoleReturnVo Execute(string[] cmd)
        {
            ConsoleReturnVo result = base.Execute(cmd);

            foreach(KeyValuePair<string, string> kvp in commands)
            {
                result.Message += "\n" + kvp.Key + ": " + kvp.Value;
            }

            return result;
        }
    }
}