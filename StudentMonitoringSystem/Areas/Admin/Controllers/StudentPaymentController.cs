using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentMonitoringSystem.Models;

namespace StudentMonitoringSystem.Areas.Admin.Controllers
{
    public class StudentPaymentController : Controller
    {
        private StudentsMonitringSystemEntities db = new StudentsMonitringSystemEntities();

        // GET: Admin/StudentPayment
        public ActionResult Index()
        {
            var invoiceInformations = db.InvoiceInformations.Include(i => i.Class).Include(i => i.Student);
            return View(invoiceInformations.ToList());
        }

        // GET: Admin/StudentPayment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceInformation invoiceInformation = db.InvoiceInformations.Find(id);
            if (invoiceInformation == null)
            {
                return HttpNotFound();
            }
            return View(invoiceInformation);
        }

        // GET: Admin/StudentPayment/Create
        public ActionResult Create()
        {
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name");
            ViewBag.StudentID = new SelectList(db.Students, "ID", "FirstName");
            return View();
        }

        // POST: Admin/StudentPayment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ManageFeeViewModel mfv)
        {
            InvoiceInformation ii = new InvoiceInformation();
            PaymentInformation pi = new PaymentInformation();
            
            if (ModelState.IsValid)
            {
                ii.StudentID = mfv.StudentID;
                ii.ClassID = mfv.ClassID;
                ii.Title = mfv.Title;
                ii.Description = mfv.Description;
                ii.Date = mfv.Date;
                // here is payment information details
                //
               
                pi.Payment = mfv.Payment;
                pi.Status = "UnPaid";
                pi.Method = "Cash";
                pi.InvoiceInformation = ii.ID;

                db.InvoiceInformations.Add(ii);
                db.PaymentInformations.Add(pi);
                db.SaveChanges();
                return RedirectToAction("Create");



            }
          

            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", ii.ClassID);
            ViewBag.StudentID = new SelectList(db.Students, "ID", "FirstName", ii.StudentID);
            return View(Create());
        }

        // GET: Admin/StudentPayment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceInformation invoiceInformation = db.InvoiceInformations.Find(id);
            if (invoiceInformation == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", invoiceInformation.ClassID);
            ViewBag.StudentID = new SelectList(db.Students, "ID", "Name", invoiceInformation.StudentID);
            return View(invoiceInformation);
        }

        // POST: Admin/StudentPayment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ClassID,StudentID,Title,Description,Date")] InvoiceInformation invoiceInformation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoiceInformation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", invoiceInformation.ClassID);
            ViewBag.StudentID = new SelectList(db.Students, "ID", "StudentID", invoiceInformation.StudentID);
            return View(invoiceInformation);
        }

        // GET: Admin/StudentPayment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceInformation invoiceInformation = db.InvoiceInformations.Find(id);
            if (invoiceInformation == null)
            {
                return HttpNotFound();
            }
            return View(invoiceInformation);
        }

        // POST: Admin/StudentPayment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InvoiceInformation invoiceInformation = db.InvoiceInformations.Find(id);
            db.InvoiceInformations.Remove(invoiceInformation);
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
