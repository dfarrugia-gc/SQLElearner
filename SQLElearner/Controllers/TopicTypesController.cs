using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SQLElearner.Models;

namespace SQLElearner.Controllers
{
    public class TopicTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TopicTypes
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Index()
        {
            return View(await db.TopicTypes.ToListAsync());
        }

        // GET: TopicTypes/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TopicType topicType = await db.TopicTypes.FindAsync(id);
            if (topicType == null)
            {
                return HttpNotFound();
            }
            return View(topicType);
        }

        // GET: TopicTypes/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: TopicTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create([Bind(Include = "TopicTypeId,TopicTypeName")] TopicType topicType)
        {
            if (ModelState.IsValid)
            {
                db.TopicTypes.Add(topicType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(topicType);
        }

        // GET: TopicTypes/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TopicType topicType = await db.TopicTypes.FindAsync(id);
            if (topicType == null)
            {
                return HttpNotFound();
            }
            return View(topicType);
        }

        // POST: TopicTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit([Bind(Include = "TopicTypeId,TopicTypeName")] TopicType topicType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(topicType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(topicType);
        }

        // GET: TopicTypes/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TopicType topicType = await db.TopicTypes.FindAsync(id);
            if (topicType == null)
            {
                return HttpNotFound();
            }
            return View(topicType);
        }

        // POST: TopicTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TopicType topicType = await db.TopicTypes.FindAsync(id);
            db.TopicTypes.Remove(topicType);
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
