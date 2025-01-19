using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace blog_web_2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["ActivePage"] = "Home";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewData["ActivePage"] = "About";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewData["ActivePage"] = "Contact";

            return View();
        }
    }
}