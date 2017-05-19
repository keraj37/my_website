using my_website.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;

namespace my_website.DataCollection
{
    public class DataCollection
    {
        private static GMailer mailer;
        private static ApplicationDbContext db = new ApplicationDbContext();
        private const string MY_EMAIL = "jerry.switalski@gmail.com";

        static DataCollection()
        {
            mailer = new GMailer();
            mailer.ToEmail = MY_EMAIL;
            GMailer.GmailUsername = MY_EMAIL;
            GMailer.GmailPassword = ConfigurationManager.AppSettings[@"gmailPass"];
        }

        public static void Save(string subject, string body)
        {
            ThreadPool.QueueUserWorkItem(delegate {

                if(bool.Parse(ConfigurationManager.AppSettings[@"sendDataCollectionEmails"]))
                {
                    mailer.Subject = "[mywebsite] " + subject;
                    mailer.Body = body;
                    mailer.IsHtml = false;
                    mailer.Send();
                }

                Data data = new Data();
                data.Subject = subject;
                data.Body = body;

                db.DataCollections.Add(data);
                db.SaveChanges();
            }, null);
        }
    }
}