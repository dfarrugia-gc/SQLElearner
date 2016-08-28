﻿using Elearner.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using Elearner.Controllers;
using System.Web;
using Elearner;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Data;

namespace SQLElearner.Controllers
{
    public class CourseTopicSectionViewerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        
        // GET: CourseTopicSectionViewer
        public ActionResult Index(int? id, int? page, string user_sql_syntax)
        {
            var user = User.Identity.GetUserId();

            List<object> myModel = new List<object>();
            var totalCourseTopicSections = db.CourseTopicSections.ToList();
            var courseTopicSections = db.CourseTopicSections.Where(cts => cts.CourseTopicId == id).OrderBy(cts => cts.Order).ToList();
            var userTopicSections = db.UserTopicSections.Where(uts => uts.CourseTopicSection.CourseTopicId == id && uts.Id.Equals(user)).ToList();
            var courseTopics = db.CourseTopics.Where(ct => ct.CourseTopicId == id).OrderBy(ct => ct.TopicOrder).ToList();

            var currentSection = userTopicSections.Find(f => f.CourseTopicSectionId == userTopicSections.Max(m => m.CourseTopicSectionId));
            try
            {

                var pageNumber = (page ?? currentSection.CourseTopicSection.Order);
                var courseTopicSectionsPages = courseTopicSections.ToPagedList(pageNumber, 1);

                if (user_sql_syntax == null)
                {
                    myModel.Add(courseTopicSectionsPages);
                    myModel.Add(userTopicSections);
                    myModel.Add(courseTopics);
                    myModel.Add(totalCourseTopicSections);
                    myModel.Add(null);
                    myModel.Add(user_sql_syntax);
                }
                else
                {
                    DataTable resultDataTable = (DataTable)Session["resultList"];

                    myModel.Add(courseTopicSectionsPages);
                    myModel.Add(userTopicSections);
                    myModel.Add(courseTopics);
                    myModel.Add(totalCourseTopicSections);
                    myModel.Add(resultDataTable);
                    myModel.Add(user_sql_syntax);                    
                }
                return View(myModel);
            }
            catch
            {
                return new HttpNotFoundResult("Content Not Found");
            }
        }

        [HttpPost]
        public ActionResult SQLQuery(int? id, int? page, string user_sql_query)
        {
            SQLEL_WorkingDatabaseEntities workingdb = new SQLEL_WorkingDatabaseEntities();
            DataTable table = new DataTable();

            using (SqlConnection connection = new SqlConnection(workingdb.Database.Connection.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(user_sql_query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                adapter.Fill(table);
            }
            
            DataTable cloneDT = new DataTable();
            cloneDT = table.Copy();

            Session["resultList"] = cloneDT;
            
            return RedirectToAction("Index","CourseTopicSectionViewer",new { id = id , page = page, user_sql_syntax = user_sql_query });
        }
    }
}
