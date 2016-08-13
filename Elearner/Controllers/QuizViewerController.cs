using Elearner.Models;
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
        public ActionResult Index(int? id, int? page, int? quizNo)
        {
            var user = User.Identity.GetUserId();
            //var quizNumber = from q in db.UserQuizs
            //             where q.Id == user &&
            //             q.QuizId == id &&
            //             q.QuizNo == db.UserQuizs.Max(m => m.QuizNo)
            //             select q.QuizNo;

            List < object > myModel = new List<object>();
            var quiz = db.Quizs.SingleOrDefault(q => q.QuizId == id).QuizId;
            var quizContents = db.QuizContents.Where(qc => qc.QuizId == id).ToList();            
            var quizContentSpecifiedAnswers = db.QuizContentSpecifiedAnswers.Where(x=>x.QuizContent.QuizId == id).ToList();
            var userQuizes = db.UserQuizs.Where(uq=>uq.QuizId == id && uq.QuizNo == quizNo && uq.Id == user).ToList();

            var userQuizResults = db.UserQuizResults.Where(uqr => uqr.UserQuiz.QuizId == id && uqr.Id.Equals(user) && uqr.UserQuiz.QuizNo == quizNo).ToList();

            var currentquestion = userQuizResults.Find(f => f.QuestionId == userQuizResults.Max(m => m.QuestionId));

             if (currentquestion == null)
            {
                var firstquestion = db.QuizContents.Min(x=>x.Question.QuestionOder);
                page = firstquestion;
            }else
            {
                var nextQuestion = db.Questions.Where(q => q.QuestionOder == currentquestion.Question.QuestionOder).SingleOrDefault();
            }
            

            var pageNumber = (page ?? currentquestion.Question.QuestionOder);
            var questionPages = quizContents.ToPagedList(pageNumber, 1);

            if (userQuizResults.Count() == 0)
            {
                try
                {
                    myModel.Add(questionPages);
                    myModel.Add(quizContents);
                    myModel.Add(userQuizResults);
                    myModel.Add(quizContentSpecifiedAnswers);
                    myModel.Add(userQuizes);

                    return View(myModel);
                }
                catch
                {
                    return new HttpNotFoundResult("No Quiz Setup fo this Course");
                }
            }
            //else if(nextQuestion == null)
            //{
            //    try
            //    {
            //        myModel.Add(questionPages);
            //        myModel.Add(quizContents);
            //        myModel.Add(userQuizResults);
            //        myModel.Add(quizContentSpecifiedAnswers);
            //        myModel.Add(userQuizes);

            //        return View(myModel);
            //    }
            //    catch
            //    {
            //        return new HttpNotFoundResult("Content Not Found");
            //    }
            //}
            else
            {
                try
                {
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
