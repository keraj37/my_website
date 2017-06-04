using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace my_website.Controllers.Console.Commands
{
    public abstract class BaseCommand
    {
        public const string COMMANDS_NS = "my_website.Controllers.Console.Commands";

        protected Controller Controller { get; set; }

        public BaseCommand (Controller controller = null)
        {
            Controller = controller;
        }

        public virtual ConsoleReturnVo Execute(string[] cmd)
        {
            return new ConsoleReturnVo("Command message: ");
        }

        protected Dictionary<string, string> GetAllKeyValues(string[] cmds)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            string key = null;
            for(int i = 1; i < cmds.Length; i++)
            {
                if (key == null)
                {
                    key = cmds[i];
                    if(cmds.Length < i + 2)
                    {
                        result.Add(key, null);
                    }
                }
                else
                {
                    result.Add(key, cmds[i]);
                    key = null;
                }
            }

            return result;
        }
    }
}