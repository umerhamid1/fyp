using StudentMonitoringSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentMonitoringSystem.Areas.Studentss1.Controllers
{
    public class ClassTimeTableController : Controller
    {
        // GET: Studentss1/ClassTimeTable
        private StudentsMonitringSystemEntities db = new StudentsMonitringSystemEntities();

     
        public ActionResult Index()
        {
           

            ViewBag.StudentID = new SelectList(db.Students, "ID", "FirstName");
            var subjectTimeTables = db.SubjectTimeTable1;
            return View(subjectTimeTables.ToList());
        }


      


        public JsonResult GetTimeTable(int ID)
        {



            var TeacherName = "";
            var timming = "";
            var ClassName = "";
            var SectionName = "";
            var query = (from stb1 in db.SubjectTimeTable1
                         join sb in db.subjects on stb1.SubjectID equals sb.ID
                         join cl in db.Classes on stb1.ClassID equals cl.ID
                         join se in db.Sections on stb1.SectionID equals se.ID
                         join stu in db.Students on cl.ID equals stu.ClassID
                         join st in db.SubjectTeachers on sb.ID equals st.SubjectID
                         join te in db.Teachers on st.TeacherID equals te.ID



                         where (stu.ID == ID && sb.ClassID == cl.ID && se.ID == stu.SectionID && st.TeacherID == te.ID)
                         select new
                         {

                             sb.SubjectName,
                             stb1.Days,
                             timming = stb1.StartTime + " To " + stb1.EndTime,
                             ClassName = cl.Name,
                             SectionName = se.Name,
                             TeacherName = te.Name




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