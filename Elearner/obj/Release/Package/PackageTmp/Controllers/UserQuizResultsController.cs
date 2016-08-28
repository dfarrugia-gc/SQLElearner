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
    public class UserQuizResultsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserQuizResults
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Index()
        {
            var userQuizResult = db.UserQuizResults.Include(u => u.Question).Include(u => u.ResponseSpecifiedAnswer).Include(u => u.User).Include(u => u.UserQuiz);
            return View(await userQuizResult.ToListAsync());
        }

        // GET: UserQuizResults/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserQuizResult userQuizResult = await db.UserQuizResults.FindAsync(id);
            if (userQuizResult == null)
            {
                return HttpNotFound();
            }
            return View(userQuizResult);
        }

        // GET: UserQuizResults/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.QuestionId = new SelectList(db.QuizContents, "QuizContentId", "QuizContentId");
            ViewBag.SpecifiedAnswerId = new SelectList(db.SpecifiedAnswers, "SpecifiedAnswerId", "AnswerText");
            ViewBag.Id = new SelectList(db.Users, "Id", "FirstName");
            ViewBag.UserQuizId = new SelectList(db.UserQuizs, "UserQuizId", "Id");
            return View();
        }

        // POST: UserQuizResults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UserQuizResultId,Id,UserQuizId,QuestionId,SpecifiedAnswerId")] UserQuizResult userQuizResult)
        {
            if (ModelState.IsValid)
            {
                db.UserQuizResults.Add(userQuizResult);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.QuestionId = new SelectList(db.QuizContents, "QuizContentId", "QuizContentId", userQuizResult.QuestionId);
            ViewBag.SpecifiedAnswerId = new SelectList(db.SpecifiedAnswers, "SpecifiedAnswerId", "AnswerText", userQuizResult.SpecifiedAnswerId);
            ViewBag.Id = new SelectList(db.ApplicationUsers, "Id", "FirstName", userQuizResult.Id);
            ViewBag.UserQuizId = new SelectList(db.UserQuizs, "UserQuizId", "Id", userQuizResult.UserQuizId);
            return View(userQuizResult);
        }

        // GET: UserQuizResults/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserQuizResult userQuizResult = await db.UserQuizResults.FindAsync(id);
            if (userQuizResult == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionId = new SelectList(db.QuizContents, "QuizContentId", "QuizContentId", userQuizResult.QuestionId);
            ViewBag.SpecifiedAnswerId = new SelectList(db.SpecifiedAnswers, "SpecifiedAnswerId", "AnswerText", userQuizResult.SpecifiedAnswerId);
            ViewBag.Id = new SelectList(db.ApplicationUsers, "Id", "FirstName", userQuizResult.Id);
            ViewBag.UserQuizId = new SelectList(db.UserQuizs, "UserQuizId", "Id", userQuizResult.UserQuizId);
            return View(userQuizResult);
        }

        // POST: UserQuizResults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UserQuizResultId,Id,UserQuizId,QuestionId,SpecifiedAnswerId")] UserQuizResult userQuizResult)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userQuizResult).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionId = new SelectList(db.QuizContents, "QuizContentId", "QuizContentId", userQuizResult.QuestionId);
            ViewBag.SpecifiedAnswerId = new SelectList(db.SpecifiedAnswers, "SpecifiedAnswerId", "AnswerText", userQuizResult.SpecifiedAnswerId);
            ViewBag.Id = new SelectList(db.ApplicationUsers, "Id", "FirstName", userQuizResult.Id);
            ViewBag.UserQuizId = new SelectList(db.UserQuizs, "UserQuizId", "Id", userQuizResult.UserQuizId);
            return View(userQuizResult);
        }

        // GET: UserQuizResults/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserQuizResult userQuizResult = await db.UserQuizResults.FindAsync(id);
            if (userQuizResult == null)
            {
                return HttpNotFound();
            }
            return View(userQuizResult);
        }

        // POST: UserQuizResults/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            UserQuizResult userQuizResult = await db.UserQuizResults.FindAsync(id);
            db.UserQuizResults.Remove(userQuizResult);
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
