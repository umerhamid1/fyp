using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentMonitoringSystem.Models;

namespace StudentMonitoringSystem.Areas.Admin.Controllers
{
    public class StudentAttendanceController : Controller
    {
        private StudentsMonitringSystemEntities db = new StudentsMonitringSystemEntities();

        // GET: Admin/StudentAttendance
        //public ActionResult Index()
        //{
        //    //var attendances = db.Attendances.Include(a => a.Class).Include(a => a.Section).Include(a => a.Student);
        //    //return View(attendances.ToList());
        //}

        // GET: Admin/StudentAttendance/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // GET: Admin/StudentAttendance/Create
        public ActionResult Create()
        {
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name");
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name");
            ViewBag.StudentID = new SelectList(db.Students, "ID", "StudentID");
            return View();
        }

        // POST: Admin/StudentAttendance/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ClassID,SectionID,StudentID,Date,Status,TeacherID")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Attendances.Add(attendance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", attendance.ClassID);
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name", attendance.SectionID);
         //   ViewBag.StudentID = new SelectList(db.Students, "ID", "StudentID", attendance.StudentID);
            return View(attendance);
        }

        // GET: Admin/StudentAttendance/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", attendance.ClassID);
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name", attendance.SectionID);
           // ViewBag.StudentID = new SelectList(db.Students, "ID", "StudentID", attendance.StudentID);
            return View(attendance);
        }

        // POST: Admin/StudentAttendance/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ClassID,SectionID,StudentID,Date,Status,TeacherID")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attendance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", attendance.ClassID);
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name", attendance.SectionID);
            //ViewBag.StudentID = new SelectList(db.Students, "ID", "StudentID", attendance.StudentID);
            return View(attendance);
        }

        // GET: Admin/StudentAttendance/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // POST: Admin/StudentAttendance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Attendance attendance = db.Attendances.Find(id);
            db.Attendances.Remove(attendance);
            db.SaveChanges();
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
