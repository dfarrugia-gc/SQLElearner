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
    public class EnrollTopicController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
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
        public ActionResult Create(int? id, string user)
        {
            var db = new ApplicationDbContext();

            var usr = user ?? User.Identity.GetUserId();
            var courseTopics = db.CourseTopics.SingleOrDefault(c => c.CourseTopicId == id);

            var userTopic = new UserTopic()
            {
                Id = usr,
                CourseTopicId = courseTopics.CourseTopicId,
                Completed = false
            };

            bool userTopicExists = db.UserTopics.Any(x => x.CourseTopicId.Equals(userTopic.CourseTopicId) 
            & x.Id.Equals(userTopic.Id));

            if (!userTopicExists)
            {
                db.UserTopics.Add(userTopic);
                db.SaveChanges();

                var courseTopicSections = db.CourseTopicSections.Where(cts => cts.CourseTopicId == id).OrderBy(cts => cts.Order).ToList();
                if(courseTopicSections.Count() == 0)
                {
                    return HttpNotFound();
                }
                else
                {
                    var firstCourseTopicSection = new EnrollTopicSectionController().Create(courseTopicSections.FirstOrDefault(f => f.CourseTopicId == id).CourseTopicSectionId, usr);
                }
                
            }
            return RedirectToAction("Index", "CourseTopicSectionViewer", new { id = courseTopics.CourseTopicId });
        }

        public ActionResult MarkAsComplete(int? id, string user)
        {
            var userTopic = db.UserTopics.SingleOrDefault(x => x.CourseTopicId == id && x.Id.Equals(user));
            //var courseTopicSection = db.CourseTopicSections.Where(x => x.CourseTopicId == userTopicSection.CourseTopicSection.CourseTopicId);

            if (ModelState.IsValid)
            {
                userTopic.Completed = true;

                db.Entry(userTopic).State = EntityState.Modified;
                db.SaveChanges();
            }

            if (userTopic.CourseTopicId == db.CourseTopics.Max(ct=>ct.CourseTopicId))
            {
                var markAsComplete = new EnrollCourseController().MarkAsComplete(userTopic.CourseTopic.CourseId, user);
            }
                return null;
        }        
    }
}
