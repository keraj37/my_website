using my_website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace my_website.Controllers.Console
{
    public class DataBaseCommand : BaseCommand
    {
        private ApplicationDbContext db;

        public DataBaseCommand()
        {
            db = new ApplicationDbContext();
        }

        public override string Execute(string[] cmd)
        {
            string result = base.Execute(cmd);
            switch (cmd[1])
            {
                case "data":
                    if(cmd.Length > 2)
                    {
                        switch (cmd[2])
                        {
                            case "clear":
                                if (cmd.Length > 3)
                                {
                                    result = "Not implemented yet...";
                                }
                                else
                                {
                                    db.Database.ExecuteSqlCommand("delete from Data");
                                    result += "Cleared: " + cmd[1];
                                }
                                break;
                            default:
                                result += "db error: " + 101;
                                break;
                        }
                    }
                    else
                    {
                        IEnumerable<Data> dbresult = db.DataCollections;
                        foreach(Data d in dbresult)
                        {
                            result += d.ToString() + "\n----------------------------------------\n";
                        }
                    }
                    break;
                default:
                    result += "db error: " + 100;
                    break;
            }

            return result;
        }
    }
}