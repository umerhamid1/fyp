using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentMonitoringSystem.Models;

namespace StudentMonitoringSystem.Areas.Teacher1.Controllers
{
    public class CampusStudentDetailsController : Controller
    {
        private StudentsMonitringSystemEntities db = new StudentsMonitringSystemEntities();

        // GET: Teacher1/CampusStudentDetails
        public ActionResult Index()
        {
            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name");
            var students = db.Students.Include(s => s.Campu).Include(s => s.Parent).Include(s => s.ClassAndSectionDetail).Include(s => s.Class).Include(s => s.Section);
            return View(students.ToList());
        }


       


        public JsonResult GetCampusStudent(string ID)
        {
            var data = (from tb in db.Students
                where tb.CampusID == ID
                select new
                {
                    tb.StudentID,
                    tb.FirstName,
                    tb.LastName,
                    //tb.Class.Name,
                    //tb.Parent.PhoneNo,
                    //tb.Section

                }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // GET: Teacher1/CampusStudentDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Teacher1/CampusStudentDetails/Create
        public ActionResult Create()
        {
            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name");
            ViewBag.ParentsID = new SelectList(db.Parents, "ID", "ParentsID");
            ViewBag.ClassID = new SelectList(db.ClassAndSectionDetails, "ID", "CampusID");
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name");
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name");
            return View();
        }

        // POST: Teacher1/CampusStudentDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,StudentID,FirstName,LastName,DOB,Gender,Address,Nationality,Password,PhoneNo,ParentsID,ClassID,SectionID,AdmissionYear,IsApproved,CampusID")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name", student.CampusID);
            ViewBag.ParentsID = new SelectList(db.Parents, "ID", "ParentsID", student.ParentsID);
            ViewBag.ClassID = new SelectList(db.ClassAndSectionDetails, "ID", "CampusID", student.ClassID);
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", student.ClassID);
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name", student.SectionID);
            return View(student);
        }

        // GET: Teacher1/CampusStudentDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name", student.CampusID);
            ViewBag.ParentsID = new SelectList(db.Parents, "ID", "ParentsID", student.ParentsID);
            ViewBag.ClassID = new SelectList(db.ClassAndSectionDetails, "ID", "CampusID", student.ClassID);
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", student.ClassID);
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name", student.SectionID);
            return View(student);
        }

        // POST: Teacher1/CampusStudentDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,StudentID,FirstName,LastName,DOB,Gender,Address,Nationality,Password,PhoneNo,ParentsID,ClassID,SectionID,AdmissionYear,IsApproved,CampusID")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name", student.CampusID);
            ViewBag.ParentsID = new SelectList(db.Parents, "ID", "ParentsID", student.ParentsID);
            ViewBag.ClassID = new SelectList(db.ClassAndSectionDetails, "ID", "CampusID", student.ClassID);
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", student.ClassID);
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name", student.SectionID);
            return View(student);
        }

        // GET: Teacher1/CampusStudentDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Teacher1/CampusStudentDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
