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
    public class EnrollUserQuizResultsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public object Users { get; private set; }


        // GET: EnrollUserQuizResults/Create
        public ActionResult Create(int questionId, int specifiedAnswerId, string user, int userQuizId, int quizId, int quizNo)
        {
            if(string.IsNullOrEmpty(user))
            {
                return RedirectToAction("Details", "Quizs");
            }
            else
            {
                
                var userquiz = db.UserQuizs.FirstOrDefault(uq=>uq.UserQuizId == userQuizId);
                var quizContents = db.QuizContents.Where(qc => qc.QuizId == quizId).ToList();
                var usrQzResult = db.UserQuizResults.FirstOrDefault(x => x.UserQuizId == userquiz.UserQuizId & x.QuestionId == questionId & x.Id.Equals(user));
                
                var userQuizResults = new UserQuizResult()
                {
                    Id = user,
                    QuestionId = questionId,
                    UserQuizId = userquiz.UserQuizId,
                    SpecifiedAnswerId = specifiedAnswerId
                };

                bool userQuizReaultExists = db.UserQuizResults.Any(x => x.UserQuizId == userquiz.UserQuizId
                &x.QuestionId == questionId
                & x.Id.Equals(user));

                if (!userQuizReaultExists)
                {
                    db.UserQuizResults.Add(userQuizResults);
                    db.SaveChanges();
                }else
                {
                    UserQuizResult uqr = db.UserQuizResults.FirstOrDefault(x => x.UserQuizResultId == usrQzResult.UserQuizResultId);
                    uqr.SpecifiedAnswerId = specifiedAnswerId;
                    db.SaveChanges();
                }
                return RedirectToAction("Index", "QuizViewer", new { id = quizId, quizNo = quizNo });
            }
        }

        public ActionResult MarkAsComplete(int? id, string user)
        {
            var userquiz = db.UserQuizs.FirstOrDefault(uq => uq.UserQuizId == id);
            //var courseTopicSection = db.CourseTopicSections.Where(x => x.CourseTopicId == userTopicSection.CourseTopicSection.CourseTopicId);

            if (ModelState.IsValid)
            {
                userquiz.DateTimeCompleted = DateTime.Now;

                db.Entry(userquiz).State = EntityState.Modified;
                db.SaveChanges();

                var markAsComplete = new EnrollCourseController().MarkAsComplete(userquiz.Quiz.CourseId, user);
            }
            return RedirectToAction("Index", "CourseTopicViewer", new { id = userquiz.Quiz.CourseId });
        }
    }
}
