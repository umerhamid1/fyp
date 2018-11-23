using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Win32;
using StudentMonitoringSystem.Models;

namespace StudentMonitoringSystem.Areas.Admin.Controllers
{
    public class StudentssController : Controller
    {
        private StudentsMonitringSystemEntities db = new StudentsMonitringSystemEntities();

        // GET: Admin/Studentss
        public ActionResult Index()
        {
            var studentsses = db.Studentsses.Include(s => s.ClassAndSectionDetail).Include(s => s.Parentss).Include(s => s.User).Where(s => s.IsApproved =="Pending");
            return View(studentsses.ToList());
        }

        // GET: Admin/Studentss/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Studentss studentss = db.Studentsses.Find(id);
            if (studentss == null)
            {
                return HttpNotFound();
            }
            return View(studentss);
        }

        // GET: Admin/Studentss/Create
        public ActionResult Create()
        {
            //ViewBag.ClassID = new SelectList(db.ClassAndSectionDetails, "ID", "CampusID");
            //ViewBag.ParentsID = new SelectList(db.Parentsses, "ID", "CNIC");
          //  ViewBag.UserID = new SelectList(db.Users, "ID", "FirstName");
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name");
            return View();
        }

        // POST: Admin/Studentss/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( StudentViewModel svm)
        {
            if (ModelState.IsValid)
            {
                string camp = "na-100-";
                User u = new User();

                u.FirstName = svm.FirstName;
                u.LastName = svm.LastName;
                u.DOB = svm.DOB;
                u.Gender = svm.Gender;
                u.Address = svm.Address;
                u.Nationality = svm.Nationality;
                u.Phone = svm.Phone;
                u.LoginID = camp + "-" + svm.U_ID;
                u.Password = svm.Password;
                u.CampusID = "";// here is when user login so gets it
                u.RoleID = 3;
                u.Year = DateTime.Now.Year.ToString();
                db.Users.Add(u);
                db.SaveChanges();

                Studentss s = new Studentss();
                s.UserID = svm.U_ID;
                s.ClassID = svm.ClassID;
                s.IsApproved = "Pending";
                

                Parentss p = new Parentss();


                u.FirstName = svm.P_FirstName;
                u.LastName = svm.P_LastName;
                u.DOB = svm.P_DOB;
                u.Gender = svm.P_Gender;
                u.Address = svm.P_Address;
                u.Nationality = svm.P_Nationality;
                u.Phone = svm.P_Phone;
                u.LoginID = camp + "-" + svm.U_ID;
                u.Password = svm.Password;
                u.CampusID = "";// here is when user login so gets it
                u.RoleID = 3;
                u.Year = DateTime.Now.Year.ToString();
                p.CNIC = svm.CNIC;
                p.Occupation = svm.Occupation;
                db.Users.Add(u);
                db.SaveChanges();

                db.Parentsses.Add(p);
                db.SaveChanges();


                // db.Studentsses.Add(studentss);
                // db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name",svm.ClassID);
           // ViewBag.ClassID = new SelectList(db.ClassAndSectionDetails, "ID", "CampusID", svm.ClassID);
         //   ViewBag.ParentsID = new SelectList(db.Parentsses, "ID", "CNIC", svm.ParentsID);
        //    ViewBag.UserID = new SelectList(db.Users, "ID", "FirstName", studentss.UserID);
            return View(svm);
        }

        // GET: Admin/Studentss/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Studentss studentss = db.Studentsses.Find(id);
            if (studentss == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassID = new SelectList(db.ClassAndSectionDetails, "ID", "CampusID", studentss.ClassID);
            ViewBag.ParentsID = new SelectList(db.Parentsses, "ID", "CNIC", studentss.ParentsID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "FirstName", studentss.UserID);
            return View(studentss);
        }

        // POST: Admin/Studentss/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,ClassID,ClassSectionID,IsApproved,ParentsID")] Studentss studentss)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentss).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.ClassAndSectionDetails, "ID", "CampusID", studentss.ClassID);
            ViewBag.ParentsID = new SelectList(db.Parentsses, "ID", "CNIC", studentss.ParentsID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "FirstName", studentss.UserID);
            return View(studentss);
        }

        // GET: Admin/Studentss/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Studentss studentss = db.Studentsses.Find(id);
            if (studentss == null)
            {
                return HttpNotFound();
            }
            return View(studentss);
        }

        // POST: Admin/Studentss/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Studentss studentss = db.Studentsses.Find(id);
            db.Studentsses.Remove(studentss);
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
