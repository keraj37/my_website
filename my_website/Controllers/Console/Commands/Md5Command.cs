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
    [ConsoleCommandDescription(Name = "md5", Description = "Generates MD5 hash", Priority = 87)]
    public class Md5Command : BaseCommand
    {
        public Md5Command(Controller controller = null) : base(controller)
        {
        }

        public override ConsoleReturnVo Execute(string[] cmd)
        {
            ConsoleReturnVo result = base.Execute(cmd);

            if (cmd.Length > 1)
            {
                result.Message = "\n" + CalculateMD5Hash(cmd[1]);
            }
            else
            {
                result.Message += "\nEmpty string...";
                return result;
            }

            return result;
        }

        private string CalculateMD5Hash(string input)
        {
            MD5 md5 = MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }

            return sb.ToString();
        }
    }
}