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
    public class UserQuizsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserQuizs
        public async Task<ActionResult> Index()
        {
            var userQuizes = db.UserQuizes.Include(u => u.User);
            return View(await userQuizes.ToListAsync());
        }

        // GET: UserQuizs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserQuiz userQuiz = await db.UserQuizes.FindAsync(id);
            if (userQuiz == null)
            {
                return HttpNotFound();
            }
            return View(userQuiz);
        }

        // GET: UserQuizs/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.ApplicationUsers, "Id", "FirstName");
            return View();
        }

        // POST: UserQuizs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UserQuizId,Id")] UserQuiz userQuiz)
        {
            if (ModelState.IsValid)
            {
                db.UserQuizes.Add(userQuiz);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.ApplicationUsers, "Id", "FirstName", userQuiz.Id);
            return View(userQuiz);
        }

        // GET: UserQuizs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserQuiz userQuiz = await db.UserQuizes.FindAsync(id);
            if (userQuiz == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.ApplicationUsers, "Id", "FirstName", userQuiz.Id);
            return View(userQuiz);
        }

        // POST: UserQuizs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UserQuizId,Id")] UserQuiz userQuiz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userQuiz).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.ApplicationUsers, "Id", "FirstName", userQuiz.Id);
            return View(userQuiz);
        }

        // GET: UserQuizs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserQuiz userQuiz = await db.UserQuizes.FindAsync(id);
            if (userQuiz == null)
            {
                return HttpNotFound();
            }
            return View(userQuiz);
        }

        // POST: UserQuizs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            UserQuiz userQuiz = await db.UserQuizes.FindAsync(id);
            db.UserQuizes.Remove(userQuiz);
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
