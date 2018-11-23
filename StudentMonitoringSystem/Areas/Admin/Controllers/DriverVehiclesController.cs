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
    public class DriverVehiclesController : Controller
    {
        private StudentsMonitringSystemEntities db = new StudentsMonitringSystemEntities();

        // GET: Admin/DriverVehicles
        public ActionResult Index()
        {
            var driverVehicles = db.DriverVehicles.Include(d => d.Driver);
            return View(driverVehicles.ToList());
        }

        // GET: Admin/DriverVehicles/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DriverVehicle driverVehicle = db.DriverVehicles.Find(id);
            if (driverVehicle == null)
            {
                return HttpNotFound();
            }
            return View(driverVehicle);
        }

        // GET: Admin/DriverVehicles/Create
        public ActionResult Create()
        {
            ViewBag.DriverID = new SelectList(db.Drivers, "ID", "Name");
            return View();
        }

        // POST: Admin/DriverVehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VechicleID,DriverID,Model,NoOfSeats,VehicleCompany,VehicleName")] DriverVehicle driverVehicle)
        {
            if (ModelState.IsValid)
            {
                db.DriverVehicles.Add(driverVehicle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DriverID = new SelectList(db.Drivers, "ID", "Name", driverVehicle.DriverID);
            return View(driverVehicle);
        }

        // GET: Admin/DriverVehicles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DriverVehicle driverVehicle = db.DriverVehicles.Find(id);
            if (driverVehicle == null)
            {
                return HttpNotFound();
            }
            ViewBag.DriverID = new SelectList(db.Drivers, "ID", "CampusID", driverVehicle.DriverID);
            return View(driverVehicle);
        }

        // POST: Admin/DriverVehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VechicleID,DriverID,Model,NoOfSeats,VehicleCompany,VehicleName")] DriverVehicle driverVehicle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(driverVehicle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DriverID = new SelectList(db.Drivers, "ID", "CampusID", driverVehicle.DriverID);
            return View(driverVehicle);
        }

        // GET: Admin/DriverVehicles/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DriverVehicle driverVehicle = db.DriverVehicles.Find(id);
            if (driverVehicle == null)
            {
                return HttpNotFound();
            }
            return View(driverVehicle);
        }

        // POST: Admin/DriverVehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DriverVehicle driverVehicle = db.DriverVehicles.Find(id);
            db.DriverVehicles.Remove(driverVehicle);
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
