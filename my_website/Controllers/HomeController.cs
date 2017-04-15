﻿using System;
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
            return View();
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
    }
}
