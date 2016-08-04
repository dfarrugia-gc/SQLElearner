using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Elearner.Models;

namespace Elearner.Models
{
    public class Quiz
    {
        [Key]
        public int QuizId { get; set; }
        public string QuizName { get; set;}
    }

    public class UserQuiz
    {
        [Key]
        public int UserQuizId { get; set;}
        public string Id { get; set; }
        [ForeignKey("Id")]
        public virtual ApplicationUser User { get; set; }
    }
  
    public class Question
    {
        [Key]
        public int QuestionId { get; set;}
        public string QuestionText { get; set;}
        public int QuestionOder { get; set;}
    }

    public class QuestionAnswer
    {
        [Key]
        public int QuestionAnswerId { get; set; }
        public string QuestionAnswerText { get; set; }
    }

    public class QuizContent
    {
        [Key]
        public int QuizContentId { get; set; }
        public int QuizId { get; set; }
        [ForeignKey("QuizId")]
        public virtual Quiz Quiz { get; set; }
        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public virtual Question Question{ get; set; }
        public int QuestionAnswerId { get; set; }
        [ForeignKey("QuestionAnswerId")]
        public virtual QuestionAnswer QuestionAnswer { get; set; }
    }

}