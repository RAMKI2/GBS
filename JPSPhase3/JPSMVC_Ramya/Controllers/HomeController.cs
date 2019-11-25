using JPSMVC_Ramya.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace JPSMVC_Ramya.Controllers
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
    public class HomeController : Controller
    {
        private JobPortalSystemContext1 db = new JobPortalSystemContext1();

        
        public ActionResult Index()
        {
            ViewBag.Message = "Your Home page.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }
}