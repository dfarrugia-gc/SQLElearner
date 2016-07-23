﻿using Elearner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SQLElearner.Controllers
{
    public class CourseTopicSectionViewerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: CourseTopicSectionViewer
        public ActionResult Index(int? id)
        {
            List<object> myModel = new List<object>();
            myModel.Add(db.UserTopics.Where(ut => ut.CourseTopicId == id).ToList());
            myModel.Add(db.CourseTopicSections.Where(cts => cts.CourseTopicId == id).OrderBy(cts =>cts.Order).ToList());
            
            return View(myModel);
        }

        // GET: CourseTopicSectionViewer/Details/5
        public async System.Threading.Tasks.Task<ActionResult> Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseTopicSection courseTopicSections = await db.CourseTopicSections.FindAsync(id);
            if (courseTopicSections == null)
            {
                return HttpNotFound();
            }
            return View(courseTopicSections);
        }

        // GET: CourseTopicSectionViewer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CourseTopicSectionViewer/Create
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

        // GET: CourseTopicSectionViewer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CourseTopicSectionViewer/Edit/5
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

        // GET: CourseTopicSectionViewer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CourseTopicSectionViewer/Delete/5
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
