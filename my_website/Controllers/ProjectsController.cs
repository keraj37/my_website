using AS3TOCS;
using my_website.Models;
using System;
using System.Collections.Generic;
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
        public ActionResult Console(bool? auth, string redirectToAction)
        {
            string forAuth = (auth != null && auth == true) ? "You were redirected to console. Please eneter your password here, using pass command.\n" : "";

            ConsoleCommand cmd = new ConsoleCommand();
            cmd.Content = forAuth + (string)Session[CONSOLE];

            if (!string.IsNullOrEmpty(redirectToAction))
                cmd.RedirectToAction = redirectToAction;

            return View(cmd);
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

            string toAction = null;

            string result = Controllers.Console.ConsoleCommandParser.Parse(cmd, this, ref toAction);
            if(!string.IsNullOrEmpty(result))
            {
                Session[CONSOLE] += "\n" + result;
            }

            return RedirectToAction("Console", new { redirectToAction = toAction });
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
            if(IsBigpoint())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Console", new { auth = true });
            }            
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AS3TOCS(string source)
        {
            if (!IsBigpoint())
                return RedirectToAction("Console");

            AS3TOCSConverter converter = new AS3TOCSConverter();

            string csString = converter.Convert(source);
                  
            return File(Encoding.UTF8.GetBytes(csString), "text/plain", "YourCSharpClass.cs");
        }

        [NonAction]
        private string AddNewLine(object value)
        {
            return !string.IsNullOrEmpty(value as string) ? "\n" : string.Empty;
        }

        [NonAction]
        private bool IsBigpoint()
        {
            return Session["bigpoint"] != null && (bool)Session["bigpoint"] == true;
            //return true;
        }
    }
}