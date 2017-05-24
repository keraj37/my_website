using my_website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace my_website.Controllers.Console.Commands
{
    [ConsoleCommand(Role = Users.Users.Roles.ADMIN)]
    [ConsoleCommandDescription(Name = "database", Description = "Operations on this site's database. You need \"admin\" role to use it", Priority = 3)]
    public class DatabaseCommand : BaseCommand
    {
        private ApplicationDbContext db;

        public DatabaseCommand(Controller controller = null):base(controller)
        {
            db = new ApplicationDbContext();
        }

        public override ConsoleReturnVo Execute(string[] cmd)
        {
            ConsoleReturnVo result = base.Execute(cmd);
            if(cmd.Length > 1)
            {
                switch (cmd[1])
                {
                    case "data":
                        if (cmd.Length > 2)
                        {
                            switch (cmd[2])
                            {
                                case "clear":
                                    if (cmd.Length > 3)
                                    {
                                        result.Message += "Not implemented yet...";
                                    }
                                    else
                                    {
                                        db.Database.ExecuteSqlCommand("delete from Data");
                                        result.Message += "Cleared: " + cmd[1];
                                    }
                                    break;
                                default:
                                    result.Message += "db error: " + 101;
                                    break;
                            }
                        }
                        else
                        {
                            IEnumerable<Data> dbresult = db.DataCollections;
                            foreach (Data d in dbresult)
                            {
                                result.Message += d.ToString() + "\n----------------------------------------\n";
                            }
                        }
                        break;
                    default:
                        result.Message += "db error: " + 100;
                        break;
                }
            }
            else
            {
                result.Message += "This command needs params";
            }

            return result;
        }
    }
}