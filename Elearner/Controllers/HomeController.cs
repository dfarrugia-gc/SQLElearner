using Elearner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Elearner.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            List<object> myModel = new List<object>();
            myModel.Add(db.Courses.ToList());
            myModel.Add(db.CourseTopics.ToList());
            myModel.Add(db.Topics.ToList());

            return View(myModel); ;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Elearner";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact";

            return View();
        }
    }
}