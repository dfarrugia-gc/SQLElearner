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

namespace SQLElearner.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string CourseName { get; set; }
    }

    public class Topic
    {
        [Key]
        public int TopicId { get; set; }
        public int TopicTypeId { get; set; }
        [ForeignKey("TopicTypeId")] public virtual TopicType TopicType{ get; set; }
        public string TopicName { get; set; }
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
    }

    public class UserCourseTopic
    {
        [Key]
        public int UserCourseTopicId { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
        public int TopicId { get; set; }
        [ForeignKey("TopicId")]
        public virtual Topic Topic { get; set; }
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