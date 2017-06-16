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
    public class SpamCommand : BaseCommand
    {
        private static GMailer mailer;

        public SpamCommand(Controller controller = null) : base(controller)
        {
            mailer = new GMailer();
            
            GMailer.GmailUsername = GMailer.MY_EMAIL;
            GMailer.GmailPassword = ConfigurationManager.AppSettings[@"gmailPass"];
        }

        public override ConsoleReturnVo Execute(string[] cmd)
        {
            ConsoleCommandVariableAttribute.Values vals = GetAllValuesInOrder(cmd);
            ConsoleMandelbrotReturnVo result = new ConsoleMandelbrotReturnVo("\nSpam responses sent...");

            mailer.ToEmail = vals.GetValue("email").StringValue;

            int num = vals.GetValue("i").IntValue;
            int sleep = vals.GetValue("sleep").IntValue;

            new Thread(x => Sender(num, sleep));

            return result;
        }

        private void Sender(int num, int sleep)
        {
            for (int i = 0; i < num; i++)
            {
                mailer.Subject = "Re: Vorschlag für Webpräsenz (Proposal for website)";
                mailer.Body = "hi :)";
                mailer.IsHtml = false;
                mailer.Send();

                Thread.Sleep(sleep);
            }
        }
    }
}