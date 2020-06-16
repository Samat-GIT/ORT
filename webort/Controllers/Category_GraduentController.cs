using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using webort;

namespace webort.Controllers
{
    public class Category_GraduentController : Controller
    {
        private ORTEntities db = new ORTEntities();

        // GET: Category_Graduent
        public ActionResult Index()
        {
            return View(db.Category_Graduent.ToList());
        }

        // GET: Category_Graduent/Details/5
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category_Graduent category_Graduent = db.Category_Graduent.Find(id);
            if (category_Graduent == null)
            {
                return HttpNotFound();
            }
            return View(category_Graduent);
        }

        // GET: Category_Graduent/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category_Graduent/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Category,Category_Graduent1")] Category_Graduent category_Graduent)
        {
            if (ModelState.IsValid)
            {
                db.Category_Graduent.Add(category_Graduent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category_Graduent);
        }

        // GET: Category_Graduent/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category_Graduent category_Graduent = db.Category_Graduent.Find(id);
            if (category_Graduent == null)
            {
                return HttpNotFound();
            }
            return View(category_Graduent);
        }

        // POST: Category_Graduent/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Category,Category_Graduent1")] Category_Graduent category_Graduent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category_Graduent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category_Graduent);
        }

        // GET: Category_Graduent/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category_Graduent category_Graduent = db.Category_Graduent.Find(id);
            if (category_Graduent == null)
            {
                return HttpNotFound();
            }
            return View(category_Graduent);
        }

        // POST: Category_Graduent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            Category_Graduent category_Graduent = db.Category_Graduent.Find(id);
            db.Category_Graduent.Remove(category_Graduent);
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
