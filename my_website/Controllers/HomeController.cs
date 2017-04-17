using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace my_website.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Models.UserClient clientData = new Models.UserClient();
            clientData.IP = Request.UserHostAddress;
            clientData.Browser = Request.Browser.Browser;
            return View(clientData);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Short info about me";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "All contacts";

            return View();
        }

        public ActionResult Art()
        {
            ViewBag.Message = "Some of my 3D renders";
            return View("Art");
        }
    }
}
