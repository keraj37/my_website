using my_website.Controllers.Console;
using my_website.DataCollection;
using my_website.Hubs;
using my_website.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace my_website.Controllers
{
    public class ProjectsController : Controller
    {
        public const string CONSOLE = "console";
        public const string BIGPOINT = "bigpoint";
        private const string AI_NAME = "ai";

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Chat()
        {
            return View();
        }

        [HttpGet]
        public ActionResult MandelbrotSet()
        {
            //FractalImage model = new MandelbrotSet.MandelbrotSet().GetImage();
            return View(/*model*/);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult MandelbrotSet(string cmd, bool? tab)
        {
            DataCollection.DataCollection.Save(Request.UserHostAddress, "Mandelbrot POST", cmd + "\n\n" + new UserClient(Request).ToString());
            return GenericProcessCommand(cmd, false);
        }

        [HttpGet]
        public ActionResult Console(bool? auth)
        {
            if (string.IsNullOrEmpty(Session[CONSOLE] as string)) Session[CONSOLE] += "Type 'help' for list of commands. Use TAB key for tab completion. Use ENTER key or 'Execute' button to send command.";
            ConsoleOutput cmd = new ConsoleOutput();
            cmd.Content = (string)Session[CONSOLE];

            return View(cmd);
        }

        [NonAction]
        public JsonResult GenericProcessCommand(string cmd, bool? tab)
        {
            ConsoleReturnVo result = Controllers.Console.ConsoleCommandParser.Parse(cmd, tab, this);
            if (!string.IsNullOrEmpty(result.Message))
            {
                Session[CONSOLE] += result.Message;
            }

            return Json(result.ToObject());
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Console(string cmd, bool? tab)
        {
            DataCollection.DataCollection.Save(Request.UserHostAddress, "Console POST", cmd + "\n\n" + new UserClient(Request).ToString());

            if (!(tab ?? false))
                Session[CONSOLE] += AddNewLine(Session[CONSOLE]) + string.Format("[{0}@quisutdeus.in ~] ", User.Identity.Name) + cmd;

            return GenericProcessCommand(cmd, tab);
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
            DataCollection.DataCollection.Save(Request.UserHostAddress, "AI POST", cmd + "\n\n" + new UserClient(Request).ToString());

            Session[AI_NAME] += AddNewLine(Session[AI_NAME]) + string.Format("[{0}@quisutdeus.in ~] ", User.Identity.Name) + cmd;

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
                DataCollection.DataCollection.Save(Request.UserHostAddress, "AS3TOCS GET", "Is Bigpoint" + "\n\n" + new UserClient(Request).ToString());
                return View();
            }
            else
            {
                DataCollection.DataCollection.Save(Request.UserHostAddress, "AS3TOCS GET", "Is not Bigpoint" + "\n\n" + new UserClient(Request).ToString());
                Session[CONSOLE] += "\nYou were redirected to console. Please enter your password here, using pass command.";
                return RedirectToAction("Console");
            }            
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AS3TOCS(string source, string key)
        {
            if (IsBigpointPassSet && !IsBigpoint)
            {
                DataCollection.DataCollection.Save(Request.UserHostAddress, "AS3TOCS POST", "ALERT! Is not Bigpoint, but tries POST" + "\n\n" + new UserClient(Request).ToString());
                return RedirectToAction("Console");
            }

            DataCollection.DataCollection.Save(Request.UserHostAddress, "AS3TOCS POST", source + "\n\n" + new UserClient(Request).ToString());

            string[] csString = new AS3TOCS.AS3TOCS().Convert(source);

            string className = csString[1] ?? "YourCSharpFile";

            if(!string.IsNullOrEmpty(key))
            {
                As3tocsHub hub = new As3tocsHub();
                hub.Send(key, className, csString[0]);
            }

            return File(Encoding.UTF8.GetBytes(csString[0]), "text/plain", className + ".cs");
        }

        [HttpGet]
        public ActionResult Threejs()
        {
            return View();
        }

        [HttpGet]
        public ActionResult WebCam()
        {
            return View();
        }

        [HttpPost]
        public void WebCam(string name, string image)
        {
            GeneralHub hub = new GeneralHub();
            hub.UpdateWebCamStream(name, image);
        }

        [NonAction]
        private string AddNewLine(object value)
        {
            return !string.IsNullOrEmpty(value as string) ? "\n" : string.Empty;
        }

        public bool IsBigpoint
        {
            get { return Session[BIGPOINT] != null && (bool)Session[BIGPOINT] == true; }
        }

        public bool IsBigpointPassSet
        {
            get { return !string.IsNullOrEmpty(ConfigurationManager.AppSettings[@"bigpointpass"]); }
        }
    }
}