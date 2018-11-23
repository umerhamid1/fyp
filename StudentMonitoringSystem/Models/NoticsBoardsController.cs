using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace StudentMonitoringSystem.Models
{
    public class NoticsBoardsController : Controller
    {
        private StudentsMonitringSystemEntities db = new StudentsMonitringSystemEntities();

        // GET: NoticsBoards
        public ActionResult Index()
        {
            var noticsBoards = db.NoticsBoards.Include(n => n.Campu);
            return View(noticsBoards.ToList());
        }

        // GET: NoticsBoards/Details/5
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

        // GET: NoticsBoards/Create
        public ActionResult Create()
        {
            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name");
            return View();
        }

        // POST: NoticsBoards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Description,Date,CampusID")] NoticsBoard noticsBoard)
        {
            if (ModelState.IsValid)
            {
                db.NoticsBoards.Add(noticsBoard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name", noticsBoard.CampusID);
            return View(noticsBoard);
        }

        // GET: NoticsBoards/Edit/5
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
            return View(noticsBoard);
        }

        // POST: NoticsBoards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Description,Date,CampusID")] NoticsBoard noticsBoard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(noticsBoard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name", noticsBoard.CampusID);
            return View(noticsBoard);
        }

        // GET: NoticsBoards/Delete/5
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

        // POST: NoticsBoards/Delete/5
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
