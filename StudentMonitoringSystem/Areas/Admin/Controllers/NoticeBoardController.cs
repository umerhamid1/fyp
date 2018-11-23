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
    public class NoticeBoardController : Controller
    {
        private StudentsMonitringSystemEntities db = new StudentsMonitringSystemEntities();

        // GET: Admin/NoticeBoard
        public ActionResult Index()
        {
            var noticsBoards = db.NoticsBoards.Include(n => n.Campu).Include(n => n.NoticeBoardUserType);
            return View(noticsBoards.ToList());
        }

        // GET: Admin/NoticeBoard/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NoticsBoard noticsBoard = db.NoticsBoards.Find(id);
            if (noticsBoard == null)
            {
                return HttpNotFound();
            }
            return View(noticsBoard);
        }

        // GET: Admin/NoticeBoard/Create
        public ActionResult Create()
        {
            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name");
            ViewBag.UserType = new SelectList(db.NoticeBoardUserTypes, "ID", "userType");
            return View();
        }

        // POST: Admin/NoticeBoard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Description,Date,CampusID,UserType")] NoticsBoard noticsBoard)
        {
            if (ModelState.IsValid)
            {
                db.NoticsBoards.Add(noticsBoard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name", noticsBoard.CampusID);
            ViewBag.UserType = new SelectList(db.NoticeBoardUserTypes, "ID", "userType", noticsBoard.UserType);
            return View(noticsBoard);
        }

        // GET: Admin/NoticeBoard/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NoticsBoard noticsBoard = db.NoticsBoards.Find(id);
            if (noticsBoard == null)
            {
                return HttpNotFound();
            }
            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name", noticsBoard.CampusID);
            ViewBag.UserType = new SelectList(db.NoticeBoardUserTypes, "ID", "userType", noticsBoard.UserType);
            return View(noticsBoard);
        }

        // POST: Admin/NoticeBoard/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Description,Date,CampusID,UserType")] NoticsBoard noticsBoard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(noticsBoard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name", noticsBoard.CampusID);
            ViewBag.UserType = new SelectList(db.NoticeBoardUserTypes, "ID", "userType", noticsBoard.UserType);
            return View(noticsBoard);
        }

        // GET: Admin/NoticeBoard/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NoticsBoard noticsBoard = db.NoticsBoards.Find(id);
            if (noticsBoard == null)
            {
                return HttpNotFound();
            }
            return View(noticsBoard);
        }

        // POST: Admin/NoticeBoard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NoticsBoard noticsBoard = db.NoticsBoards.Find(id);
            db.NoticsBoards.Remove(noticsBoard);
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
