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

namespace Elearner.Controllers
{
    public class CourseTopicsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CourseTopics
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Index()
        {
            var courseTopics = db.CourseTopics.Include(c => c.Course).Include(c => c.Topic);
            return View(await courseTopics.ToListAsync());
        }

        // GET: CourseTopics/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseTopic courseTopic = await db.CourseTopics.FindAsync(id);
            if (courseTopic == null)
            {
                return HttpNotFound();
            }
            return View(courseTopic);
        }

        // GET: CourseTopics/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName");
            ViewBag.TopicId = new SelectList(db.Topics, "TopicId", "TopicName");
            return View();
        }

        // POST: CourseTopics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create([Bind(Include = "CourseTopicId,CourseId,TopicId,TopicOrder")] CourseTopic courseTopic)
        {
            if (ModelState.IsValid)
            {
                db.CourseTopics.Add(courseTopic);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName", courseTopic.CourseId);
            ViewBag.TopicId = new SelectList(db.Topics, "TopicId", "TopicName", courseTopic.TopicId);
            return View(courseTopic);
        }

        // GET: CourseTopics/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseTopic courseTopic = await db.CourseTopics.FindAsync(id);
            if (courseTopic == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName", courseTopic.CourseId);
            ViewBag.TopicId = new SelectList(db.Topics, "TopicId", "TopicName", courseTopic.TopicId);
            return View(courseTopic);
        }

        // POST: CourseTopics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit([Bind(Include = "CourseTopicId,CourseId,TopicId,TopicOrder")] CourseTopic courseTopic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseTopic).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName", courseTopic.CourseId);
            ViewBag.TopicId = new SelectList(db.Topics, "TopicId", "TopicName", courseTopic.TopicId);
            return View(courseTopic);
        }

        // GET: CourseTopics/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseTopic courseTopic = await db.CourseTopics.FindAsync(id);
            if (courseTopic == null)
            {
                return HttpNotFound();
            }
            return View(courseTopic);
        }

        // POST: CourseTopics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CourseTopic courseTopic = await db.CourseTopics.FindAsync(id);
            db.CourseTopics.Remove(courseTopic);
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
