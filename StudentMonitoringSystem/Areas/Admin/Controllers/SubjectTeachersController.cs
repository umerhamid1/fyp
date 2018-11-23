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
    public class SubjectTeachersController : Controller
    {
        private StudentsMonitringSystemEntities db = new StudentsMonitringSystemEntities();

        // GET: Admin/SubjectTeachers
        public ActionResult Index()
        {
            var subjectTeachers = db.SubjectTeachers.Include(s => s.Campu).Include(s => s.Section).Include(s => s.Teacher).Include(s => s.Class).Include(s => s.subject);
            return View(subjectTeachers.ToList());
        }

        // GET: Admin/SubjectTeachers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubjectTeacher subjectTeacher = db.SubjectTeachers.Find(id);
            if (subjectTeacher == null)
            {
                return HttpNotFound();
            }
            return View(subjectTeacher);
        }

        // GET: Admin/SubjectTeachers/Create
        public ActionResult Create()
        {
            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name");
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name");
            ViewBag.TeacherID = new SelectList(db.Teachers, "ID", "Name");
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name");
            ViewBag.SubjectID = new SelectList(db.subjects, "ID", "SubjectName");
            return View();
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




        public JsonResult GetSubject(int ID)
        {

            var data = (from c in db.ExamsTimeTables
                join cn in db.subjects on c.ClassID equals cn.ClassID
                //   join cnn in db.Classes on cn.CampusID equals cnn.AddClasses
                //  where (cn.ID == ID)

                select new
                {
                    cn.ID,
                    cn.SubjectName

                }).Distinct().ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetSection(int ID)
        {

            var data = (from c in db.AddClasses
                join cn in db.Sections on c.SectionID equals cn.ID
                where (c.ClassID == ID)

                select new
                {
                    cn.ID,
                    cn.Name

                }).Distinct().ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        // POST: Admin/SubjectTeachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CampusID,TeacherID,SectionID,SubjectID,ClassID")] SubjectTeacher subjectTeacher)
        {
            if (ModelState.IsValid)
            {
                db.SubjectTeachers.Add(subjectTeacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name", subjectTeacher.CampusID);
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name", subjectTeacher.SectionID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "ID", "Name", subjectTeacher.TeacherID);
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", subjectTeacher.ClassID);
            ViewBag.SubjectID = new SelectList(db.subjects, "ID", "SubjectName", subjectTeacher.SubjectID);
            return View(subjectTeacher);
        }

        // GET: Admin/SubjectTeachers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubjectTeacher subjectTeacher = db.SubjectTeachers.Find(id);
            if (subjectTeacher == null)
            {
                return HttpNotFound();
            }
            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name", subjectTeacher.CampusID);
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name", subjectTeacher.SectionID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "ID", "Name", subjectTeacher.TeacherID);
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", subjectTeacher.ClassID);
            ViewBag.SubjectID = new SelectList(db.subjects, "ID", "SubjectName", subjectTeacher.SubjectID);
            return View(subjectTeacher);
        }

        // POST: Admin/SubjectTeachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CampusID,TeacherID,SectionID,SubjectID,ClassID")] SubjectTeacher subjectTeacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subjectTeacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name", subjectTeacher.CampusID);
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name", subjectTeacher.SectionID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "ID", "Name", subjectTeacher.TeacherID);
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", subjectTeacher.ClassID);
            ViewBag.SubjectID = new SelectList(db.subjects, "ID", "SubjectName", subjectTeacher.SubjectID);
            return View(subjectTeacher);
        }

        // GET: Admin/SubjectTeachers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubjectTeacher subjectTeacher = db.SubjectTeachers.Find(id);
            if (subjectTeacher == null)
            {
                return HttpNotFound();
            }
            return View(subjectTeacher);
        }

        // POST: Admin/SubjectTeachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubjectTeacher subjectTeacher = db.SubjectTeachers.Find(id);
            db.SubjectTeachers.Remove(subjectTeacher);
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
