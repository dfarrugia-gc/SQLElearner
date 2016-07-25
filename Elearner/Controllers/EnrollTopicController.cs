using Microsoft.AspNet.Identity;
using Elearner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Elearner.Controllers
{
    public class EnrollTopicController : Controller
    {
        public object Users { get; private set; }

        // GET: EnrollCourse
        public ActionResult Index()
        {
            var user = User.Identity.GetUserId();
            var userTopic = from t in new ApplicationDbContext().UserTopics
                                  where t.Id == user
                                  select t;
            return View();
        }

        // GET: EnrollCourse/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EnrollCourse/Create
        public ActionResult Create(int? id)
        {
            var db = new ApplicationDbContext();

            var user = User.Identity.GetUserId();
            var courseTopics = db.CourseTopics.SingleOrDefault(c => c.CourseTopicId == id);

            var userTopic = new UserTopic()
            {
                Id = user,
                CourseTopicId = courseTopics.CourseTopicId,
                Completed = false
            };

            bool userTopicExists = db.UserTopics.Any(x => x.CourseTopicId.Equals(userTopic.CourseTopicId) 
            & x.Id.Equals(userTopic.Id));

            if (!userTopicExists)
            {
                db.UserTopics.Add(userTopic);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "CourseTopicSectionViewer", new { id = courseTopics.CourseTopicId });
        }

        // POST: EnrollCourse/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EnrollCourse/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EnrollCourse/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EnrollCourse/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EnrollCourse/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
