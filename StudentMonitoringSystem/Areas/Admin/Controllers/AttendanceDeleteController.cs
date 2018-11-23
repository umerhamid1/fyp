
//using StudentMonitoringSystem.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace StudentMonitoringSystem.Controllers
//{
//    public class AttendanceController : Controller
//    {
//        private StudentsMonitringSystemEntities db = new StudentsMonitringSystemEntities();
//        // GET: Attendance
//        public ActionResult MarkAttendance()
//        {
//            ViewBag.Class = new SelectList(db.Classes, "ID", "Name");
//            return View();
//        }

//        public PartialViewResult GetStudents(DateTime Date, int ClassID)
//        {
//            var data = db.Students.Where(x => x.ClassID == ClassID).ToList();
//            AttendanceViewModel AttendViewData = new AttendanceViewModel();
//            AttendViewData.Date = Date;
//            AttendViewData.ClassID = ClassID;
//            AttendViewData.StudentList = new List<StudentAttendance>();
//            int i = 0;
//            foreach (var temp in data)
//            {
//                AttendViewData.StudentList.Add(new StudentAttendance());
//                AttendViewData.StudentList[i].StudentID = temp.ID;
//                AttendViewData.StudentList[i].Student2 = new Student();
//                AttendViewData.StudentList[i].Student2.FirstName = temp.FirstName;
//                i++;
//            }
//            return PartialView("_StudentData", AttendViewData);
//        }
//        [HttpPost]
//        public ViewResult MarkAttendance(AttendanceViewModel attend)
//        {
//            if (ModelState.IsValid)
//            {
//                Attendance att = new Attendance();
//                att.ClassID = attend.ClassID;
//                att.Date = attend.Date.ToString();
//                db.Attendances.Add(att);
//                db.SaveChanges();
//                //StudentAttendence stdAtt = new StudentAttendence();
//                for (int i = 0; i < attend.StudentList.Count(); i++)
//                {
//                    attend.StudentList[i].AttendanceID = att.ID; // here is closed
//                }
//                db.StudentAttendances.AddRange(attend.StudentList);
//                db.SaveChanges();


//                //ModelState.Clear();
//            }

//            ViewBag.Class = new SelectList(db.Classes, "ID", "Name");
//            return View();
//        }
//    }
//}