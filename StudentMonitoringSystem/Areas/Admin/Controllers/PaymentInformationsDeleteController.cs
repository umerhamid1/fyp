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
    public class PaymentInformationsDeleteController : Controller
    {
        private StudentsMonitringSystemEntities db = new StudentsMonitringSystemEntities();

        // GET: Admin/PaymentInformationsDelete
        public ActionResult Index()
        {
            var paymentInformations = db.PaymentInformations.Include(p => p.InvoiceInformation1);
            return View(paymentInformations.ToList());
        }

        // GET: Admin/PaymentInformationsDelete/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentInformation paymentInformation = db.PaymentInformations.Find(id);
            if (paymentInformation == null)
            {
                return HttpNotFound();
            }
            return View(paymentInformation);
        }

        // GET: Admin/PaymentInformationsDelete/Create
        public ActionResult Create()
        {
            ViewBag.InvoiceInformation = new SelectList(db.InvoiceInformations, "ID", "Title");
            return View();
        }

        // POST: Admin/PaymentInformationsDelete/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Payment,Status,Method,InvoiceInformation")] PaymentInformation paymentInformation)
        {
            if (ModelState.IsValid)
            {
                db.PaymentInformations.Add(paymentInformation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InvoiceInformation = new SelectList(db.InvoiceInformations, "ID", "Title", paymentInformation.InvoiceInformation);
            return View(paymentInformation);
        }

        // GET: Admin/PaymentInformationsDelete/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentInformation paymentInformation = db.PaymentInformations.Find(id);
            if (paymentInformation == null)
            {
                return HttpNotFound();
            }
            ViewBag.InvoiceInformation = new SelectList(db.InvoiceInformations, "ID", "Title", paymentInformation.InvoiceInformation);
            return View(paymentInformation);
        }

        // POST: Admin/PaymentInformationsDelete/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Payment,Status,Method,InvoiceInformation")] PaymentInformation paymentInformation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paymentInformation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InvoiceInformation = new SelectList(db.InvoiceInformations, "ID", "Title", paymentInformation.InvoiceInformation);
            return View(paymentInformation);
        }

        // GET: Admin/PaymentInformationsDelete/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentInformation paymentInformation = db.PaymentInformations.Find(id);
            if (paymentInformation == null)
            {
                return HttpNotFound();
            }
            return View(paymentInformation);
        }

        // POST: Admin/PaymentInformationsDelete/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PaymentInformation paymentInformation = db.PaymentInformations.Find(id);
            db.PaymentInformations.Remove(paymentInformation);
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
