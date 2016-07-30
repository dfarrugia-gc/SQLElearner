using Microsoft.AspNet.Identity;
using Elearner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Elearner.Controllers
{
    public class EnrollTopicSectionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public object Users { get; private set; }

        // GET: EnrollCourse
        public ActionResult Index()
        {
            var user = User.Identity.GetUserId();
            var userTopicSection = from t in new ApplicationDbContext().UserTopicSections
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
            var db = new ApplicationDbContext();

            var courseTopicSections = db.CourseTopicSections.SingleOrDefault(c => c.CourseTopicSectionId == id);

            var userTopicSection = new UserTopicSection()
            {
                Id = user,
                CourseTopicSectionId = courseTopicSections.CourseTopicSectionId,
                Completed = false
            };

            bool userTopicSectionExists = db.UserTopicSections.Any(x => x.CourseTopicSectionId.Equals(userTopicSection.CourseTopicSectionId) 
            & x.Id.Equals(userTopicSection.Id));

            if (!userTopicSectionExists)
            {
                db.UserTopicSections.Add(userTopicSection);
                db.SaveChanges();
            }
            return View("Index", "CourseTopicSectionViewer");
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

        public ActionResult MarkAsComplete(int? id)
        {
            var user = User.Identity.GetUserId();
            var userTopicSection = db.UserTopicSections.SingleOrDefault(x => x.CourseTopicSectionId == id && x.Id.Equals(user));
            
            if (ModelState.IsValid)
            {
                userTopicSection.Completed = true;

                db.Entry(userTopicSection).State = EntityState.Modified;
                db.SaveChanges();

                var nextTopic = from nt in db.UserTopicSections
                                where nt.CourseTopicSectionId == userTopicSection.CourseTopicSectionId
                                select nt.CourseTopicSection.Order + 1;

                var entollNextTopicSection = new EnrollTopicSectionController().Create(nextTopic.FirstOrDefault(), user);
            }           

            return RedirectToAction("Index", "CourseTopicSectionViewer", new { id = userTopicSection.CourseTopicSection.CourseTopicId, page = id });
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
