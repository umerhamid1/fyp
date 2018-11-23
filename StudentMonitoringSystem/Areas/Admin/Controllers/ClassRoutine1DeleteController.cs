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
    public class ClassRoutine1DeleteController : Controller
    {
        private StudentsMonitringSystemEntities db = new StudentsMonitringSystemEntities();

        // GET: Admin/ClassRoutine
        public ActionResult Index()
        {
            var subjectTimeTables = db.SubjectTimeTables.Include(s => s.Campu).Include(s => s.Section).Include(s => s.subject).Include(s => s.AmAndPm).Include(s => s.AmAndPm1).Include(s => s.Day).Include(s => s.Hour).Include(s => s.Hour1).Include(s => s.Minute).Include(s => s.Minute1).Include(s => s.Class);
            return View(subjectTimeTables.ToList());
        }

        // GET: Admin/ClassRoutine/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubjectTimeTable subjectTimeTable = db.SubjectTimeTables.Find(id);
            if (subjectTimeTable == null)
            {
                return HttpNotFound();
            }
            return View(subjectTimeTable);
        }

        // GET: Admin/ClassRoutine/Create
        public ActionResult Create()
        {
            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name");
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name");
            ViewBag.SubjectID = new SelectList(db.subjects, "ID", "SubjectName");
            ViewBag.StartOClockID = new SelectList(db.AmAndPms, "ID", "Name");
            ViewBag.EmdOClockID = new SelectList(db.AmAndPms, "ID", "Name");
            ViewBag.DayID = new SelectList(db.Days, "ID", "Name");
            ViewBag.StartHourID = new SelectList(db.Hours, "ID", "ID");
            ViewBag.EndHourID = new SelectList(db.Hours, "ID", "ID");
            ViewBag.StartMintID = new SelectList(db.Minutes, "ID", "Minuts");
            ViewBag.EndMintID = new SelectList(db.Minutes, "ID", "Minuts");
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name");
            return View();
        }

        // POST: Admin/ClassRoutine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CampusID,SectionID,SubjectID,DayID,StartHourID,StartMintID,EndHourID,EndMintID,StartOClockID,EmdOClockID,ClassID")] SubjectTimeTable subjectTimeTable)
        {
            if (ModelState.IsValid)
            {
                db.SubjectTimeTables.Add(subjectTimeTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name", subjectTimeTable.CampusID);
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name", subjectTimeTable.SectionID);
            ViewBag.SubjectID = new SelectList(db.subjects, "ID", "SubjectName", subjectTimeTable.SubjectID);
            ViewBag.StartOClockID = new SelectList(db.AmAndPms, "ID", "Name", subjectTimeTable.StartOClockID);
            ViewBag.EmdOClockID = new SelectList(db.AmAndPms, "ID", "Name", subjectTimeTable.EmdOClockID);
            ViewBag.DayID = new SelectList(db.Days, "ID", "Name", subjectTimeTable.DayID);
            ViewBag.StartHourID = new SelectList(db.Hours, "ID", "ID", subjectTimeTable.StartHourID);
            ViewBag.EndHourID = new SelectList(db.Hours, "ID", "ID", subjectTimeTable.EndHourID);
            ViewBag.StartMintID = new SelectList(db.Minutes, "ID", "Minuts", subjectTimeTable.StartMintID);
            ViewBag.EndMintID = new SelectList(db.Minutes, "ID", "Minuts", subjectTimeTable.EndMintID);
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", subjectTimeTable.ClassID);
            return View(subjectTimeTable);
        }

        // GET: Admin/ClassRoutine/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubjectTimeTable subjectTimeTable = db.SubjectTimeTables.Find(id);
            if (subjectTimeTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name", subjectTimeTable.CampusID);
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name", subjectTimeTable.SectionID);
            ViewBag.SubjectID = new SelectList(db.subjects, "ID", "SubjectName", subjectTimeTable.SubjectID);
            ViewBag.StartOClockID = new SelectList(db.AmAndPms, "ID", "Name", subjectTimeTable.StartOClockID);
            ViewBag.EmdOClockID = new SelectList(db.AmAndPms, "ID", "Name", subjectTimeTable.EmdOClockID);
            ViewBag.DayID = new SelectList(db.Days, "ID", "Name", subjectTimeTable.DayID);
            ViewBag.StartHourID = new SelectList(db.Hours, "ID", "ID", subjectTimeTable.StartHourID);
            ViewBag.EndHourID = new SelectList(db.Hours, "ID", "ID", subjectTimeTable.EndHourID);
            ViewBag.StartMintID = new SelectList(db.Minutes, "ID", "Minuts", subjectTimeTable.StartMintID);
            ViewBag.EndMintID = new SelectList(db.Minutes, "ID", "Minuts", subjectTimeTable.EndMintID);
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", subjectTimeTable.ClassID);
            return View(subjectTimeTable);
        }

        // POST: Admin/ClassRoutine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CampusID,SectionID,SubjectID,DayID,StartHourID,StartMintID,EndHourID,EndMintID,StartOClockID,EmdOClockID,ClassID")] SubjectTimeTable subjectTimeTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subjectTimeTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name", subjectTimeTable.CampusID);
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name", subjectTimeTable.SectionID);
            ViewBag.SubjectID = new SelectList(db.subjects, "ID", "SubjectName", subjectTimeTable.SubjectID);
            ViewBag.StartOClockID = new SelectList(db.AmAndPms, "ID", "Name", subjectTimeTable.StartOClockID);
            ViewBag.EmdOClockID = new SelectList(db.AmAndPms, "ID", "Name", subjectTimeTable.EmdOClockID);
            ViewBag.DayID = new SelectList(db.Days, "ID", "Name", subjectTimeTable.DayID);
            ViewBag.StartHourID = new SelectList(db.Hours, "ID", "ID", subjectTimeTable.StartHourID);
            ViewBag.EndHourID = new SelectList(db.Hours, "ID", "ID", subjectTimeTable.EndHourID);
            ViewBag.StartMintID = new SelectList(db.Minutes, "ID", "Minuts", subjectTimeTable.StartMintID);
            ViewBag.EndMintID = new SelectList(db.Minutes, "ID", "Minuts", subjectTimeTable.EndMintID);
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", subjectTimeTable.ClassID);
            return View(subjectTimeTable);
        }

        // GET: Admin/ClassRoutine/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubjectTimeTable subjectTimeTable = db.SubjectTimeTables.Find(id);
            if (subjectTimeTable == null)
            {
                return HttpNotFound();
            }
            return View(subjectTimeTable);
        }

        // POST: Admin/ClassRoutine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubjectTimeTable subjectTimeTable = db.SubjectTimeTables.Find(id);
            db.SubjectTimeTables.Remove(subjectTimeTable);
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
