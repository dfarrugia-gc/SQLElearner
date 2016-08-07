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
        public string QuizName { get; set; }
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
    }

    public class UserQuiz
    {
        [Key]
        public int UserQuizId { get; set;}
        public string Id { get; set; }
        [ForeignKey("Id")]
        public virtual ApplicationUser User { get; set; }
        public DateTime DateTimeCompleted { get; set; }
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

    public class QuestionType
    {
        [Key]
        public int QuestionTypeId { get; set; }
        public string QuestionTypeName { get; set; }
    }

    public class QuizContent
    {
        [Key]
        public int QuizContentId { get; set; }
        public int QuizId { get; set; }
        [ForeignKey("QuizId")]
        public virtual Quiz Quiz { get; set; }
        public int QuestionTypeId { get; set; }
        [ForeignKey("QuestionTypeId")]
        public virtual QuestionType QuestionType { get; set; }
        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public virtual Question Question{ get; set; }
        public int QuestionAnswerId { get; set; }
        [ForeignKey("QuestionAnswerId")]
        public virtual QuestionAnswer QuestionAnswer { get; set; }

        public QuizContent()
        {
            QuestionAnswerId = 0;
        }
    }

    public class QuizContentSpecifiedAnswer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuizContentSpecifiedAnswerId { get; set; }
        [Key, Column(Order = 1)]
        public int QuizContentId { get; set; }
        [ForeignKey("QuizContentId")]
        public virtual QuizContent QuizContent { get; set; }
        [Key, Column(Order = 2)]
        public int SpecifiedAnswerId { get; set; }
        [ForeignKey("SpecifiedAnswerId")]
        public virtual SpecifiedAnswer SpecifiedAnswer { get; set; }
    }

    public class SpecifiedAnswer
    {
        [Key]
        public int SpecifiedAnswerId { get; set; }
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }
    }

    public class UserQuizResult
    {
        [Key]
        public int UserQuizResultId { get; set; }
        public string Id { get; set; }
        [ForeignKey("Id")]
        public virtual ApplicationUser User { get; set; }
        public int UserQuizId { get; set; }
        [ForeignKey("UserQuizId")]
        public virtual UserQuiz UserQuiz { get; set; }
        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public virtual QuizContent QuizContentQuestion { get; set; }
        public int SpecifiedAnswerId { get; set; }
        [ForeignKey("SpecifiedAnswerId")]
        public virtual SpecifiedAnswer ResponseSpecifiedAnswer { get; set; }
    }

}