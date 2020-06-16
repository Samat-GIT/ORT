using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using webort;
using webort.Models;

namespace webort.Controllers
{
    public class SubjectsController : Controller
    {
        private ORTEntities db = new ORTEntities();

        // GET: Subjects
        public ActionResult main() {
            var result = (from c in db.Subject
                          select new SubjectsModel
                          {
                              id_subject = c.ID_Subject,
                              subject_name = c.Subject_name,
                              subject_Price = c.Subject_Price
                          }).ToList();
            return View(result);
        }
        //public static string DecodeFromUtf8(this string utf8String)
        //{
        //    // copy the string as UTF-8 bytes.
        //    byte[] utf8Bytes = new byte[utf8String.Length];
        //    for (int i = 0; i < utf8String.Length; ++i)
        //    {
        //        //Debug.Assert( 0 <= utf8String[i] && utf8String[i] <= 255, "the char must be in byte's range");
        //        utf8Bytes[i] = (byte)utf8String[i];
        //    }

        //    return Encoding.UTF8.GetString(utf8Bytes, 0, utf8Bytes.Length);
        //}

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Subject,Subject_name,Subject_Price")] Subject subject)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    db.Subject.Add(subject);
                    db.SaveChanges();
                    return RedirectToAction("main");
                }
                catch (Exception ex) {
                    ViewBag.message = ex;
                }
            }

            return View(subject);
        }

        // GET: Subjects/Edit/5
        public ActionResult Edit(byte? id)
        {
            Encoding unicode = Encoding.Unicode; 

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Subject subject = db.Subject.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            byte[] utf8Bytes = new byte[subject.Subject_name.Length];
            for (int i = 0; i < subject.Subject_name.Length; i++)
            {
                //Debug.Assert( 0 <= utf8String[i] && utf8String[i] <= 255, "the char must be in byte's range");
                utf8Bytes[i] = (byte)subject.Subject_name[i];
            }

            Encoding.UTF8.GetString(utf8Bytes, 0, utf8Bytes.Length);
            //byte[] unicodeBytes = unicode.GetBytes(subject.Subject_name);
            //DecodeFromUtf8(subject.Subject_name);

            return View(subject);
        }

        // POST: Subjects/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Subject,Subject_name,Subject_Price")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subject).State = EntityState.Modified;
                db.SaveChanges();
               return RedirectToAction("main");
               // return RedirectToAction("Edit");
            }
            byte[] utf8Bytes = new byte[subject.Subject_name.Length];
            for (int i = 0; i < subject.Subject_name.Length; i++)
            {
                //Debug.Assert( 0 <= utf8String[i] && utf8String[i] <= 255, "the char must be in byte's range");
                utf8Bytes[i] = (byte)subject.Subject_name[i];
            }

            Encoding.UTF8.GetString(utf8Bytes, 0, utf8Bytes.Length);
            //Encoding unicode = Encoding.Unicode;
            //byte[] unicodeBytes = unicode.GetBytes(subject.Subject_name);
            return View(subject);
        }

        // GET: Subjects/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = db.Subject.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            try
            {
                Subject subject = db.Subject.Find(id);
                db.Subject.Remove(subject);
                db.SaveChanges();
                return RedirectToAction("main");
            }catch
            {
                return HttpNotFound();
              
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
