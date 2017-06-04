using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using my_website.Controllers.Console.Commands.Attributes;
using System.Reflection;

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

        protected ConsoleCommandVariableAttribute.Vo[] GetAllValuesInOrder(string[] cmds)
        {
            var allVariables = this.GetType().GetCustomAttributes<ConsoleCommandVariableAttribute>().OrderBy(x => x.Order);
            var cmdsDic = GetDictionary(cmds);
            ConsoleCommandVariableAttribute.Vo[] result = allVariables.Select(x => x.GetValue(cmdsDic.FirstOrDefault(y => x.KeyName == y.Key).Value)).ToArray<ConsoleCommandVariableAttribute.Vo>();
            return result;
        }

        private Dictionary<string, string> GetDictionary(string[] cmds)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            string key = null;
            for (int i = 1; i < cmds.Length; i++)
            {
                if (key == null)
                {
                    key = cmds[i];
                    if (cmds.Length < i + 2)
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