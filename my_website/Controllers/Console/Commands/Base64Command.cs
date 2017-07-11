using my_website.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using my_website.Controllers.Console.Commands.Attributes;
using System.Security.Cryptography;
using System.Text;

namespace my_website.Controllers.Console.Commands
{
    [ConsoleCommandDescription(Name = "base64", Description = "Encodes to Base64", Priority = 88)]
    public class Base64Command : BaseCommand
    {
        public Base64Command(Controller controller = null) : base(controller)
        {
        }

        public override ConsoleReturnVo Execute(string[] cmd)
        {
            ConsoleReturnVo result = base.Execute(cmd);

            if (cmd.Length > 1)
            {
                result.Message = "\n" + GetBase64(cmd[1]);
            }
            else
            {
                result.Message += "\nEmpty string...";
                return result;
            }

            return result;
        }

        private string GetBase64(string input)
        {
            byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            string result = System.Convert.ToBase64String(inputBytes);
            return result;
        }
    }
}