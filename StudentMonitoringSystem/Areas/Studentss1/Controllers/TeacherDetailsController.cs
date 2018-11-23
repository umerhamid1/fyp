using StudentMonitoringSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentMonitoringSystem.Areas.Studentss1.Controllers
{
    public class TeacherDetailsController : Controller
    {
        // GET: Studentss1/TeacherDetails
        private StudentsMonitringSystemEntities db = new StudentsMonitringSystemEntities();

      
        public ActionResult Index()
        {
           

            ViewBag.StudentID = new SelectList(db.Students, "ID", "FirstName");

            // ViewBag.ParentID1 = new SelectList(db.Parents, "ID", "LastName");


            var teachers = db.Teachers;
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

                             name = st.FirstName + " " +
                             st.LastName
                         }).Distinct().ToList();

            return Json(query, JsonRequestBehavior.AllowGet);


        }
      

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
                             teacherName = te.Name,
                             te.Gender,
                           //  te.Phone,
                             te.Email,
                             cl.Name,
                             sb.SubjectName

                         }).Distinct().ToList();

            return Json(query, JsonRequestBehavior.AllowGet);


        }
    }
}