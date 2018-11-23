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
    public class ExamsTimeTablesController : Controller
    {
        private StudentsMonitringSystemEntities db = new StudentsMonitringSystemEntities();

        // GET: Admin/ExamsTimeTables
        public ActionResult Index()
        {
            var examsTimeTables = db.ExamsTimeTables.Include(e => e.Campu).Include(e => e.Class).Include(e => e.Hour).Include(e => e.Hour1).Include(e => e.Minute).Include(e => e.Minute1).Include(e => e.subject).Include(e => e.ExamsType);
            return View(examsTimeTables.ToList());
        }

        // GET: Admin/ExamsTimeTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamsTimeTable examsTimeTable = db.ExamsTimeTables.Find(id);
            if (examsTimeTable == null)
            {
                return HttpNotFound();
            }
            return View(examsTimeTable);
        }

        // GET: Admin/ExamsTimeTables/Create
        public ActionResult Create()
        {
            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name");
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name");
            ViewBag.StartTimeHour = new SelectList(db.Hours, "ID", "ID");
            ViewBag.EndTimeHour = new SelectList(db.Hours, "ID", "ID");
            ViewBag.StratTimeMinute = new SelectList(db.Minutes, "ID", "Minuts");
            ViewBag.EndTimeMinuts = new SelectList(db.Minutes, "ID", "Minuts");
            ViewBag.SubjectID = new SelectList(db.subjects, "ID", "SubjectName");
            ViewBag.ExamsTypeID = new SelectList(db.ExamsTypes, "ID", "Name");
            return View();
        }

        // POST: Admin/ExamsTimeTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ExamsTypeID,ClassID,SubjectID,StartTimeHour,StratTimeMinute,EndTimeHour,EndTimeMinuts,Date,CampusID")] ExamsTimeTable examsTimeTable)
        {
            if (ModelState.IsValid)
            {
                db.ExamsTimeTables.Add(examsTimeTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name", examsTimeTable.CampusID);
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", examsTimeTable.ClassID);
            ViewBag.StartTimeHour = new SelectList(db.Hours, "ID", "ID", examsTimeTable.StartTimeHour);
            ViewBag.EndTimeHour = new SelectList(db.Hours, "ID", "ID", examsTimeTable.EndTimeHour);
            ViewBag.StratTimeMinute = new SelectList(db.Minutes, "ID", "Minuts", examsTimeTable.StratTimeMinute);
            ViewBag.EndTimeMinuts = new SelectList(db.Minutes, "ID", "Minuts", examsTimeTable.EndTimeMinuts);
            ViewBag.SubjectID = new SelectList(db.subjects, "ID", "SubjectName", examsTimeTable.SubjectID);
            ViewBag.ExamsTypeID = new SelectList(db.ExamsTypes, "ID", "Name", examsTimeTable.ExamsTypeID);
            return View(examsTimeTable);
        }

        // GET: Admin/ExamsTimeTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamsTimeTable examsTimeTable = db.ExamsTimeTables.Find(id);
            if (examsTimeTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name", examsTimeTable.CampusID);
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", examsTimeTable.ClassID);
            ViewBag.StartTimeHour = new SelectList(db.Hours, "ID", "ID", examsTimeTable.StartTimeHour);
            ViewBag.EndTimeHour = new SelectList(db.Hours, "ID", "ID", examsTimeTable.EndTimeHour);
            ViewBag.StratTimeMinute = new SelectList(db.Minutes, "ID", "Minuts", examsTimeTable.StratTimeMinute);
            ViewBag.EndTimeMinuts = new SelectList(db.Minutes, "ID", "Minuts", examsTimeTable.EndTimeMinuts);
            ViewBag.SubjectID = new SelectList(db.subjects, "ID", "SubjectName", examsTimeTable.SubjectID);
            ViewBag.ExamsTypeID = new SelectList(db.ExamsTypes, "ID", "Name", examsTimeTable.ExamsTypeID);
            return View(examsTimeTable);
        }

        // POST: Admin/ExamsTimeTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ExamsTypeID,ClassID,SubjectID,StartTimeHour,StratTimeMinute,EndTimeHour,EndTimeMinuts,Date,CampusID")] ExamsTimeTable examsTimeTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(examsTimeTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name", examsTimeTable.CampusID);
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", examsTimeTable.ClassID);
            ViewBag.StartTimeHour = new SelectList(db.Hours, "ID", "ID", examsTimeTable.StartTimeHour);
            ViewBag.EndTimeHour = new SelectList(db.Hours, "ID", "ID", examsTimeTable.EndTimeHour);
            ViewBag.StratTimeMinute = new SelectList(db.Minutes, "ID", "Minuts", examsTimeTable.StratTimeMinute);
            ViewBag.EndTimeMinuts = new SelectList(db.Minutes, "ID", "Minuts", examsTimeTable.EndTimeMinuts);
            ViewBag.SubjectID = new SelectList(db.subjects, "ID", "SubjectName", examsTimeTable.SubjectID);
            ViewBag.ExamsTypeID = new SelectList(db.ExamsTypes, "ID", "Name", examsTimeTable.ExamsTypeID);
            return View(examsTimeTable);
        }

        // GET: Admin/ExamsTimeTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamsTimeTable examsTimeTable = db.ExamsTimeTables.Find(id);
            if (examsTimeTable == null)
            {
                return HttpNotFound();
            }
            return View(examsTimeTable);
        }

        // POST: Admin/ExamsTimeTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExamsTimeTable examsTimeTable = db.ExamsTimeTables.Find(id);
            db.ExamsTimeTables.Remove(examsTimeTable);
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
    }
}
