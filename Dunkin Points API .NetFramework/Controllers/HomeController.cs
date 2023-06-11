using Dunkin_Points_API.NetFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dunkin_Points_API.NetFramework.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            Utility.getAuthHeader("post", "application/json", "checkbalance");
            return View();
        }
    }
}
