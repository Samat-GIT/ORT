using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using webort.Models;
using webort.Repository;

namespace webort.Controllers
{
    public class SchoolsController : Controller
    {
        private ORTEntities db = new ORTEntities();
        [HttpGet]
        public ViewResult ViewResult() {
            SchoolLocRepository schoolLoc = new SchoolLocRepository();
            ModelState.Clear();
            var school = db.School.Include(s => s.District_town_);
            return View(schoolLoc.GetSchools());
        }
        // GET: Schools
        public ActionResult main()
        {
            SchoolLocRepository schoolLoc = new SchoolLocRepository();
            ModelState.Clear();
            var school = db.School.Include(s => s.District_town_);
            return View(schoolLoc.GetSchools());
        }

        // POST: Schools/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        public ActionResult CreateSchool()
        {
            ViewBag.District = new SelectList(db.District_town_, "ID_District", "District_name");
            return View();
        }
        [HttpPost]
        public ActionResult CreateSchool(SchoolModel school)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SchoolLocRepository SchoolRepo = new SchoolLocRepository();
                    if (SchoolRepo.AddSchoolLoc(school))
                    {
                        ViewBag.Message = "Данные о школе успешно добавлен";
                    }
                    return RedirectToAction("main");
                }
                return RedirectToAction("main");
            }
            catch
            {
                return View();
            }
        }
        // GET: Schools/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = db.School.Find(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            ViewBag.District = new SelectList(db.District_town_, "ID_District", "District_name", school.District);
            return View(school);
        }

        // POST: Schools/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_School,School_name,Postal_zip,Code_of_school,Password_school,District,LocalitiName,LocalityAddress")] School school)
        {
            if (ModelState.IsValid)
            {
                db.Entry(school).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("main");
            }
            ViewBag.District = new SelectList(db.District_town_, "ID_District", "District_name", school.District);
            //   ViewBag.locality = new SelectList(db.Locality, "ID_Locality", "Name", school.locality);
            return View(school);
        }
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = db.School.Find(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }

        // POST: Schools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            School school = db.School.Find(id);
            db.School.Remove(school);
            db.SaveChanges();
            return RedirectToAction("main");
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
