using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RepairShop.DAL;
using RepairShop.Models;

namespace RepairShop.Controllers
{
    public class RepairmenController : Controller
    {
        private ShopDbContext db = new ShopDbContext();

        // GET: Repairmen
        public ActionResult Index()
        {
            return View(db.Repairmen.ToList());
        }

        // GET: Repairmen/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repairman repairman = db.Repairmen.Find(id);
            if (repairman == null)
            {
                return HttpNotFound();
            }
            return View(repairman);
        }

        // GET: Repairmen/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Repairmen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Wage,FirstName,LastName")] Repairman repairman)
        {
            if (ModelState.IsValid)
            {
                db.Repairmen.Add(repairman);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(repairman);
        }

        // GET: Repairmen/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repairman repairman = db.Repairmen.Find(id);
            if (repairman == null)
            {
                return HttpNotFound();
            }
            return View(repairman);
        }

        // POST: Repairmen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Wage,FirstName,LastName")] Repairman repairman)
        {
            if (ModelState.IsValid)
            {
                db.Entry(repairman).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(repairman);
        }

        // GET: Repairmen/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repairman repairman = db.Repairmen.Find(id);
            if (repairman == null)
            {
                return HttpNotFound();
            }
            return View(repairman);
        }

        // POST: Repairmen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Repairman repairman = db.Repairmen.Find(id);
            db.Repairmen.Remove(repairman);
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
