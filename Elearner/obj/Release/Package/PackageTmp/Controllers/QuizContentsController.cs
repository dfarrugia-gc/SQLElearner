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
    public class QuizContentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: QuizContents
        public async Task<ActionResult> Index()
        {
            var quizContents = db.QuizContents.Include(q => q.Question).Include(q => q.QuestionAnswer).Include(q => q.QuestionType).Include(q => q.Quiz);
            return View(await quizContents.ToListAsync());
        }

        // GET: QuizContents/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizContent quizContent = await db.QuizContents.FindAsync(id);
            if (quizContent == null)
            {
                return HttpNotFound();
            }
            return View(quizContent);
        }

        // GET: QuizContents/Create
        public ActionResult Create()
        {
            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "QuestionText");
            ViewBag.QuestionAnswerId = new SelectList(db.QuestionAnswers, "QuestionAnswerId", "QuestionAnswerText");
            ViewBag.QuestionTypeId = new SelectList(db.QuestionTypes, "QuestionTypeId", "QuestionTypeName");
            ViewBag.QuizId = new SelectList(db.Quizs, "QuizId", "QuizName");
            return View();
        }

        // POST: QuizContents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "QuizContentId,QuizId,QuestionTypeId,QuestionId,QuestionAnswerId")] QuizContent quizContent)
        {
            if (ModelState.IsValid)
            {
                db.QuizContents.Add(quizContent);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "QuestionText", quizContent.QuestionId);
            ViewBag.QuestionAnswerId = new SelectList(db.QuestionAnswers, "QuestionAnswerId", "QuestionAnswerText", quizContent.QuestionAnswerId);
            ViewBag.QuestionTypeId = new SelectList(db.QuestionTypes, "QuestionTypeId", "QuestionTypeName", quizContent.QuestionTypeId);
            ViewBag.QuizId = new SelectList(db.Quizs, "QuizId", "QuizName", quizContent.QuizId);
            return View(quizContent);
        }

        // GET: QuizContents/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizContent quizContent = await db.QuizContents.FindAsync(id);
            if (quizContent == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "QuestionText", quizContent.QuestionId);
            ViewBag.QuestionAnswerId = new SelectList(db.QuestionAnswers, "QuestionAnswerId", "QuestionAnswerText", quizContent.QuestionAnswerId);
            ViewBag.QuestionTypeId = new SelectList(db.QuestionTypes, "QuestionTypeId", "QuestionTypeName", quizContent.QuestionTypeId);
            ViewBag.QuizId = new SelectList(db.Quizs, "QuizId", "QuizName", quizContent.QuizId);
            return View(quizContent);
        }

        // POST: QuizContents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "QuizContentId,QuizId,QuestionTypeId,QuestionId,QuestionAnswerId")] QuizContent quizContent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quizContent).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "QuestionText", quizContent.QuestionId);
            ViewBag.QuestionAnswerId = new SelectList(db.QuestionAnswers, "QuestionAnswerId", "QuestionAnswerText", quizContent.QuestionAnswerId);
            ViewBag.QuestionTypeId = new SelectList(db.QuestionTypes, "QuestionTypeId", "QuestionTypeName", quizContent.QuestionTypeId);
            ViewBag.QuizId = new SelectList(db.Quizs, "QuizId", "QuizName", quizContent.QuizId);
            return View(quizContent);
        }

        // GET: QuizContents/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizContent quizContent = await db.QuizContents.FindAsync(id);
            if (quizContent == null)
            {
                return HttpNotFound();
            }
            return View(quizContent);
        }

        // POST: QuizContents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            QuizContent quizContent = await db.QuizContents.FindAsync(id);
            db.QuizContents.Remove(quizContent);
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
