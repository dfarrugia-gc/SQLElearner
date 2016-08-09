﻿using Elearner.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using Elearner.Controllers;
using System.Web;

namespace SQLElearner.Controllers
{
    public class QuizViewerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: QuizViewer
        public ActionResult Index(int? id, int? page)
        {
            var user = User.Identity.GetUserId();

            List<object> myModel = new List<object>();
            var quiz = db.Quizs.Where(q => q.QuizId == id).ToList();
            var quizContents = db.QuizContents.Where(qc => qc.QuizId == id).ToList();
            var userQuizResults = db.UserQuizResult.Where(uqr => uqr.UserQuiz.QuizId == id && uqr.Id.Equals(user)).ToList();
            var quizContentSpecifiedAnswers = db.QuizContentSpecifiedAnswers.Where(x=>x.QuizContent.QuizId == id).ToList();
            var userQuizes = db.UserQuizs.Where(uq=>uq.QuizId == id).ToList();

            if (userQuizResults.Count() == 0)
            {
                try
                {

                    var pageNumber = (page ?? 1);
                    var questionPages = quizContents.ToPagedList(pageNumber, 1);

                    myModel.Add(questionPages);
                    myModel.Add(quizContents);
                    myModel.Add(userQuizResults);
                    myModel.Add(quizContentSpecifiedAnswers);
                    myModel.Add(userQuizes);

                    return View(myModel);
                }
                catch
                {
                    return new HttpNotFoundResult("Content Not Found");
                }
            }
            else
            {
                var currentquestion = quizContents.First(f => f.QuestionId == userQuizResults.Max(m => m.QuestionId));

                try
                {

                    var pageNumber = (page ?? currentquestion.Question.QuestionOder);
                    var questionPages = quizContents.ToPagedList(pageNumber, 1);

                    myModel.Add(questionPages);
                    myModel.Add(quizContents);
                    myModel.Add(userQuizResults);

                    return View(myModel);
                }
                catch
                {
                    return new HttpNotFoundResult("Content Not Found");
                }
            }            
        }

        // GET: QuizViewer/Details/5
        [Authorize(Roles = "Admin")]
        public async System.Threading.Tasks.Task<ActionResult> Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseTopicSection courseTopicSections = await db.CourseTopicSections.FindAsync(id);
            if (courseTopicSections == null)
            {
                return HttpNotFound();
            }
            return View(courseTopicSections);
        }

        // GET: QuizViewer/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuizViewer/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: QuizViewer/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: QuizViewer/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: QuizViewer/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: QuizViewer/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}