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
    public class ParentsController : Controller
    {
        private StudentsMonitringSystemEntities db = new StudentsMonitringSystemEntities();

        // GET: Admin/Parents
        public ActionResult Index()
        {
        //    var query = (from c in db.Parents
        //        join cn in db.Students on c.ID equals cn.ParentsID
        //        where (c.ID == cn.ParentsID) && (cn.IsApproved == "Approved")
        //        select c
        //});
        //{
        //   ID= c.ParentsID,
        //    firstName=c.FirstName,
        //    Lastname=c.LastName,
        //    email=c.Email,
        //   occupation= c.Occupation,
        //   password= c.Password,
        //   campusID= c.CampusID,
        //   phone = c.PhoneNo
        //}).ToList();


        //     db.Parents = query.ToList();


         return View(db.Parents.ToList());
        }

        //public ActionResult check()
        //{

        //    parentStudentViewModel p = new parentStudentViewModel();

        //    p.parentlist = (from c in db.Parents
        //                    join cn in db.Students on c.ID equals cn.ParentsID
        //                    where (c.ID == cn.ParentsID) && (cn.IsApproved == "Approved")
        //                    select new 
        //                    {
        //                        //c.ParentsID,
        //                        //c.FirstName,
        //                        //c.LastName,
        //                        //c.Email,
        //                        //c.Occupation,
        //                        //c.Password,
        //                        //c.CampusID,
        //                        //c.PhoneNo

        //                    }).ToList();

            
        //    return view(p);
        //}

        // GET: Admin/Parents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parent parent = db.Parents.Find(id);
            if (parent == null)
            {
                return HttpNotFound();
            }
            return View(parent);
        }

        //// GET: Admin/Parents/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Admin/Parents/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,ParentsID,PhoneNo,Occupation,Email,Password,CampusID,FirstName,LastName")] Parent parent)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Parents.Add(parent);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(parent);
        //}

        // GET: Admin/Parents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parent parent = db.Parents.Find(id);
            if (parent == null)
            {
                return HttpNotFound();
            }
            return View(parent);
        }

        // POST: Admin/Parents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ParentsID,PhoneNo,Occupation,Email,Password,CampusID,FirstName,LastName")] Parent parent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(parent);
        }

        // GET: Admin/Parents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parent parent = db.Parents.Find(id);
            if (parent == null)
            {
                return HttpNotFound();
            }
            return View(parent);
        }

        // POST: Admin/Parents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {

            
            Parent parent = db.Parents.Find(id);
            db.Parents.Remove(parent);
            db.SaveChanges();
            return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Msg"] = "you can't delete this parents's data, 1st delete his children then you can delete this record ";
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
