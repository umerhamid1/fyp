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
    public class subjectsController : Controller
    {
        private StudentsMonitringSystemEntities db = new StudentsMonitringSystemEntities();

        // GET: Admin/subjects
        public ActionResult Index()
        {
            var subjects = db.subjects.Include(s => s.Campu).Include(s => s.Class);
            return View(subjects.ToList());
        }

        // GET: Admin/subjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subject subject = db.subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // GET: Admin/subjects/Create
        public ActionResult Create()
        {
            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name");
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name");
            ViewBag.TeacherID = new SelectList(db.Teachers, "ID", "Name");
            return View();
        }

        // POST: Admin/subjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SubjectName,ClassID,TeacherID,CampusID")] subject subject)
        {
            if (ModelState.IsValid)
            {
                db.subjects.Add(subject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name", subject.CampusID);
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", subject.ClassID);
          
            return View(subject);
        }

        // GET: Admin/subjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subject subject = db.subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name", subject.CampusID);
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", subject.ClassID);
         //   ViewBag.TeacherID = new SelectList(db.Teachers, "ID", "Name", subject.TeacherID);
            return View(subject);
        }

        // POST: Admin/subjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SubjectName,ClassID,TeacherID,CampusID")] subject subject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name", subject.CampusID);
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", subject.ClassID);
           // ViewBag.TeacherID = new SelectList(db.Teachers, "ID", "Name", subject.TeacherID);
            return View(subject);
        }

        // GET: Admin/subjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subject subject = db.subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // POST: Admin/subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            subject subject = db.subjects.Find(id);
            db.subjects.Remove(subject);
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


        public JsonResult GetClasses(string ID)
        {


            var data = (from c in db.AddClasses
                join cn in db.Classes on c.ClassID equals cn.ID
                where (c.CampusID == ID)

                select new
                {
                    cn.ID,
                    cn.Name

                }).Distinct().ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetTeacher(string ID)
        {
            var data = (from tb in db.Teachers
                where tb.CampusID == ID
                select new
                {
                    tb.ID,
                    tb.Name
                }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}
