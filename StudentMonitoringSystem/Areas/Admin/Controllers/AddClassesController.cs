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
    public class AddClassesController : Controller
    {
        private StudentsMonitringSystemEntities db = new StudentsMonitringSystemEntities();

        // GET: Admin/AddClasses
        public ActionResult Index()
        {
            var addClasses = db.AddClasses.Include(a => a.Campu).Include(a => a.Class).Include(a => a.Section);
            return View(addClasses.ToList());

        }

        // GET: Admin/AddClasses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddClass addClass = db.AddClasses.Find(id);
            if (addClass == null)
            {
                return HttpNotFound();
            }
            return View(addClass);
        }

        // GET: Admin/AddClasses/Create
        public ActionResult Create()
        {
            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name");
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name");
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name");
            return View();
        }

        // POST: Admin/AddClasses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ClassID,SectionID,CampusID")] AddClass addClass)
        {
            try
            {


                if (ModelState.IsValid)
                {
                    db.AddClasses.Add(addClass);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            catch (Exception ex)
            {
                TempData["Msg"] = "class or section  have already  exist : ";
                return RedirectToAction("Create");
            }

            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name", addClass.CampusID);
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", addClass.ClassID);
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name", addClass.SectionID);
            return View(addClass);
        }

        // GET: Admin/AddClasses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddClass addClass = db.AddClasses.Find(id);
            if (addClass == null)
            {
                return HttpNotFound();
            }
            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name", addClass.CampusID);
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", addClass.ClassID);
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name", addClass.SectionID);
            return View(addClass);
        }

        // POST: Admin/AddClasses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ClassID,SectionID,CampusID")] AddClass addClass)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(addClass).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            catch
        (Exception ex)
            {
                TempData["Msg"] = "class or section  have already  exist : ";
                return RedirectToAction("Create");
            }
            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name", addClass.CampusID);
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", addClass.ClassID);
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name", addClass.SectionID);
            return View(addClass);
        }

        // GET: Admin/AddClasses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddClass addClass = db.AddClasses.Find(id);
            if (addClass == null)
            {
                return HttpNotFound();
            }
            return View(addClass);
   
        }

        // POST: Admin/AddClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                AddClass addClass = db.AddClasses.Find(id);
                db.AddClasses.Remove(addClass);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Msg"] = "First Delete Class's Teacher Details On This Section Then you  Can Delete this Class Details : ";
                return RedirectToAction("Delete");
            }
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
