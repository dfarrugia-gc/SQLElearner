using Elearner.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Elearner.Controllers
{
    public class CourseTopicViewerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CourseTopicViewer
        public ActionResult Index(int? id)
        {
            List<object> myModel = new List<object>();
            myModel.Add(db.Courses.Where(c => c.CourseId == id).ToList());
            myModel.Add(db.CourseTopics.Where(ct => ct.CourseId == id).ToList());

            return View(myModel);
        }

        // GET: CourseTopicViewer/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseTopic courseTopics = await db.CourseTopics.FindAsync(id);
            if (courseTopics == null)
            {
                return HttpNotFound();
            }
            return View(courseTopics);
        }

        // GET: CourseTopicViewer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CourseTopicViewer/Create
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

        // GET: CourseTopicViewer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CourseTopicViewer/Edit/5
        [HttpPost]
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

        // GET: CourseTopicViewer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CourseTopicViewer/Delete/5
        [HttpPost]
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
