using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentMonitoringSystem.Models;

namespace StudentMonitoringSystem.Areas.Parentss1.Controllers
{
    public class TeachersDetailController : Controller
    {
        private StudentsMonitringSystemEntities db = new StudentsMonitringSystemEntities();

        // GET: Parentss1/TeachersDetail
        public ActionResult Index()
        {
            ViewBag.ParentID = new SelectList(db.Parents, "ID", "FirstName");

            ViewBag.StudentID = new SelectList(db.Students, "ID", "FirstName");

            // ViewBag.ParentID1 = new SelectList(db.Parents, "ID", "LastName");


            var teachers = db.Teachers.Include(t => t.Campu);
            return View(teachers.ToList());
        }

        public JsonResult GetStudents(int ID)
        {


            var name = "";
            var query = (from pe in db.Parents
                         join st in db.Students on pe.ID equals st.ParentsID

                         where (pe.ID == ID)
                         select new
                         {
                             st.ID,

                           name=  st.FirstName +" "+
                             st.LastName
                         }).Distinct().ToList();

            return Json(query, JsonRequestBehavior.AllowGet);


        }
//        SELECT Class.Name AS Expr1, Teacher.*, Student.ID AS Expr2
//FROM     Teacher INNER JOIN
//                  SubjectTeacher ON Teacher.ID = SubjectTeacher.TeacherID INNER JOIN
//                  Class ON SubjectTeacher.ClassID = Class.ID INNER JOIN
//                  Student ON Class.ID = Student.ClassID INNER JOIN
//                  subject ON SubjectTeacher.SubjectID = subject.ID AND Class.ID = subject.ClassID
//WHERE  (Student.ID = 14001)

        public JsonResult GetTeachersDetails(int ID)
        {


            var teacherName = "";
          
            var query = (from te in db.Teachers
                         join st in db.SubjectTeachers on te.ID equals st.TeacherID
                         join cl in db.Classes on st.ClassID equals cl.ID
                         join stu in db.Students on cl.ID equals stu.ClassID
                         join sb in db.subjects on st.SubjectID equals sb.ID

                         where (stu.ID == ID && cl.ID == sb.ClassID)
                         select new
                         {
                             teacherName =  te.Name,
                            te.Gender,
                         //   te.Phone,
                            te.Email,
                            cl.Name,
                            sb.SubjectName
                            
                         }).Distinct().ToList();

            return Json(query, JsonRequestBehavior.AllowGet);


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
