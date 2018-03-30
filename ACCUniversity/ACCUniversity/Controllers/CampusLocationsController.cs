using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ACCUniversity.DAL;
using ACCUniversity.Models;

namespace ACCUniversity.Controllers
{
    public class CampusLocationsController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: CampusLocations
        public ActionResult Index()
        {
            return View(db.CampusLocations.ToList());
        }

        // GET: CampusLocations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CampusLocations campusLocations = db.CampusLocations.Find(id);
            if (campusLocations == null)
            {
                return HttpNotFound();
            }
            return View(campusLocations);
        }

        // GET: CampusLocations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CampusLocations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "campusId,CampusCity")] CampusLocations campusLocations)
        {
            if (ModelState.IsValid)
            {
                db.CampusLocations.Add(campusLocations);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(campusLocations);
        }

        // GET: CampusLocations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CampusLocations campusLocations = db.CampusLocations.Find(id);
            if (campusLocations == null)
            {
                return HttpNotFound();
            }
            return View(campusLocations);
        }

        // POST: CampusLocations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "campusId,CampusCity")] CampusLocations campusLocations)
        {
            if (ModelState.IsValid)
            {
                db.Entry(campusLocations).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(campusLocations);
        }

        // GET: CampusLocations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CampusLocations campusLocations = db.CampusLocations.Find(id);
            if (campusLocations == null)
            {
                return HttpNotFound();
            }
            return View(campusLocations);
        }

        // POST: CampusLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CampusLocations campusLocations = db.CampusLocations.Find(id);
            db.CampusLocations.Remove(campusLocations);
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
