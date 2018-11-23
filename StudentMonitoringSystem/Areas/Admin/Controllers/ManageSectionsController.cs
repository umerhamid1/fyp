using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentMonitoringSystem.Models;

namespace StudentMonitoringSystem.Areas.Admin.Controllers
{
    public class ManageSectionsController : Controller
    {
        private StudentsMonitringSystemEntities db = new StudentsMonitringSystemEntities();

        // GET: Admin/ManageSections
        public ActionResult Index()
        {
            var classAndSectionDetails = db.ClassAndSectionDetails.Include(c => c.Campu).Include(c => c.Class).Include(c => c.Section).Include(c => c.Teacher);
            return View(classAndSectionDetails.ToList());
        }

        // GET: Admin/ManageSections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassAndSectionDetail classAndSectionDetail = db.ClassAndSectionDetails.Find(id);
            if (classAndSectionDetail == null)
            {
                return HttpNotFound();
            }
            return View(classAndSectionDetail);
        }

        // GET: Admin/ManageSections/Create
        public ActionResult Create()
        {
            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name");
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name");
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name");
            ViewBag.TeacherID = new SelectList(db.Teachers, "ID", "ID");
            return View();
        }

        // POST: Admin/ManageSections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SectionID,TeacherID,CampusID,ClassID")] ClassAndSectionDetail classAndSectionDetail)
        {
            if (ModelState.IsValid)
            {
                try
                {


                    db.ClassAndSectionDetails.Add(classAndSectionDetail);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["Msg"] = "class's section Or Teacher duplication occour : ";
                    return RedirectToAction("Create");
                }


            }

            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name", classAndSectionDetail.CampusID);
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", classAndSectionDetail.ClassID);
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name", classAndSectionDetail.SectionID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "ID", "ID", classAndSectionDetail.TeacherID);
            return View(classAndSectionDetail);
        }

        // GET: Admin/ManageSections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassAndSectionDetail classAndSectionDetail = db.ClassAndSectionDetails.Find(id);
            if (classAndSectionDetail == null)
            {
                return HttpNotFound();
            }

            ViewBag.CampusID =
                new SelectList(db.Campus, "Code", "Name", classAndSectionDetail.CampusID);
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", classAndSectionDetail.ClassID);
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name", classAndSectionDetail.SectionID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "ID", "Name", classAndSectionDetail.TeacherID);
            return View(classAndSectionDetail);
        }

        // POST: Admin/ManageSections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SectionID,TeacherID,CampusID,ClassID")] ClassAndSectionDetail classAndSectionDetail)
        {
            if (ModelState.IsValid)
            {
                try
                {


                    db.Entry(classAndSectionDetail).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["Msg"] = "class's section Or Teacher duplication occour : ";
                    return RedirectToAction("Edit");
                }
            }
            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name", classAndSectionDetail.CampusID);
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", classAndSectionDetail.ClassID);
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name", classAndSectionDetail.SectionID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "ID", "Name", classAndSectionDetail.TeacherID);
            return View(classAndSectionDetail);
        }

        // GET: Admin/ManageSections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassAndSectionDetail classAndSectionDetail = db.ClassAndSectionDetails.Find(id);
            if (classAndSectionDetail == null)
            {
                return HttpNotFound();
            }
            return View(classAndSectionDetail);
        }

        // POST: Admin/ManageSections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClassAndSectionDetail classAndSectionDetail = db.ClassAndSectionDetails.Find(id);
            db.ClassAndSectionDetails.Remove(classAndSectionDetail);
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



        public JsonResult GetClasses(string ID)
        {
            

            var data = (from c in db.AddClasses
                         join cn in db.Classes on c.ClassID equals cn.ID
                        where(c.CampusID == ID)

                                         select new
                                         {
                                             cn.ID,
                                             cn.Name
                                             
                                         }).Distinct().ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


    

        public JsonResult GetSection(int ID)
        {

            var data = (from c in db.AddClasses
                join cn in db.Sections on c.SectionID equals cn.ID
                where (c.ClassID == ID)

                select new
                {
                    cn.ID,
                    cn.Name

                }).Distinct().ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
