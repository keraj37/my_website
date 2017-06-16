using my_website.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using my_website.Controllers.Console.Commands.Attributes;
using my_website.DataCollection;
using System.Threading;

namespace my_website.Controllers.Console.Commands
{
    [ConsoleCommand(Role = Users.Users.Roles.ADMIN)]
    [ConsoleCommandDescription(Name = "spam", Description = "Send response to spam", Priority = 12)]
    [ConsoleCommandVariable(KeyName = "to", Type = ConsoleCommandVariableAttribute.CommandValueType.STRING, DefaultValue = GMailer.MY_EMAIL)]
    [ConsoleCommandVariable(KeyName = "subject", Type = ConsoleCommandVariableAttribute.CommandValueType.STRING, DefaultValue = "Re: Proposal for website")]
    [ConsoleCommandVariable(KeyName = "body", Type = ConsoleCommandVariableAttribute.CommandValueType.STRING, DefaultValue = "Can I have a website? Thank you!")]
    [ConsoleCommandVariable(KeyName = "pass", Type = ConsoleCommandVariableAttribute.CommandValueType.STRING, DefaultValue = "sfgh47fnv722v")]
    [ConsoleCommandVariable(KeyName = "i", Type = ConsoleCommandVariableAttribute.CommandValueType.INT, DefaultValue = 1)]
    [ConsoleCommandVariable(KeyName = "sleep", Type = ConsoleCommandVariableAttribute.CommandValueType.INT, DefaultValue = 1000)]
    public class SpamCommand : BaseCommand
    {
        private static GMailer mailer;

        public SpamCommand(Controller controller = null) : base(controller)
        {
            mailer = new GMailer();
        }

        public override ConsoleReturnVo Execute(string[] cmd)
        {
            ConsoleCommandVariableAttribute.Values vals = GetAllValuesInOrder(cmd);
            ConsoleMandelbrotReturnVo result = new ConsoleMandelbrotReturnVo("\nSpam responses sent...");

            string to = vals.GetValue("to").StringValue;
            int num = vals.GetValue("i").IntValue;
            int sleep = vals.GetValue("sleep").IntValue;
            string subject = vals.GetValue("subject").StringValue;
            string body = vals.GetValue("body").StringValue;
            string pass = vals.GetValue("pass").StringValue;

            new Thread(x => Sender(num, sleep, to, pass)).Start();

            return result;
        }

        private void Sender(int num, int sleep, string to, string pass)
        {
            for (int i = 0; i < num; i++)
            {
                mailer.SendSPM(to: to, pass: pass);
                Thread.Sleep(sleep);
            }
        }
    }
}