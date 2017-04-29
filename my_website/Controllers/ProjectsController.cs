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
        private const string AI_NAME = "ai";

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
            Session[CONSOLE] += AddNewLine(Session[CONSOLE]) + string.Format("[{0}@jerryswitalski.com ~] ", User.Identity.Name) + cmd;

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

        [HttpGet]
        public ActionResult AI()
        {
            return View(Session[AI_NAME]);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AI(string cmd)
        {
            Session[AI_NAME] += AddNewLine(Session[AI_NAME]) + string.Format("[{0}@jerryswitalski.com ~] ", User.Identity.Name) + cmd;

            switch (cmd)
            {
                case "clear":
                    Session[AI_NAME] = null;
                    break;
            }

            string result = Controllers.Console.ConsoleCommandParser.Parse(cmd);
            if (!string.IsNullOrEmpty(result))
            {
                Session[AI_NAME] += "\n" + result;
            }

            return RedirectToAction("AI");
        }

        [NonAction]
        private string AddNewLine(object value)
        {
            return !string.IsNullOrEmpty(value as string) ? "\n" : string.Empty;
        }
    }
}