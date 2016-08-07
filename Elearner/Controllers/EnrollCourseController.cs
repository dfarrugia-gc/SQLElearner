using Microsoft.AspNet.Identity;
using Elearner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Elearner.Controllers
{
    public class EnrollCourseController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public object Users { get; private set; }

        // GET: EnrollCourse
        public ActionResult Index()
        {
            var user = User.Identity.GetUserId();
            var userCourseTopic = from t in new ApplicationDbContext().UserCourses
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
        public ActionResult Create(int? id, string user)
        {
            if(string.IsNullOrEmpty(user))
            {
                return RedirectToAction("Details", "Courses");
            }
            else
            {
                var course = db.Courses.SingleOrDefault(c => c.CourseId == id);
                var courseTopics = from t in new ApplicationDbContext().CourseTopics
                                   where t.CourseId == id
                                   select t.Topic.TopicId;

                var userCourse = new UserCourse()
                {
                    Id = user,
                    CourseId = course.CourseId
                };

                bool userCourseExists = db.UserCourses.Any(x => x.CourseId.Equals(userCourse.CourseId)
                & x.Id.Equals(userCourse.Id));

                if (!userCourseExists)
                {
                    db.UserCourses.Add(userCourse);
                    db.SaveChanges();
                }
                return RedirectToAction("Index", "CourseTopicViewer", new { id = userCourse.CourseId });
            }
            
        }

        public ActionResult MarkAsComplete(int? id, string user)
        {
            var userCourse = db.UserCourses.SingleOrDefault(x => x.CourseId == id && x.Id.Equals(user));
            //var courseTopicSection = db.CourseTopicSections.Where(x => x.CourseTopicId == userTopicSection.CourseTopicSection.CourseTopicId);

            if (ModelState.IsValid)
            {
                userCourse.Completed = true;

                db.Entry(userCourse).State = EntityState.Modified;
                db.SaveChanges();
            }
            return null;
        }

        // POST: EnrollCourse/Create
        [Authorize(Roles = "Admin")]
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
