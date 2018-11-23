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
    public class ClassRoutineController : Controller
    {
        private StudentsMonitringSystemEntities db = new StudentsMonitringSystemEntities();

        // GET: Admin/ClassRoutine
        public ActionResult Index()
        {
            var subjectTimeTable1 = db.SubjectTimeTable1.Include(s => s.Campu).Include(s => s.Class).Include(s => s.Section).Include(s => s.subject);
            return View(subjectTimeTable1.ToList());
        }

        // GET: Admin/ClassRoutine/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubjectTimeTable1 subjectTimeTable1 = db.SubjectTimeTable1.Find(id);
            if (subjectTimeTable1 == null)
            {
                return HttpNotFound();
            }
            return View(subjectTimeTable1);
        }

        // GET: Admin/ClassRoutine/Create
        public ActionResult Create()
        {
            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name");
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name");
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name");
            ViewBag.SubjectID = new SelectList(db.subjects, "ID", "SubjectName");
            return View();
        }

        // POST: Admin/ClassRoutine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CampusID,SectionID,SubjectID,Days,StartTime,EndTime,ClassID")] SubjectTimeTable1 subjectTimeTable1)
        {
            if (ModelState.IsValid)
            {
                db.SubjectTimeTable1.Add(subjectTimeTable1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name", subjectTimeTable1.CampusID);
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", subjectTimeTable1.ClassID);
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name", subjectTimeTable1.SectionID);
            ViewBag.SubjectID = new SelectList(db.subjects, "ID", "SubjectName", subjectTimeTable1.SubjectID);
            return View(subjectTimeTable1);
        }

        // GET: Admin/ClassRoutine/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubjectTimeTable1 subjectTimeTable1 = db.SubjectTimeTable1.Find(id);
            if (subjectTimeTable1 == null)
            {
                return HttpNotFound();
            }
            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name", subjectTimeTable1.CampusID);
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", subjectTimeTable1.ClassID);
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name", subjectTimeTable1.SectionID);
            ViewBag.SubjectID = new SelectList(db.subjects, "ID", "SubjectName", subjectTimeTable1.SubjectID);
            return View(subjectTimeTable1);
        }

        // POST: Admin/ClassRoutine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CampusID,SectionID,SubjectID,Days,StartTime,EndTime,ClassID")] SubjectTimeTable1 subjectTimeTable1)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subjectTimeTable1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name", subjectTimeTable1.CampusID);
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", subjectTimeTable1.ClassID);
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name", subjectTimeTable1.SectionID);
            ViewBag.SubjectID = new SelectList(db.subjects, "ID", "SubjectName", subjectTimeTable1.SubjectID);
            return View(subjectTimeTable1);
        }

        // GET: Admin/ClassRoutine/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubjectTimeTable1 subjectTimeTable1 = db.SubjectTimeTable1.Find(id);
            if (subjectTimeTable1 == null)
            {
                return HttpNotFound();
            }
            return View(subjectTimeTable1);
        }

        // POST: Admin/ClassRoutine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubjectTimeTable1 subjectTimeTable1 = db.SubjectTimeTable1.Find(id);
            db.SubjectTimeTable1.Remove(subjectTimeTable1);
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

        public JsonResult GetSubjects(int ID)
        {


            var data = (from c in db.Classes
                        join cn in db.subjects on c.ID equals cn.ClassID
                        where (cn.ClassID == ID)

                        select new
                        {
                            cn.ID,
                            cn.SubjectName

                        }).Distinct().ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
