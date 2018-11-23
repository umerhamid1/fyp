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
    public class StudentsController : Controller
    {
        private StudentsMonitringSystemEntities db = new StudentsMonitringSystemEntities();


     
        public ActionResult NavTab()
        {
            return View();
        }

        // GET: Admin/Students
        public ActionResult Index()
        {
           // var students = db.Students.Include(s => s.Campu).Include(s => s.Class).Include(s => s.Parent);


            var urls = db.Students.ToList().Where(x => x.IsApproved == "Pending").ToList();
            return View(urls);
            //return View(students.ToList());
        }

        // GET: Admin/Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }


        // GET: Admin/Students/Edit/5
        public ActionResult Decision(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);

            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name", student.CampusID);
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", student.ClassID);
            ViewBag.ParentsID = new SelectList(db.Parents, "ID", "ParentsID", student.ParentsID);
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name", student.SectionID);
            return View(student);
        }

        // POST: Admin/Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Decision(Student student)
        {
            if (ModelState.IsValid)
            {
               // student.Password = "iqra123";
                //student.AdmissionYear = DateTime.Now.Year.ToString();

                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("NavTab");
            }
            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name", student.CampusID);
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", student.ClassID);
            ViewBag.ParentsID = new SelectList(db.Parents, "ID", "ParentsID", student.ParentsID);
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name", student.SectionID);

            return View(student);
        }




        // GET: Admin/Students/Create
        public ActionResult Create()
        {
            AddmissionViewModel avm = new AddmissionViewModel();
            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name");
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name");
            ViewBag.ParentsID = new SelectList(db.Parents, "ID", "ParentsID");
            return View(avm);
        }

        // POST: Admin/Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( AddmissionViewModel avm)

        {
            Student s = new Student();

            s.Password = "iqra123";
            s.AdmissionYear = DateTime.Now.Year.ToString();
            s.IsApproved = "Pending";
            s.CampusID = avm.CampusID;


            Parent p = new Parent();
            p.Password = "iqra123";
            s.ParentsID = p.ID;
            p.CampusID = s.CampusID;

           if(ModelState.IsValid)
            { 
                s.CampusID = avm.CampusID;
                s.FirstName = avm.FirstName;
                s.LastName = avm.LastName;
                s.DOB = avm.DOB;
                s.Gender = avm.Gender;
                s.Address = avm.Address;
                s.Nationality = avm.Nationality;
                
                s.PhoneNo = avm.PhoneNo;
               
                s.ClassID = avm.ClassID;
                s.SectionID = 1;
                
               
                


                // here is parents start

                

                p.PhoneNo = avm.P_PhoneNo;
                p.Occupation = avm.P_Occupation;
                p.Email = avm.P_Email;
              
                
                p.FirstName = avm.P_FirstName;
                p.LastName = avm.P_LastName;

                // student parents id

               

                db.Students.Add(s);
                db.Parents.Add(p);
                db.SaveChanges();
                return RedirectToAction("NavTab");
            }

            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name", avm.CampusID);
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", avm.ClassID);
            ViewBag.ParentsID = new SelectList(db.Parents, "ID", "ParentsID", avm.P_ParentsID);
            return View(avm);
        }

        // GET: Admin/Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name", student.CampusID);
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", student.ClassID);
            ViewBag.ParentsID = new SelectList(db.Parents, "ID", "FirstName", student.ParentsID);
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name", student.SectionID);
            return View(student);
        }

        // POST: Admin/Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(/*[Bind(Include = "ID,StudentID,FirstName,LastName,DOB,Gender,Address,Nationality,Password,PhoneNo,ParentsID,ClassID,SectionID,AdmissionYear,IsApproved,CampusID")] */Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("NavTab");
            }
            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name", student.CampusID);
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", student.ClassID);
            ViewBag.ParentsID = new SelectList(db.Parents, "ID", "ParentsID", student.ParentsID);
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name", student.SectionID);
            return View(student);
        }

        // GET: Admin/Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Admin/Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("NavTab");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }




       


        public ActionResult RejectStudent()
        {
            var urls = db.Students.ToList().Where(x => x.IsApproved == "Reject").ToList();
           // return RedirectToAction("NavTab");

            return View(urls);
        }


        public ActionResult ApprovedStudent()
        {
            var urls = db.Students.ToList().Where(x => x.IsApproved == "Approved").ToList();
            return View(urls);
            
        }

        public ActionResult CampusStudent()
        {

            var urls = db.Students.ToList().Where(x => x.IsApproved == "Approved")/*.Where(y => y.ClassID == 45)*/.ToList();
            return View(urls);
       
        }

        public ActionResult EditCampusStudent(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name", student.CampusID);
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", student.ClassID);
            ViewBag.ParentsID = new SelectList(db.Parents, "ID", "FirstName", student.ParentsID);
            return View(student);
        }

        // POST: Admin/Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCampusStudent([Bind(Include = "ID,StudentID,FirstName,LastName,DOB,Gender,Address,Nationality,Password,PhoneNo,ParentsID,ClassID,SectionID,AdmissionYear,IsApproved,CampusID")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("CampusStudent");
            }
            ViewBag.CampusID = new SelectList(db.Campus, "Code", "Name", student.CampusID);
            ViewBag.ClassID = new SelectList(db.Classes, "ID", "Name", student.ClassID);
            ViewBag.ParentsID = new SelectList(db.Parents, "ID", "ParentsID", student.ParentsID);
            return View(student);
        }


        public JsonResult DeleteStudentRecord(int StudentId)
        {
            bool result = false;
            //tblStudent Stu = db.tblStudents.SingleOrDefault(x => x.IsDeleted == false && x.StudentId == StudentId);

            var q = db.Students.SingleOrDefault(x => x.ID == StudentId);
            if (q != null)
            {

                db.SaveChanges();
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
