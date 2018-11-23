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
    public class StudentTimeTableController : Controller
    {
        private StudentsMonitringSystemEntities db = new StudentsMonitringSystemEntities();

        // GET: Parentss1/StudentTimeTable
        public ActionResult Index()
        {
            ViewBag.ParentID = new SelectList(db.Parents, "ID", "FirstName");

            ViewBag.StudentID = new SelectList(db.Students, "ID", "FirstName");
            var subjectTimeTables = db.SubjectTimeTable1.Include(s => s.Campu).Include(s => s.Section);//./*Include(s => s.AmAndPm).Include(s => s.AmAndPm1).Include(s => s.Day).Include(s => s.Hour).Include(s => s.Hour1).Include(s => s.Minute).Include(s => s.Minute1).Include(s => s.Class).Include(s => s.subject);*/
            return View(subjectTimeTables.ToList());
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
                             timming = stb1.StartTime+" To "+stb1.EndTime,
                           ClassName= cl.Name,
                            SectionName =se.Name,
                           TeacherName= te.Name




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
