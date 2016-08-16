﻿using System;
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
    public class SpecifiedAnswersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SpecifiedAnswers
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Index()
        {
            return View(await db.SpecifiedAnswers.ToListAsync());
        }

        // GET: SpecifiedAnswers/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpecifiedAnswer specifiedAnswer = await db.SpecifiedAnswers.FindAsync(id);
            if (specifiedAnswer == null)
            {
                return HttpNotFound();
            }
            return View(specifiedAnswer);
        }

        // GET: SpecifiedAnswers/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: SpecifiedAnswers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SpecifiedAnswerId,AnswerText,IsCorrect")] SpecifiedAnswer specifiedAnswer)
        {
            if (ModelState.IsValid)
            {
                db.SpecifiedAnswers.Add(specifiedAnswer);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(specifiedAnswer);
        }

        // GET: SpecifiedAnswers/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpecifiedAnswer specifiedAnswer = await db.SpecifiedAnswers.FindAsync(id);
            if (specifiedAnswer == null)
            {
                return HttpNotFound();
            }
            return View(specifiedAnswer);
        }

        // POST: SpecifiedAnswers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SpecifiedAnswerId,AnswerText,IsCorrect")] SpecifiedAnswer specifiedAnswer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(specifiedAnswer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(specifiedAnswer);
        }

        // GET: SpecifiedAnswers/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpecifiedAnswer specifiedAnswer = await db.SpecifiedAnswers.FindAsync(id);
            if (specifiedAnswer == null)
            {
                return HttpNotFound();
            }
            return View(specifiedAnswer);
        }

        // POST: SpecifiedAnswers/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SpecifiedAnswer specifiedAnswer = await db.SpecifiedAnswers.FindAsync(id);
            db.SpecifiedAnswers.Remove(specifiedAnswer);
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
