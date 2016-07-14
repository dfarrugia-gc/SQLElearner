using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace SQLElearner.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class Course
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
    }

    public class CourseDbContext :  DbContext
    {
        public CourseDbContext()
            : base("DefaultConnection")
        {
        }
        
        public DbSet<Course> Courses { get; set; }
    }
}