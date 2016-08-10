using Microsoft.AspNet.Identity;
using Elearner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Elearner.Controllers
{
    public class EnrollQuizController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public object Users { get; private set; }

        // GET: EnrollQuiz
        public ActionResult Index()
        {
            return View();
        }

        // GET: EnrollQuiz/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EnrollQuiz/Create
        public ActionResult Create(int? id, string user)
        {
            if(string.IsNullOrEmpty(user))
            {
                return RedirectToAction("Details", "Quizs");
            }
            else
            {
                var quiz = db.Quizs.SingleOrDefault(q => q.CourseId == id);
                
                var userQuiz = new UserQuiz()
                {
                    Id = user,
                    DateTimeCompleted = DateTime.Parse("1900-01-01"),
                    QuizId = quiz.QuizId
                };

                bool userQuizExists = db.UserQuizs.Any(x => x.QuizId == quiz.QuizId
                & x.Id.Equals(user));
                
                if (!userQuizExists)
                {
                    db.UserQuizs.Add(userQuiz);
                    db.SaveChanges();
                }
                return RedirectToAction("Index", "QuizViewer", new { id = userQuiz.QuizId});

            }
            
        }

        public ActionResult MarkAsComplete(int? id, string user)
        {
            var userCourse = db.UserCourses.SingleOrDefault(x => x.CourseId == id && x.Id.Equals(user));
            //var courseTopicSection = db.CourseTopicSections.Where(x => x.CourseTopicId == userTopicSection.CourseTopicSection.CourseTopicId);

            if (ModelState.IsValid)
            {
                userCourse.Completed = true;

                db.Entry(userCourse).State = EntityState.Modified;
                db.SaveChanges();
            }
            return null;
        }

        // POST: EnrollQuiz/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
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

        // GET: EnrollQuiz/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EnrollQuiz/Edit/5
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

        // GET: EnrollQuiz/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EnrollQuiz/Delete/5
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
