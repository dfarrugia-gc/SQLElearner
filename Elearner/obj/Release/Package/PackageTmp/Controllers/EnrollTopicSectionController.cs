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
            var userTopics = db.UserTopics.Where(ut => ut.Id == user).ToList();

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
            var courseTopicSection = db.CourseTopicSections.Where(x=>x.CourseTopicId == userTopicSection.CourseTopicSection.CourseTopicId);
            

            userTopicSection.Completed = true;

            db.Entry(userTopicSection).State = EntityState.Modified;
            db.SaveChanges();

            var nextTopicSection = db.CourseTopicSections.Where(x => x.Order == userTopicSection.CourseTopicSection.Order + 1 && x.CourseTopicId == userTopicSection.CourseTopicSection.CourseTopicId).ToList();

            if (nextTopicSection.Count() != 0)
            {
                if (nextTopicSection.Max(x => x.Order) > courseTopicSection.Max(m => m.Order))
                {
                    var nextTopic = (from nt in db.CourseTopics
                                     where nt.TopicOrder == (userTopicSection.CourseTopicSection.CourseTopic.TopicOrder + 1)
                                     select nt.CourseTopicId).ToList();

                    var markAsComplete = new EnrollTopicController().MarkAsComplete(userTopicSection.CourseTopicSection.CourseTopicId, user);
                    var enrollNextTopic = new EnrollTopicController().Create(nextTopic.First(), user);
                    return RedirectToAction("Index", "CourseTopicViewer", new { id = userTopicSection.CourseTopicSection.CourseTopic.CourseId, page = id });
                }
                else
                {
                    var entollNextTopicSection = new EnrollTopicSectionController().Create(nextTopicSection.Max(x => x.CourseTopicSectionId), user);
                    return RedirectToAction("Index", "CourseTopicSectionViewer", new { id = userTopicSection.CourseTopicSection.CourseTopicId, page = nextTopicSection.Max(x => x.Order) });
                }
            }
            else
            {
                var nextTopic = (from nt in db.CourseTopics
                                 where nt.TopicOrder == (userTopicSection.CourseTopicSection.CourseTopic.TopicOrder + 1)
                                 select nt.CourseTopicId).ToList();

                var markAsComplete = new EnrollTopicController().MarkAsComplete(userTopicSection.CourseTopicSection.CourseTopicId, user);
                if(nextTopic.Count() == 0)
                {
                    var markCourseAsComplete = new EnrollCourseController().MarkAsComplete(userTopicSection.CourseTopicSection.CourseTopic.CourseId, user);
                    return RedirectToAction("Index", "CourseTopicViewer", new { id = userTopicSection.CourseTopicSection.CourseTopic.CourseId, page = id });
                }
                else
                {
                    var enrollNextTopic = new EnrollTopicController().Create(nextTopic.First(), user);
                    return RedirectToAction("Index", "CourseTopicViewer", new { id = userTopicSection.CourseTopicSection.CourseTopic.CourseId, page = id });
                }                
                
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
