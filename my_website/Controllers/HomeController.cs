using my_website.DataCollection;
using my_website.Models;
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
            DataCollection.DataCollection.Save(Request.UserHostAddress, "Home/Index GET", "Someone opened site main node" + "\n\n" + new UserClient(Request).ToString());
            return View(new Models.UserClient(Request));
        }

        public ActionResult About()
        {
            DataCollection.DataCollection.Save(Request.UserHostAddress, "About GET", "Someone is looking at About page" + "\n\n" + new UserClient(Request).ToString());
            return View();
        }

        public ActionResult Contact()
        {
            DataCollection.DataCollection.Save(Request.UserHostAddress, "Contact GET", "Someone is looking at Contact page" + "\n\n" + new UserClient(Request).ToString());
            return View();
        }

        public ActionResult Art()
        {
            DataCollection.DataCollection.Save(Request.UserHostAddress, "Art/3D GET", "Someone is looking at 3D page" + "\n\n" + new UserClient(Request).ToString());
            return View("Art");
        }

        public ActionResult Arma3()
        {
            DataCollection.DataCollection.Save(Request.UserHostAddress, "Arma3 GET", "Someone is looking at Arma3 page" + "\n\n" + new UserClient(Request).ToString());
            return View();
        }
    }
}
