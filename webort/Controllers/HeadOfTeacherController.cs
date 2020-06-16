using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using webort;
using webort.Models;
using webort.Repository;

namespace webort.Controllers
{
    public class HeadOfTeacherController : Controller
    {
        private ORTEntities db = new ORTEntities();

        // GET: HeadOfTeacher
        public ActionResult GetHeadOfTeacher() {
            var Teacher = (from teach in db.Employees.AsEnumerable()
                           join pos in db.Position.AsEnumerable() on teach.Position equals pos.ID_Position
                           join al in db.Access_level.AsEnumerable() on teach.Access_level equals al.ID_access_level
                           join sch in db.School.AsEnumerable() on teach.School equals sch.ID_School
                         //  join fe in db.Fired_Employee.AsEnumerable() on teach.ID_Employee equals fe.ID_Employee
                           where al.Access_level1 == ("Директор")
                           select new HeadOfTeacherModel
                           {
                               id_HeadOfTeacher = teach.ID_Employee,
                               surname = teach.Surname,
                               first_name = teach.First_name,
                               third_Name = teach.Third_Name,
                               birth_date = teach.Birth_date,
                               gender = teach.Gender,
                               phone_number = teach.Phone_number,
                               inn_passport = teach.INN_passport,
                               address = teach.Address,
                               email = teach.Email,
                               FiredEmpl = teach.Fired,
                               date_of_appointment = teach.Date_of_appointment,
                               position = pos.Position_Name,
                               access_level = al.Access_level1,
                               schoolName = sch.School_name,
                               schoolCode = sch.Code_of_school,
                               //date_leavingFired = fe.Date_leavingFierd,
                           }).ToList();
            return View(Teacher);
        }
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.Access_level1).Include(e => e.Authorization).Include(e => e.District_town_).Include(e => e.Position1).Include(e => e.School1);
            return View(employees.ToList());
        }

        // GET: HeadOfTeacher/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = db.Employees.Find(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            return View(employees);
        }

        // GET: HeadOfTeacher/Create
        public ActionResult Create()
        {
            ViewBag.Access_level = new SelectList(db.Access_level, "ID_access_level", "Access_level1");
           ViewBag.Authotization = new SelectList(db.Authorization, "id_Autorization", "Login");
           ViewBag.District = new SelectList(db.District_town_, "ID_District", "District_name");
            ViewBag.Position = new SelectList(db.Position, "ID_Position", "Position_Name");
            ViewBag.School = new SelectList(db.School, "ID_School", "School_name");
            return View();
        }
        //public ActionResult CreateHeadOfTeacher() {
        //    // ViewBag.Authotization = new SelectList(db.Authorization, "id_Autorization", "Login");

        //    ViewBag.Access_level = new SelectList(db.Access_level, "ID_access_level", "Access_level1");
        //    ViewBag.Position = new SelectList(db.Position, "ID_Position", "Position_Name");
        //    ViewBag.School = new SelectList(db.School, "ID_School", "School_name");
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CreateHeadOfTeacher(HeadOfTeacherModel teacherModel) {
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            HeadOfTeacherRepository teacherRepository = new HeadOfTeacherRepository();
        //            if (teacherRepository.AddHeadOfSchool(teacherModel))
        //            {
        //                ViewBag.Message = "Данные о школе успешно добавлен";
        //            }
        //            return RedirectToAction("GetHeadOfTeacher");
        //        }
        //        return RedirectToAction("GetHeadOfTeacher");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}


        // POST: HeadOfTeacher/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Employee,Surname,First_name,Third_Name,Birth_date,Gender,Phone_number,INN_passport,Address,Email,Date_of_appointment,Fired,Position,Access_level,District,School,Authotization")] Employees employees)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employees);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Access_level = new SelectList(db.Access_level, "ID_access_level", "Access_level1", employees.Access_level);
            ViewBag.Authotization = new SelectList(db.Authorization, "id_Autorization", "Login", employees.Authotization);
            ViewBag.District = new SelectList(db.District_town_, "ID_District", "District_name", employees.District);
            ViewBag.Position = new SelectList(db.Position, "ID_Position", "Position_Name", employees.Position);
            ViewBag.School = new SelectList(db.School, "ID_School", "School_name", employees.School);
            return View(employees);
        }

        // GET: HeadOfTeacher/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = db.Employees.Find(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            ViewBag.Access_level = new SelectList(db.Access_level, "ID_access_level", "Access_level1", employees.Access_level);
            ViewBag.Authotization = new SelectList(db.Authorization, "id_Autorization", "Login", employees.Authotization);
            ViewBag.District = new SelectList(db.District_town_, "ID_District", "District_name", employees.District);
            ViewBag.Position = new SelectList(db.Position, "ID_Position", "Position_Name", employees.Position);
            ViewBag.School = new SelectList(db.School, "ID_School", "School_name", employees.School);
            return View(employees);
        }

        // POST: HeadOfTeacher/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Employee,Surname,First_name,Third_Name,Birth_date,Gender,Phone_number,INN_passport,Address,Email,Date_of_appointment,Fired,Position,Access_level,District,School,Authotization")] Employees employees)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employees).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Access_level = new SelectList(db.Access_level, "ID_access_level", "Access_level1", employees.Access_level);
            ViewBag.Authotization = new SelectList(db.Authorization, "id_Autorization", "Login", employees.Authotization);
            ViewBag.District = new SelectList(db.District_town_, "ID_District", "District_name", employees.District);
            ViewBag.Position = new SelectList(db.Position, "ID_Position", "Position_Name", employees.Position);
            ViewBag.School = new SelectList(db.School, "ID_School", "School_name", employees.School);
            return View(employees);
        }

        // GET: HeadOfTeacher/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = db.Employees.Find(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            return View(employees);
        }

        // POST: HeadOfTeacher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employees employees = db.Employees.Find(id);
            db.Employees.Remove(employees);
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
