using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elearner.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseInformation { get; set; }
    }

    public class Topic
    {
        [Key]
        public int TopicId { get; set; }
        public int TopicTypeId { get; set; }
        [ForeignKey("TopicTypeId")]
        public virtual TopicType TopicType{ get; set; }
        public string TopicName { get; set; }
        public string TopicInformation { get; set; }
    }

    public class TopicType
    {
        [Key]
        public int TopicTypeId { get; set; }
        public string TopicTypeName { get; set; }
    }

    public class CourseTopic
    {
        [Key]
        public int CourseTopicId { get; set; }
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
        public int TopicId { get; set; }
        [ForeignKey("TopicId")]
        public virtual Topic Topic { get; set; }
        public int TopicOrder { get; set; }
    }

    public class UserCourse
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserCourseId { get; set; }
        [Key, Column(Order = 1)]
        public string Id { get; set; }
        [ForeignKey("Id")]
        public virtual ApplicationUser User { get; set; }
        [Key, Column(Order = 2)]
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
        public bool Completed { get; set; }
        public int Grade { get; set; }
    }

    public class UserTopic
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserTopicId { get; set; }
        [Key, Column(Order = 1)]
        public string Id { get; set; }
        [ForeignKey("Id")]
        public virtual ApplicationUser User { get; set; }
        [Key, Column(Order = 2)]
        public int CourseTopicId { get; set; }
        [ForeignKey("CourseTopicId")]
        public virtual CourseTopic CourseTopic { get; set; }
        public bool Completed { get; set; }
    }

    public class CourseTopicSection
    {
        [Key]
        public int CourseTopicSectionId { get; set; }
        public int CourseTopicId { get; set; }
        [ForeignKey("CourseTopicId")]
        public virtual CourseTopic CourseTopic { get; set; }
        public string CourseTopicSectionName { get; set; }
        [System.Web.Mvc.AllowHtml]
        public string TopicSectionText { get; set; }
        public string TopicSectionInstruction { get; set; }
        public int Order { get; set; }
    }

    //public class CourseDbContext :  DbContext
    //{
    //    public CourseDbContext()
    //        : base("DefaultConnection")
    //    {
    //    }

    //    public DbSet<Course> Courses { get; set; }
    //    public DbSet<Topic> Topics { get; set; }
    //    public DbSet<TopicType> TopicTypes { get; set; }
    //    public DbSet<CourseTopic> CourseTopics { get; set; }
    //    public DbSet<UserCourseTopic> UserCourseTopics { get; set; }

    //    public static CourseDbContext Create()
    //    {
    //        return new CourseDbContext();
    //    }
    //}
}