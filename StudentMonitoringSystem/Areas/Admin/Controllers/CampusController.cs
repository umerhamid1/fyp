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
    public class CampusController : Controller
    {
        private StudentsMonitringSystemEntities db = new StudentsMonitringSystemEntities();

        // GET: Admin/Campus
        public ActionResult Index()
        {
            var campus = db.Campus.Include(c => c.City);
            return View(campus.ToList());
        }

        // GET: Admin/Campus/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campu campu = db.Campus.Find(id);
            if (campu == null)
            {
                return HttpNotFound();
            }
            return View(campu);
        }

        // GET: Admin/Campus/Create
        public ActionResult Create()
        {
            ViewBag.CityID = new SelectList(db.Cities, "ID", "CityCode");
            return View();
        }

        // POST: Admin/Campus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Code,Name,Address,PhoneNo,Email,CityID,Gender,ClassLevel")] Campu campu)
        {
            if (ModelState.IsValid)
            {
                db.Campus.Add(campu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CityID = new SelectList(db.Cities, "ID", "CityCode", campu.CityID);
            return View(campu);
        }

        // GET: Admin/Campus/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campu campu = db.Campus.Find(id);
            if (campu == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityID = new SelectList(db.Cities, "ID", "CityCode", campu.CityID);
            return View(campu);
        }

        // POST: Admin/Campus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Code,Name,Address,PhoneNo,Email,CityID,Gender,ClassLevel")] Campu campu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(campu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityID = new SelectList(db.Cities, "ID", "CityCode", campu.CityID);
            return View(campu);
        }

        // GET: Admin/Campus/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campu campu = db.Campus.Find(id);
            if (campu == null)
            {
                return HttpNotFound();
            }
            return View(campu);
        }

        // POST: Admin/Campus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Campu campu = db.Campus.Find(id);
            db.Campus.Remove(campu);
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
    }
}
