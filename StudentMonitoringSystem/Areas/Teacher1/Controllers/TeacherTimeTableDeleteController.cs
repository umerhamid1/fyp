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
    public class TeacherTimeTableDeleteController : Controller
    {
        private StudentsMonitringSystemEntities db = new StudentsMonitringSystemEntities();

        // GET: Teacher1/TeacherTimeTable
        public ActionResult Index()
        {
            ViewBag.TeacherID = new SelectList(db.Teachers, "ID", "Name");
            var subjectTimeTables = db.SubjectTimeTables.Include(s => s.Campu).Include(s => s.Section).Include(s => s.AmAndPm).Include(s => s.AmAndPm1).Include(s => s.Day).Include(s => s.Hour).Include(s => s.Hour1).Include(s => s.Minute).Include(s => s.Minute1).Include(s => s.Class).Include(s => s.subject);
            return View(subjectTimeTables.ToList());
        }


        public JsonResult GetTimeTable(int ID)
        {
            //    FROM AmAndPm INNER JOIN
            //          SubjectTimeTable ON AmAndPm.ID = SubjectTimeTable.StartOClockID AND AmAndPm.ID = SubjectTimeTable.EmdOClockID INNER JOIN
            //          Section ON SubjectTimeTable.SectionID = Section.ID INNER JOIN
            //          subject ON SubjectTimeTable.SubjectID = subject.ID INNER JOIN
            //          Days ON SubjectTimeTable.DayID = Days.ID INNER JOIN
            //          Hour ON SubjectTimeTable.StartHourID = Hour.ID AND SubjectTimeTable.EndHourID = Hour.ID INNER JOIN
            //          Minutes ON SubjectTimeTable.StartMintID = Minutes.ID AND SubjectTimeTable.EndMintID = Minutes.ID INNER JOIN
            //          AmAndPm AS AmAndPm_1 ON SubjectTimeTable.StartOClockID = AmAndPm_1.ID AND SubjectTimeTable.EmdOClockID = AmAndPm_1.ID

            string hour="";
            var data1 = (from st in db.SubjectTeachers 
                         from cl in db.Classes
                         from day in db.Days
                         from h in db.Hours
                         from m in db.Minutes
                         from am  in db.AmAndPms

                         join stt5 in db.SubjectTimeTables on am.ID equals stt5.StartOClockID
                         join stt4 in db.SubjectTimeTables on m.ID equals stt4.StartMintID
                         join stt3 in db.SubjectTimeTables on h.ID equals stt3.StartHourID
                         join stt2 in db.SubjectTimeTables on day.ID equals stt2.DayID
                         join stt1 in db.SubjectTimeTables on am.ID equals stt1.StartOClockID
                         join te in db.Teachers on st.TeacherID equals te.ID
                        
                        from se in db.Sections
                         join stt in db.SubjectTimeTables on se.ID equals stt.SectionID
                         join sb in db.subjects on stt.SubjectID equals sb.ID
                         join st1 in db.SubjectTeachers on se.ID equals st1.SectionID
                         join sb1 in db.subjects on st1.SubjectID equals sb1.ID


                         where (te.ID == ID && am.ID == stt1.EndHourID && stt3.EndHourID == h.ID && stt4.EndMintID == m.ID && stt5.EndHourID == am.ID)
                         select new
                         {
                            // stt.ID,
                             se.Name,
                           //  c.s

                             stt.DayID,
                             stt.subject.SubjectName,
                            hour= am.Name

                         }
                        ).ToList();
            return Json(data1, JsonRequestBehavior.AllowGet);
        }

        // GET: Teacher1/TeacherTimeTable/Details/5
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

        // GET: Teacher1/TeacherTimeTable/Create
        public ActionResult Create()
        {
            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name");
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name");
            ViewBag.StartOClockID = new SelectList(db.AmAndPms, "ID", "Name");
            ViewBag.EmdOClockID = new SelectList(db.AmAndPms, "ID", "Name");
            ViewBag.DayID = new SelectList(db.Days, "ID", "Name");
            ViewBag.StartHourID = new SelectList(db.Hours, "ID", "ID");
            ViewBag.EndHourID = new SelectList(db.Hours, "ID", "ID");
            ViewBag.StartMintID = new SelectList(db.Minutes, "ID", "Minuts");
            ViewBag.EndMintID = new SelectList(db.Minutes, "ID", "Minuts");
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name");
            ViewBag.SubjectID = new SelectList(db.subjects, "ID", "SubjectName");
            return View();
        }

        // POST: Teacher1/TeacherTimeTable/Create
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
            ViewBag.StartOClockID = new SelectList(db.AmAndPms, "ID", "Name", subjectTimeTable.StartOClockID);
            ViewBag.EmdOClockID = new SelectList(db.AmAndPms, "ID", "Name", subjectTimeTable.EmdOClockID);
            ViewBag.DayID = new SelectList(db.Days, "ID", "Name", subjectTimeTable.DayID);
            ViewBag.StartHourID = new SelectList(db.Hours, "ID", "ID", subjectTimeTable.StartHourID);
            ViewBag.EndHourID = new SelectList(db.Hours, "ID", "ID", subjectTimeTable.EndHourID);
            ViewBag.StartMintID = new SelectList(db.Minutes, "ID", "Minuts", subjectTimeTable.StartMintID);
            ViewBag.EndMintID = new SelectList(db.Minutes, "ID", "Minuts", subjectTimeTable.EndMintID);
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", subjectTimeTable.ClassID);
            ViewBag.SubjectID = new SelectList(db.subjects, "ID", "SubjectName", subjectTimeTable.SubjectID);
            return View(subjectTimeTable);
        }

        // GET: Teacher1/TeacherTimeTable/Edit/5
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
            ViewBag.StartOClockID = new SelectList(db.AmAndPms, "ID", "Name", subjectTimeTable.StartOClockID);
            ViewBag.EmdOClockID = new SelectList(db.AmAndPms, "ID", "Name", subjectTimeTable.EmdOClockID);
            ViewBag.DayID = new SelectList(db.Days, "ID", "Name", subjectTimeTable.DayID);
            ViewBag.StartHourID = new SelectList(db.Hours, "ID", "ID", subjectTimeTable.StartHourID);
            ViewBag.EndHourID = new SelectList(db.Hours, "ID", "ID", subjectTimeTable.EndHourID);
            ViewBag.StartMintID = new SelectList(db.Minutes, "ID", "Minuts", subjectTimeTable.StartMintID);
            ViewBag.EndMintID = new SelectList(db.Minutes, "ID", "Minuts", subjectTimeTable.EndMintID);
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", subjectTimeTable.ClassID);
            ViewBag.SubjectID = new SelectList(db.subjects, "ID", "SubjectName", subjectTimeTable.SubjectID);
            return View(subjectTimeTable);
        }

        // POST: Teacher1/TeacherTimeTable/Edit/5
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
            ViewBag.StartOClockID = new SelectList(db.AmAndPms, "ID", "Name", subjectTimeTable.StartOClockID);
            ViewBag.EmdOClockID = new SelectList(db.AmAndPms, "ID", "Name", subjectTimeTable.EmdOClockID);
            ViewBag.DayID = new SelectList(db.Days, "ID", "Name", subjectTimeTable.DayID);
            ViewBag.StartHourID = new SelectList(db.Hours, "ID", "ID", subjectTimeTable.StartHourID);
            ViewBag.EndHourID = new SelectList(db.Hours, "ID", "ID", subjectTimeTable.EndHourID);
            ViewBag.StartMintID = new SelectList(db.Minutes, "ID", "Minuts", subjectTimeTable.StartMintID);
            ViewBag.EndMintID = new SelectList(db.Minutes, "ID", "Minuts", subjectTimeTable.EndMintID);
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", subjectTimeTable.ClassID);
            ViewBag.SubjectID = new SelectList(db.subjects, "ID", "SubjectName", subjectTimeTable.SubjectID);
            return View(subjectTimeTable);
        }

        // GET: Teacher1/TeacherTimeTable/Delete/5
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

        // POST: Teacher1/TeacherTimeTable/Delete/5
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
    }
}
