﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Elearner.Models;
using Microsoft.AspNet.Identity;

namespace Elearner.Controllers
{
    public class UserCourseTopicsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserCourseTopics
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Index()
        {
            var userCourseTopics = db.UserCourseTopics.Include(u => u.Course).Include(u => u.Topic).Include(u => u.User);
            return View(await userCourseTopics.ToListAsync());
        }

        // GET: UserCourseTopics/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserCourseTopic userCourseTopic = await db.UserCourseTopics.FindAsync(id);
            if (userCourseTopic == null)
            {
                return HttpNotFound();
            }
            return View(userCourseTopic);
        }

        // GET: UserCourseTopics/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName");
            ViewBag.TopicId = new SelectList(db.Topics, "TopicId", "TopicName");
            ViewBag.Id = new SelectList(db.Users, "Id", "FirstName");

            return View();
        }

        // POST: UserCourseTopics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UserCourseTopicId,Id,CourseId,TopicId")] UserCourseTopic userCourseTopic)
        {
            if (ModelState.IsValid)
            {
                db.UserCourseTopics.Add(userCourseTopic);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName", userCourseTopic.CourseId);
            ViewBag.TopicId = new SelectList(db.Topics, "TopicId", "TopicName", userCourseTopic.TopicId);
            ViewBag.Id = new SelectList(db.ApplicationUsers, "Id", "FirstName", userCourseTopic.Id);
            return View(userCourseTopic);
        }

        // GET: UserCourseTopics/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserCourseTopic userCourseTopic = await db.UserCourseTopics.FindAsync(id);
            if (userCourseTopic == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName", userCourseTopic.CourseId);
            ViewBag.TopicId = new SelectList(db.Topics, "TopicId", "TopicName", userCourseTopic.TopicId);
            ViewBag.Id = new SelectList(db.ApplicationUsers, "Id", "FirstName", userCourseTopic.Id);
            return View(userCourseTopic);
        }

        // POST: UserCourseTopics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit([Bind(Include = "UserCourseTopicId,Id,CourseId,TopicId")] UserCourseTopic userCourseTopic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userCourseTopic).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName", userCourseTopic.CourseId);
            ViewBag.TopicId = new SelectList(db.Topics, "TopicId", "TopicName", userCourseTopic.TopicId);
            ViewBag.Id = new SelectList(db.ApplicationUsers, "Id", "FirstName", userCourseTopic.Id);
            return View(userCourseTopic);
        }

        // GET: UserCourseTopics/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserCourseTopic userCourseTopic = await db.UserCourseTopics.FindAsync(id);
            if (userCourseTopic == null)
            {
                return HttpNotFound();
            }
            return View(userCourseTopic);
        }

        // POST: UserCourseTopics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            UserCourseTopic userCourseTopic = await db.UserCourseTopics.FindAsync(id);
            db.UserCourseTopics.Remove(userCourseTopic);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
