using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace my_website.Controllers
{
    public class ProjectsController : Controller
    {
        private const string CONSOLE = "console";

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Console()
        {
            return View(Session[CONSOLE]);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Console(string cmd)
        {
            Session[CONSOLE] += AddNewLine() + string.Format("[{0}@jerryswitalski.com ~] ", User.Identity.Name) + cmd;

            switch(cmd)
            {
                case "clear":
                    Session[CONSOLE] = null;
                    break;
            }

            string result = Controllers.Console.ConsoleCommandParser.Parse(cmd);
            if(!string.IsNullOrEmpty(result))
            {
                Session[CONSOLE] += "\n" + result;
            }

            return RedirectToAction("Console");
        }

        [NonAction]
        private string AddNewLine()
        {
            return Session[CONSOLE] != null && Session[CONSOLE].ToString() != string.Empty ? "\n" : string.Empty;
        }
    }
}