using RepairShop.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace RepairShop.Controllers
{
    public class RepairOrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RepairOrders
        public ActionResult Index()
        {
            return View(db.RepairOrders.ToList());
        }

        // GET: RepairOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RepairOrder repairOrder = db.RepairOrders.Find(id);
            if (repairOrder == null)
            {
                return HttpNotFound();
            }

            return View(repairOrder);
        }

        // GET: RepairOrders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RepairOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,StartDate,EndDate,Status")] RepairOrder repairOrder)
        {
            if (ModelState.IsValid)
            {
                db.RepairOrders.Add(repairOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(repairOrder);
        }

        // GET: RepairOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RepairOrder repairOrder = db.RepairOrders.Find(id);
            if (repairOrder == null)
            {
                return HttpNotFound();
            }

            return View(repairOrder);
        }

        // POST: RepairOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,StartDate,EndDate,Status")] RepairOrder repairOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(repairOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(repairOrder);
        }

        // GET: RepairOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RepairOrder repairOrder = db.RepairOrders.Find(id);
            if (repairOrder == null)
            {
                return HttpNotFound();
            }

            return View(repairOrder);
        }

        // POST: RepairOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RepairOrder repairOrder = db.RepairOrders.Find(id);
            db.RepairOrders.Remove(repairOrder);
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
