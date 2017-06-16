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

        static DataCollection()
        {
            mailer = new GMailer();
            mailer.ToEmail = GMailer.MY_EMAIL;
            GMailer.GmailUsername = GMailer.MY_EMAIL;
            GMailer.GmailPassword = ConfigurationManager.AppSettings[@"gmailPass"];
        }

        public static void Save(string IP, string subject, string body)
        {
            if (!bool.Parse(ConfigurationManager.AppSettings[@"dataCollection"] ?? "false"))
                return;

            string excludedIps = ConfigurationManager.AppSettings[@"dataCollectionIpExclude"];

            if (!string.IsNullOrEmpty(excludedIps) && excludedIps.Split("|"[0]).FirstOrDefault(x => x.Equals(IP)) != default(string))
                return;

            ThreadPool.QueueUserWorkItem(delegate
            {
                if (bool.Parse(ConfigurationManager.AppSettings[@"sendDataCollectionEmails"] ?? "false"))
                {
                    mailer.Subject = "[mywebsite] " + subject;
                    mailer.Body = body;
                    mailer.IsHtml = false;
                    mailer.Send();
                }

                Data data = new Data();
                data.Subject = subject;
                data.Body = body;
                data.Time = DateTime.Now;

                db.DataCollections.Add(data);
                db.SaveChanges();
            }, null);
        }
    }
}