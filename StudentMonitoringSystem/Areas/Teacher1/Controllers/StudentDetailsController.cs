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
    public class StudentDetailsController : Controller
    {
        private StudentsMonitringSystemEntities db = new StudentsMonitringSystemEntities();

        // GET: Teacher1/StudentDetails
        public ActionResult Index()
        {

            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name");
            ViewBag.TeacherID = new SelectList(db.Teachers, "ID", "Name");
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name");
            var students = db.Students.Include(s => s.Campu).Include(s => s.Parent).Include(s => s.ClassAndSectionDetail).Include(s => s.Class).Include(s => s.Section);
            return View(students.ToList());
        }

        // GET: Teacher1/StudentDetails/Details/5
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

        // GET: Teacher1/StudentDetails/Create
      
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

        public JsonResult GetClasses(int ID)
        {

            var q = (from s in db.SubjectTeachers
                join t in db.Teachers on s.TeacherID equals t.ID

                join cl in db.Classes on s.ClassID equals cl.ID
                where (t.ID == ID)
                select new
                {
                    cl.ID,
                    cl.Name

                }).Distinct().ToList();
            return Json(q, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult GetTeacherClass(int ID)
        //{
        //    var data = (from cl in db.Classes
        //        join sb in db.subjects on cl.ID equals sb.ClassID

        //        join te in db.Teachers on sb.TeacherID equals te.ID

        //        where te.ID == ID
        //        select new
        //        {
        //            te.ID,
        //            te.Name,


        //        }).ToList();
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}





        public JsonResult GetClassSection(int ID)
        { // here is work remaining......
            var data = (from cl in db.Classes
                join sb in db.subjects on cl.ID equals sb.ClassID

            //    join te in db.Teachers on sb.TeacherID equals te.ID

               // where te.ID == ID
                select new
                {
                   // te.ID,
                   // te.Name,


                }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);


            //SELECT Section.ID, Section.Name, Class.ID AS Expr1
            //    FROM     SubjectTeacher INNER JOIN
            //    Section ON SubjectTeacher.SectionID = Section.ID INNER JOIN
            //Teacher ON SubjectTeacher.TeacherID = Teacher.ID INNER JOIN
            //Class ON SubjectTeacher.ClassID = Class.ID
            //WHERE(Class.ID = 5)
        }





        public JsonResult GetStudents(int ID)
        {
            var data = (from tb in db.Teachers
              ///  where tb.CampusID = ID
                select new
                {
                    tb.TeacherID,
                    tb.Name,
                    tb.CampusID,
                    tb.Email,
                    tb.Phone

                }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
