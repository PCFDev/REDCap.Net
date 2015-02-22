using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using REDCapMVC.Models;

namespace REDCapMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var temp = new REDCapClient.REDCapStudy();

            using (var db = new StudyContext())
            {
                db.Studies.Add(new REDCapClient.REDCapStudy
                {
                    ApiKey = "SSS",
                    StudyName = "PPPP",
                    Events = new List<REDCapClient.Event>(),
                    Metadata = new List<REDCapClient.Metadata>()
                });

                db.SaveChanges();

                temp = db.Studies.FirstOrDefault();
            }

            return View(temp);
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