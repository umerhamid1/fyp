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
    public class TeacherTimeTableController : Controller
    {
        private StudentsMonitringSystemEntities db = new StudentsMonitringSystemEntities();

        // GET: Teacher1/TeacherTimeTable
        public ActionResult Index()
        {
            ViewBag.TeacherID = new SelectList(db.Teachers, "ID", "Name");
            var subjectTimeTable1 = db.SubjectTimeTable1.Include(s => s.Campu).Include(s => s.Class).Include(s => s.Section).Include(s => s.subject);
            return View(subjectTimeTable1.ToList());
        }

        // GET: Teacher1/TeacherTimeTable/Details/5



        public JsonResult GetTimeTable(int ID)
        {


            var TeacherName = "";
            var timming = "";
            var ClassName = "";
            var SectionName = "";

            string hour = "";
            var data1 = (from st in db.SubjectTeachers
                         join se in db.Sections on st.SectionID equals se.ID
                         join cl in db.Classes on st.ClassID equals cl.ID
                         join sb in db.subjects on st.SubjectID equals sb.ID
                         join stb1 in db.SubjectTimeTable1 on sb.ID equals stb1.SubjectID 
                         join te in db.Teachers on st.TeacherID equals te.ID



                        


                         where (te.ID == ID &&  cl.ID == sb.ClassID )
                         select new
                         {

                             sb.SubjectName,
                             stb1.Days,
                             timming = stb1.StartTime + " To " + stb1.EndTime,
                             ClassName = cl.Name,
                             SectionName = se.Name,
                             TeacherName = te.Name

                             

                         }
                        ).ToList();
            return Json(data1, JsonRequestBehavior.AllowGet);
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
