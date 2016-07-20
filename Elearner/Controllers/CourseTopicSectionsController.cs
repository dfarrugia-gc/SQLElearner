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
    public class CourseTopicSectionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CourseTopicSections
        public async Task<ActionResult> Index()
        {
            var courseTopicSections = db.CourseTopicSections.Include(c => c.CourseTopic);
            return View(await courseTopicSections.ToListAsync());
        }

        // GET: CourseTopicSections/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseTopicSection courseTopicSection = await db.CourseTopicSections.FindAsync(id);
            if (courseTopicSection == null)
            {
                return HttpNotFound();
            }
            return View(courseTopicSection);
        }

        // GET: CourseTopicSections/Create
        public ActionResult Create()
        {
            ViewBag.CourseTopicId = new SelectList(db.CourseTopics, "CourseTopicId", "CourseTopicId");
            return View();
        }

        // POST: CourseTopicSections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CourseTopicSectionId,CourseTopicId,CourseTopicSectionName,TopicSectionText,TopicSectionInstruction,Order")] CourseTopicSection courseTopicSection)
        {
            if (ModelState.IsValid)
            {
                db.CourseTopicSections.Add(courseTopicSection);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CourseTopicId = new SelectList(db.CourseTopics, "CourseTopicId", "CourseTopicId", courseTopicSection.CourseTopicId);
            return View(courseTopicSection);
        }

        // GET: CourseTopicSections/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseTopicSection courseTopicSection = await db.CourseTopicSections.FindAsync(id);
            if (courseTopicSection == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseTopicId = new SelectList(db.CourseTopics, "CourseTopicId", "CourseTopicId", courseTopicSection.CourseTopicId);
            return View(courseTopicSection);
        }

        // POST: CourseTopicSections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CourseTopicSectionId,CourseTopicId,CourseTopicSectionName,TopicSectionText,TopicSectionInstruction,Order")] CourseTopicSection courseTopicSection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseTopicSection).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CourseTopicId = new SelectList(db.CourseTopics, "CourseTopicId", "CourseTopicId", courseTopicSection.CourseTopicId);
            return View(courseTopicSection);
        }

        // GET: CourseTopicSections/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseTopicSection courseTopicSection = await db.CourseTopicSections.FindAsync(id);
            if (courseTopicSection == null)
            {
                return HttpNotFound();
            }
            return View(courseTopicSection);
        }

        // POST: CourseTopicSections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CourseTopicSection courseTopicSection = await db.CourseTopicSections.FindAsync(id);
            db.CourseTopicSections.Remove(courseTopicSection);
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
