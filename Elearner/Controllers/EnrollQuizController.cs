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
                
        // GET: EnrollQuiz/Create
        public ActionResult Create(int? id, string user, bool retake, int? quizNo)
        {
            if(string.IsNullOrEmpty(user))
            {
                return RedirectToAction("Details", "Quizs");
            }
            else
            {
                var existingUserQuiz = db.UserQuizs.Where(q => q.Quiz.CourseId == id && q.Id == user).ToList();
                var quiz = db.Quizs.SingleOrDefault(q => q.CourseId == id);

                if (retake == true)
                {
                    var userQuiz = db.UserQuizs.Where(q => q.Quiz.CourseId == id);
                    var getLastUserQuizNo = existingUserQuiz.Max(q => q.QuizNo);
                    int newQuizNo = getLastUserQuizNo + 1;

                    var retakeUserQuiz = new UserQuiz()
                    {
                        Id = user,
                        DateTimeCompleted = DateTime.Parse("1900-01-01"),
                        QuizId = quiz.QuizId,
                        QuizNo = newQuizNo
                    };

                    db.UserQuizs.Add(retakeUserQuiz);
                    db.SaveChanges();

                    return RedirectToAction("Index", "QuizViewer", new { id = retakeUserQuiz.QuizId, quizNo = newQuizNo });
                }
                else
                {
                    bool userQuizExists = db.UserQuizs.Any(x => x.QuizId == quiz.QuizId
                    && x.QuizNo == quizNo
                    && x.Id.Equals(user));

                    if (!userQuizExists)
                    {
                        var userQuiz = new UserQuiz()
                        {
                            Id = user,
                            DateTimeCompleted = DateTime.Parse("1900-01-01"),
                            QuizId = quiz.QuizId,
                            QuizNo = 1
                        };

                        db.UserQuizs.Add(userQuiz);
                        db.SaveChanges();

                        return RedirectToAction("Index", "QuizViewer", new { id = userQuiz.QuizId, userQuiz.QuizNo });
                    }
                }              
            }
            return RedirectToAction("Index", "QuizViewer", new { id = id, quizNo = quizNo });
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
    }
}
