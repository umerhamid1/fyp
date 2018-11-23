using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentMonitoringSystem.Models;

namespace StudentMonitoringSystem.Areas.Teacher1.Controllers
{
    public class StudentAssignmentController : Controller
    {
        private StudentsMonitringSystemEntities db = new StudentsMonitringSystemEntities();

        // GET: Teacher1/StudentAssignment
        public ActionResult Index()
        {
            var assignments = db.Assignments.Include(a => a.Class).Include(a => a.Section).Include(a => a.Teacher).Include(a => a.subject);
            return View(assignments.ToList());
        }

        // GET: Teacher1/StudentAssignment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        // GET: Teacher1/StudentAssignment/Create
        public ActionResult Create()
        {
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name");
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name");
            ViewBag.TeacherID = new SelectList(db.Teachers, "ID", "Name");
            ViewBag.SubjectID = new SelectList(db.subjects, "ID", "SubjectName");
            return View();
        }

        // POST: Teacher1/StudentAssignment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TeacherID,ClassID,SectionID,SubmitDate,Description,SubjectID")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                db.Assignments.Add(assignment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", assignment.ClassID);
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name", assignment.SectionID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "ID", "Name", assignment.TeacherID);
            ViewBag.SubjectID = new SelectList(db.subjects, "ID", "SubjectName", assignment.SubjectID);
            return View(assignment);
        }

        // GET: Teacher1/StudentAssignment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", assignment.ClassID);
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name", assignment.SectionID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "ID", "Name", assignment.TeacherID);
            ViewBag.SubjectID = new SelectList(db.subjects, "ID", "SubjectName", assignment.SubjectID);
            return View(assignment);
        }

        // POST: Teacher1/StudentAssignment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TeacherID,ClassID,SectionID,SubmitDate,Description,SubjectID")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", assignment.ClassID);
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name", assignment.SectionID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "ID", "Name", assignment.TeacherID);
            ViewBag.SubjectID = new SelectList(db.subjects, "ID", "SubjectName", assignment.SubjectID);
            return View(assignment);
        }

        // GET: Teacher1/StudentAssignment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return HttpNotFound();
            }
            return View(assignment);
        }

        // POST: Teacher1/StudentAssignment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Assignment assignment = db.Assignments.Find(id);
            db.Assignments.Remove(assignment);
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




        public JsonResult GetClasses(int ID)
        {


           


            var query = (from te in db.Teachers
                join st in db.SubjectTeachers on te.ID equals st.TeacherID
                join cl in db.Classes on st.ClassID equals cl.ID
                where (te.ID == ID)
                select new
                {
                    cl.ID,
                    cl.Name
                }).Distinct().ToList();

            return Json(query, JsonRequestBehavior.AllowGet);


        }



        public JsonResult GetSection(int ID, int tID)
        {

            var data = (from st in db.SubjectTeachers
                join se in db.Sections on st.SectionID equals se.ID
                join cl in db.Classes on st.ClassID equals cl.ID
                join te in db.Teachers on st.TeacherID equals te.ID
                where ((se.ID == ID && te.ID == tID))
                select new
                {
                    se.ID,
                    se.Name
                }).Distinct().ToList();




            return Json(data, JsonRequestBehavior.AllowGet);

        }





        public JsonResult GetSubject(int ID, int tID, int cID)
        { // here is some work is remain.

        var data = (from st in db.SubjectTeachers
            join te in db.Teachers on st.TeacherID equals  te.ID
            join se in db.Sections on st.SectionID equals  se.ID
            join sb in db.subjects on st.SubjectID equals sb.ID
            join cl in db.Classes on st.ClassID     equals cl.ID

           
             where ((se.ID == 1 && te.ID == 7 && cl.ID== 1 ))

                   select new
                   {
                    sb.ID,
                    sb.SubjectName
                   }).Distinct().ToList();




            return Json(data, JsonRequestBehavior.AllowGet);

    }


}
}
