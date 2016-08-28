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
    public class QuizContentSpecifiedAnswersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: QuizContentSpecifiedAnswers
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Index()
        {
            var quizContentSpecifiedAnswers = db.QuizContentSpecifiedAnswers.Include(q => q.QuizContent).Include(q => q.SpecifiedAnswer);
            return View(await quizContentSpecifiedAnswers.ToListAsync());
        }

        // GET: QuizContentSpecifiedAnswers/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizContentSpecifiedAnswer quizContentSpecifiedAnswer = await db.QuizContentSpecifiedAnswers.FindAsync(id);
            if (quizContentSpecifiedAnswer == null)
            {
                return HttpNotFound();
            }
            return View(quizContentSpecifiedAnswer);
        }

        // GET: QuizContentSpecifiedAnswers/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.QuizContentId = new SelectList(db.QuizContents, "QuizContentId", "QuizContentId");
            ViewBag.SpecifiedAnswerId = new SelectList(db.SpecifiedAnswers, "SpecifiedAnswerId", "AnswerText");
            return View();
        }

        // POST: QuizContentSpecifiedAnswers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "QuizContentId,SpecifiedAnswerId,QuizContentSpecifiedAnswerId")] QuizContentSpecifiedAnswer quizContentSpecifiedAnswer)
        {
            if (ModelState.IsValid)
            {
                db.QuizContentSpecifiedAnswers.Add(quizContentSpecifiedAnswer);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.QuizContentId = new SelectList(db.QuizContents, "QuizContentId", "QuizContentId", quizContentSpecifiedAnswer.QuizContentId);
            ViewBag.SpecifiedAnswerId = new SelectList(db.SpecifiedAnswers, "SpecifiedAnswerId", "AnswerText", quizContentSpecifiedAnswer.SpecifiedAnswerId);
            return View(quizContentSpecifiedAnswer);
        }

        // GET: QuizContentSpecifiedAnswers/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizContentSpecifiedAnswer quizContentSpecifiedAnswer = await db.QuizContentSpecifiedAnswers.FindAsync(id);
            if (quizContentSpecifiedAnswer == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuizContentId = new SelectList(db.QuizContents, "QuizContentId", "QuizContentId", quizContentSpecifiedAnswer.QuizContentId);
            ViewBag.SpecifiedAnswerId = new SelectList(db.SpecifiedAnswers, "SpecifiedAnswerId", "AnswerText", quizContentSpecifiedAnswer.SpecifiedAnswerId);
            return View(quizContentSpecifiedAnswer);
        }

        // POST: QuizContentSpecifiedAnswers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "QuizContentId,SpecifiedAnswerId,QuizContentSpecifiedAnswerId")] QuizContentSpecifiedAnswer quizContentSpecifiedAnswer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quizContentSpecifiedAnswer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.QuizContentId = new SelectList(db.QuizContents, "QuizContentId", "QuizContentId", quizContentSpecifiedAnswer.QuizContentId);
            ViewBag.SpecifiedAnswerId = new SelectList(db.SpecifiedAnswers, "SpecifiedAnswerId", "AnswerText", quizContentSpecifiedAnswer.SpecifiedAnswerId);
            return View(quizContentSpecifiedAnswer);
        }

        // GET: QuizContentSpecifiedAnswers/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizContentSpecifiedAnswer quizContentSpecifiedAnswer = await db.QuizContentSpecifiedAnswers.FindAsync(id);
            if (quizContentSpecifiedAnswer == null)
            {
                return HttpNotFound();
            }
            return View(quizContentSpecifiedAnswer);
        }

        // POST: QuizContentSpecifiedAnswers/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            QuizContentSpecifiedAnswer quizContentSpecifiedAnswer = await db.QuizContentSpecifiedAnswers.FindAsync(id);
            db.QuizContentSpecifiedAnswers.Remove(quizContentSpecifiedAnswer);
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
