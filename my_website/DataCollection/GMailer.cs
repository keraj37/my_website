using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace my_website.DataCollection
{
    public class GMailer
    {
        public const string MY_EMAIL = "jerry.switalski@gmail.com";

        public static string GmailUsername { get; set; }
        public static string GmailPassword { get; set; }
        public static string GmailHost { get; set; }
        public static int GmailPort { get; set; }
        public static bool GmailSSL { get; set; }

        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }

        private static Random rnd;

        static GMailer()
        {
            GmailHost = "smtp.gmail.com";
            GmailPort = 25; // Gmail can use ports 25, 465 & 587; but must be 25 for medium trust environment.
            GmailSSL = true;
            rnd = new Random();
        }

        public void Send()
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Host = GmailHost;
            smtp.Port = GmailPort;
            smtp.EnableSsl = GmailSSL;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(GmailUsername, GmailPassword);

            using (var message = new MailMessage(GmailUsername, ToEmail))
            {
                message.Subject = Subject;
                message.Body = Body;
                message.IsBodyHtml = IsHtml;
                smtp.Send(message);
            }
        }

        public void SendSPM(string user = "mucha", string username = "Omas Kartoffel", string to = MY_EMAIL, string toname = "Matthew Drake", string pass = "somepass")
        {
            var fromAddress = new MailAddress(user + "@quisutdeus.in", username);
            var toAddress = new MailAddress(to, toname);
            string fromPassword = pass;

            var smtp = new SmtpClient
            {
                Host = "quisutdeus.in",
                Port = 25,
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            var subjects = new string[] { "Hey, I want website", "Do you do the web home sites?", "How much for a website?", "Why this is?", "Bussiness inquiryr - IT service needed", "USA - does you do there wbesites?", "How many sites you do?", "My grandmother wants wbesite" };
            var bodies = new string[] { "Hi do you have me to do for me website? Tommorow? Thanks.", "Hey, I want website", "Do you do the web home sites?", "How much for a website?", "Why this is?", "Bussiness inquiryr - IT service needed", "USA - does you do there wbesites?", "How many sites you do?", "My grandmother wants wbesite", "Frank Sinatra has a website orr not?", "Did you see my dog? He is huge", "Oh dear, what am I doing here", "Why does this things needs to be that much?", "How are you. I am person from Uganda. Do you do the www?" };

            string subject = subjects[rnd.Next(subjects.Length)];
            string body = bodies[rnd.Next(bodies.Length)];
            int res = rnd.Next(3);

            for (int i = 0; i < res; i++)
                subject = "Re: " + subject;

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}