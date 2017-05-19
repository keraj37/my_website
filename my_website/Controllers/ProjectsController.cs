using AS3TOCS;
using my_website.DataCollection;
using my_website.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
        public ActionResult Console(bool? auth)
        {
            ConsoleCommand cmd = new ConsoleCommand();
            cmd.Content = (string)Session[CONSOLE];

            return View(cmd);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Console(string cmd)
        {
            DataCollection.DataCollection.Save("Console POST", cmd + "\n\n" + new UserClient(Request).ToString());

            Session[CONSOLE] += AddNewLine(Session[CONSOLE]) + string.Format("[{0}@jerryswitalski.com ~] ", User.Identity.Name) + cmd;

            switch(cmd)
            {
                case "clear":
                    Session[CONSOLE] = null;
                    break;
            }

            string toAction = null;

            string result = Controllers.Console.ConsoleCommandParser.Parse(cmd, this, ref toAction);
            if(!string.IsNullOrEmpty(result))
            {
                Session[CONSOLE] += "\n" + result;
            }

            return Json(new { content = Session[CONSOLE], redirectToAction = toAction });
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
            DataCollection.DataCollection.Save("AI POST", cmd + "\n\n" + new UserClient(Request).ToString());

            Session[AI_NAME] += AddNewLine(Session[AI_NAME]) + string.Format("[{0}@jerryswitalski.com ~] ", User.Identity.Name) + cmd;

            switch (cmd)
            {
                case "clear":
                    Session[AI_NAME] = null;
                    break;
            }

            //string result = Controllers.Console.ConsoleCommandParser.Parse(cmd, this, ref null);
            //if (!string.IsNullOrEmpty(result))
            //{
                //Session[AI_NAME] += "\n" + result;
            //}

            return RedirectToAction("AI");
        }

        [HttpGet]
        public ActionResult AS3TOCS()
        {
            if(!IsBigpointPassSet || IsBigpoint)
            {
                DataCollection.DataCollection.Save("AS3TOCS GET", "Is Bigpoint" + "\n\n" + new UserClient(Request).ToString());
                return View();
            }
            else
            {
                DataCollection.DataCollection.Save("AS3TOCS GET", "Is not Bigpoint" + "\n\n" + new UserClient(Request).ToString());
                Session[CONSOLE] += "\nYou were redirected to console. Please enter your password here, using pass command.";
                return RedirectToAction("Console");
            }            
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AS3TOCS(string source)
        {
            if (IsBigpointPassSet && !IsBigpoint)
            {
                DataCollection.DataCollection.Save("AS3TOCS POST", "ALERT! Is not Bigpoint, but tries POST" + "\n\n" + new UserClient(Request).ToString());
                return RedirectToAction("Console");
            }

            DataCollection.DataCollection.Save("AS3TOCS POST", source + "\n\n" + new UserClient(Request).ToString());

            string[] csString = new AS3TOCSConverter().Convert(source);

            string className = csString[1] ?? "YourCSharpFile";

            return File(Encoding.UTF8.GetBytes(csString[0]), "text/plain", className + ".cs");
        }

        [NonAction]
        private string AddNewLine(object value)
        {
            return !string.IsNullOrEmpty(value as string) ? "\n" : string.Empty;
        }

        public bool IsBigpoint
        {
            get { return Session["bigpoint"] != null && (bool)Session["bigpoint"] == true; }
        }

        public bool IsBigpointPassSet
        {
            get { return !string.IsNullOrEmpty(ConfigurationManager.AppSettings[@"bigpointpass"]); }
        }
    }
}