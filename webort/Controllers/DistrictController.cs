using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using webort.Models;

namespace webort.Controllers
{
    public class DistrictController : Controller
    {
        private ORTEntities db = new ORTEntities();

        // GET: District
        public ActionResult GetDistrict() {
            var result = (from dis in db.District_town_.AsEnumerable()
                                  join reg in db.Region.AsEnumerable() on dis.Region equals reg.ID_Region
                                  select new DistrictRegModel
                                  {
                                      id_District = dis.ID_District,
                                      district_name = dis.District_name,
                                      password_district = dis.Password_district,
                                       regionName = reg.Region_name
                                  }).ToList();
            return View(result);
        }
        //Get: Region
        public ActionResult GetRegion() {
            var regionr = (from reg in db.Region.AsEnumerable()
                           select new RegionModel
                           {
                               id_Region = reg.ID_Region,
                               region_name = reg.Region_name
                           }).ToList();
            return View(regionr);
        }
       
        // GET: District/Create
        public ActionResult Create()
        {
            ViewBag.Region = new SelectList(db.Region, "ID_Region", "Region_name");
            return View();
        }

        // POST: District/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_District,District_name,Password_district,Region")] District_town_ district_town_)
        {
            if (ModelState.IsValid)
            {
                db.District_town_.Add(district_town_);
                db.SaveChanges();
                return RedirectToAction("GetDistrict");
            }

            ViewBag.Region = new SelectList(db.Region, "ID_Region", "Region_name", district_town_.Region);
            return View(district_town_);
        }
        // GET: District/CreateRegion
        public ActionResult CreateRegion()
        {
            return View();
        }
        //Create: Region 
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult CreateRegion([Bind(Include ="ID_Region,Region_name")]Region region)
        {
            if (ModelState.IsValid)
            {
                db.Region.Add(region);
                db.SaveChanges();
                return RedirectToAction("GetRegion");
            }
            return View(region);
        }
        //Update District/Region
        public ActionResult UpdateRegion(short? id)
        {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Region region = db.Region.Find(id);
            if (region == null)
            {
                return HttpNotFound();
            }
            return View(region);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateRegion([Bind(Include = "ID_Region,Region_name")]Region  region)
        {
            if (ModelState.IsValid)
            {
                db.Entry(region).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetRegion");
            }
           return View(region);
        }
        // GET: District/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            District_town_ district_town_ = db.District_town_.Find(id);
            if (district_town_ == null)
            {
                return HttpNotFound();
            }
            ViewBag.Region = new SelectList(db.Region, "ID_Region", "Region_name", district_town_.Region);
            return View(district_town_);
        }

        // POST: District/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_District,District_name,Password_district,Region")] District_town_ district_town_)
        {
            if (ModelState.IsValid)
            {
                db.Entry(district_town_).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetDistrict");
            }
            ViewBag.Region = new SelectList(db.Region, "ID_Region", "Region_name", district_town_.Region);
            return View(district_town_);
        }

        // GET: District/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            District_town_ district_town_ = db.District_town_.Find(id);
            if (district_town_ == null)
            {
                return HttpNotFound();
            }
            return View(district_town_);
        }

        // POST: District/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            District_town_ district_town_ = db.District_town_.Find(id);
            db.District_town_.Remove(district_town_);
            db.SaveChanges();
            return RedirectToAction("GetDistrict");
        }
        // GET: District/Region/Delete
        public ActionResult DeleteRegion(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Region region = db.Region.Find(id);
            if (region == null)
            {
                return HttpNotFound();
            }
            return View(region);
        }

        // POST: District/Region/Delete
        [HttpPost, ActionName("DeleteRegion")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRegion(short id)
        {
            Region region = db.Region.Find(id);
            db.Region.Remove(region);
            db.SaveChanges();
            return RedirectToAction("GetRegion");
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
