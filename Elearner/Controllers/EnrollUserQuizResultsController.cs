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
        public ActionResult Create(int questionId, int specifiedAnswerId, string user, int userQuizId)
        {
            if(string.IsNullOrEmpty(user))
            {
                return RedirectToAction("Details", "Quizs");
            }
            else
            {
                
                var userQuizResults = new UserQuizResult()
                {
                    Id = user,
                    QuestionId = questionId,
                    UserQuizId = userQuizId,
                    SpecifiedAnswerId = specifiedAnswerId
                };

                db.UserQuizResult.Add(userQuizResults);
                db.SaveChanges();

                return RedirectToAction("Index", "QuizViewer", new { id = userQuizResults.UserQuiz.QuizId});
            }
        }
    }
}
