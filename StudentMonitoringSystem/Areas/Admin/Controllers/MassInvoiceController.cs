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
    public class MassInvoiceController : Controller
    {
        private StudentsMonitringSystemEntities db = new StudentsMonitringSystemEntities();

        // GET: Admin/MassInvoice
        public ActionResult Index()
        {
            var invoiceInformations = db.InvoiceInformations.Include(i => i.Class).Include(i => i.Student);
            return View(invoiceInformations.ToList());
        }


        public PartialViewResult GetStudents(int ClassID, string Title, string Description, string Date)
        {

            //AttendanceViewModel AttendViewData = new AttendanceViewModel();
            //AttendViewData.Date = Date;
            //AttendViewData.ClassID = ClassID;
            //AttendViewData.StudentList = new List<StudentAttendence>();
            //int i = 0;
            //foreach (var temp in data)
            //{
            //    AttendViewData.StudentList.Add(new StudentAttendence());
            //    AttendViewData.StudentList[i].StudentID = temp.ID;
            //    AttendViewData.StudentList[i].Student2 = new Student();
            //    AttendViewData.StudentList[i].Student2.Name = temp.Name;
            //    i++;
            //}
            //return PartialView("_StudentData", AttendViewData);

            var data = db.Students.Where(x => x.ClassID == ClassID).ToList();

            ManageFeeViewModel mfv = new ManageFeeViewModel();
            mfv.ClassID = ClassID;
            mfv.Title = Title;
            mfv.Description = Description;
            mfv.Date = Date;
            mfv.StudentList = new List<InvoiceInformation>();

            int i = 0;
            foreach (var temp in data)
            {
                mfv.StudentList.Add(new InvoiceInformation());
                mfv.StudentList[i].StudentID = temp.ID;
                mfv.StudentList[i].Student = new Student();
                mfv.StudentList[i].Student.FirstName = temp.FirstName;
                i++;
            }

            return PartialView("_StudentData", mfv);



        }

        // GET: Admin/MassInvoice/Details/5
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

        // GET: Admin/MassInvoice/Create
        public ActionResult Create()
        {
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name");
            ViewBag.StudentID = new SelectList(db.Students, "ID", "FirstName");
            return View();
        }

        // POST: Admin/MassInvoice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ManageFeeViewModel mfv)
        {
            //if (ModelState.IsValid)
            //{
            //    db.InvoiceInformations.Add(invoiceInformation);
            //    db.SaveChanges();

            //    for (int i = 0; i < attend.StudentList.Count(); i++)
            //    {
            //        attend.StudentList[i].AttendanceID = att.ID;
            //    }
            //    db.StudentAttendences.AddRange(attend.StudentList);
            //    return RedirectToAction("Index");
            //}


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

                for (int i = 0; i < mfv.StudentList.Count(); i++)
                {
                    mfv.StudentList[i].StudentID = mfv.StudentID; // here is closed
                }
                db.InvoiceInformations.AddRange(mfv.StudentList);

                db.InvoiceInformations.Add(ii);
                db.PaymentInformations.Add(pi);


                db.SaveChanges();
                return RedirectToAction("Create");



            }
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", ii.ClassID);
            ViewBag.StudentID = new SelectList(db.Students, "ID", "StudentID", ii.StudentID);
            return View(Create());
        }

        // GET: Admin/MassInvoice/Edit/5
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
            ViewBag.StudentID = new SelectList(db.Students, "ID", "StudentID", invoiceInformation.StudentID);
            return View(invoiceInformation);
        }

        // POST: Admin/MassInvoice/Edit/5
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

        // GET: Admin/MassInvoice/Delete/5
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

        // POST: Admin/MassInvoice/Delete/5
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
