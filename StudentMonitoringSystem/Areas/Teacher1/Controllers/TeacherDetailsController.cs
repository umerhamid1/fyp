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
    public class TeacherDetailsController : Controller
    {
        private StudentsMonitringSystemEntities db = new StudentsMonitringSystemEntities();

        // GET: Teacher1/TeacherDetails
        public ActionResult Index()
        {
            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name");


            var teachers = db.Teachers.Include(e => e.Campu);
           // var teachers = db.Teachers.Include(t => t.Campu);
            return View(teachers.ToList());
        }

        public JsonResult GetCampus(string ID)
        {
            var data = (from tb in db.Campus
                where tb.Code == ID
                select new
                {
                    tb.Code,
                    tb.Name
                }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTeacher(string ID)
        {
            var data = (from tb in db.Teachers
                where tb.CampusID == ID
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
