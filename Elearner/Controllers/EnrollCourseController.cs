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
    public class EnrollCourseController : Controller
    {
        public object Users { get; private set; }

        // GET: EnrollCourse
        public ActionResult Index()
        {
            var user = User.Identity.GetUserId();
            var userCourseTopic = from t in new ApplicationDbContext().UserCourseTopics
                                  where t.Id == user
                                  select t;
            return View();
        }

        // GET: EnrollCourse/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EnrollCourse/Create
        public ActionResult Create(int? id)
        {
            var db = new ApplicationDbContext();

            var user = User.Identity.GetUserId();
            var course = db.Courses.SingleOrDefault(c => c.CourseId == id);
            var courseTopics = from t in new ApplicationDbContext().CourseTopics
                               where t.CourseId == id
                               select t.Topic.TopicId;

            var userCourseTopic = new UserCourseTopic()
            {
                Id = user,
                CourseId = course.CourseId,
                TopicId = courseTopics.FirstOrDefault()
            };

            bool userCourseTopicExists = db.UserCourseTopics.Any(x => x.CourseId.Equals(userCourseTopic.CourseId) 
            & x.TopicId.Equals(userCourseTopic.TopicId) 
            & x.Id.Equals(userCourseTopic.Id));

            if (!userCourseTopicExists)
            {
                db.UserCourseTopics.Add(userCourseTopic);
                db.SaveChanges();

                return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
            }
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }

        // POST: EnrollCourse/Create
        [HttpPost]
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EnrollCourse/Edit/5
        [HttpPost]
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EnrollCourse/Delete/5
        [HttpPost]
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
