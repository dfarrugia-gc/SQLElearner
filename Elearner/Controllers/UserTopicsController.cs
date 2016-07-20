using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Elearner.Models;

namespace SQLElearner.Controllers
{
    public class UserTopicsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserTopics
        public async Task<ActionResult> Index()
        {
            var userTopics = db.UserTopics.Include(u => u.CourseTopic).Include(u => u.User);
            return View(await userTopics.ToListAsync());
        }

        // GET: UserTopics/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTopic userTopic = await db.UserTopics.FindAsync(id);
            if (userTopic == null)
            {
                return HttpNotFound();
            }
            return View(userTopic);
        }

        // GET: UserTopics/Create
        public ActionResult Create()
        {
            ViewBag.CourseTopicId = new SelectList(db.CourseTopics, "CourseTopicId", "CourseTopicId");
            ViewBag.Id = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: UserTopics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CourseTopicId,UserTopicId,Completed")] UserTopic userTopic)
        {
            if (ModelState.IsValid)
            {
                db.UserTopics.Add(userTopic);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CourseTopicId = new SelectList(db.CourseTopics, "CourseTopicId", "CourseTopicId", userTopic.CourseTopicId);
            ViewBag.Id = new SelectList(db.ApplicationUsers, "Id", "FirstName", userTopic.Id);
            return View(userTopic);
        }

        // GET: UserTopics/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTopic userTopic = await db.UserTopics.FindAsync(id);
            if (userTopic == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseTopicId = new SelectList(db.CourseTopics, "CourseTopicId", "CourseTopicId", userTopic.CourseTopicId);
            ViewBag.Id = new SelectList(db.ApplicationUsers, "Id", "FirstName", userTopic.Id);
            return View(userTopic);
        }

        // POST: UserTopics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CourseTopicId,UserTopicId,Completed")] UserTopic userTopic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userTopic).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CourseTopicId = new SelectList(db.CourseTopics, "CourseTopicId", "CourseTopicId", userTopic.CourseTopicId);
            ViewBag.Id = new SelectList(db.ApplicationUsers, "Id", "FirstName", userTopic.Id);
            return View(userTopic);
        }

        // GET: UserTopics/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTopic userTopic = await db.UserTopics.FindAsync(id);
            if (userTopic == null)
            {
                return HttpNotFound();
            }
            return View(userTopic);
        }

        // POST: UserTopics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            UserTopic userTopic = await db.UserTopics.FindAsync(id);
            db.UserTopics.Remove(userTopic);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
