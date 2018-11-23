using StudentMonitoringSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentMonitoringSystem.Areas.Parentss1.Controllers
{
    public class ParentNoticeBoardController : Controller
    {
        // GET: Parentss1/ParentNoticeBoard

        private StudentsMonitringSystemEntities db = new StudentsMonitringSystemEntities();

        
        public ActionResult Index()
        {

            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name");
            var q = (from nb in db.NoticsBoards

                     select nb

                     ).ToList();

            //  var q = db.NoticsBoards.Where(q => q.NoticeBoardUserType == 1).Tolist();
            // return View(noticsBoards.ToList());
            return View(q);
        }


        public JsonResult GetNoticeBoard(string Code)
        {


            var q = (from nb in db.NoticsBoards
                     join nbu in db.NoticeBoardUserTypes on nb.UserType equals nbu.ID
                     where (nbu.ID == 3 && nb.CampusID == Code)
                     select new
                     {
                         nb.Description,
                         nb.Date

                     }

              ).ToList();
            return Json(q, JsonRequestBehavior.AllowGet);
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