using RepairShop.DAL;
using RepairShop.Models;
using RepairShop.View_Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace RepairShop.Controllers
{
    public class RepairOrdersController : Controller
    {
        private readonly ShopDbContext db = new ShopDbContext();
        
        // GET: RepairOrders
        public ActionResult Index()
        {
            return View(db.RepairOrders.Include(x => x.Customer).ToList());
        }

        // GET: RepairOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RepairOrder repairOrder = db.RepairOrders.Include(x => x.Customer)
                                                     .SingleOrDefault(x => x.ID == id);
            if (repairOrder == null)
            {
                return HttpNotFound();
            }

            return View(repairOrder);
        }

        // GET: RepairOrders/Create
        public ActionResult Create()
        {
            RepairOrdersViewModel model = new RepairOrdersViewModel
            {
                Customers = db.Customers.ToList()
            };
            return View(model);
        }

        // POST: RepairOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RepairOrdersViewModel repairOrderVM)
        {
            if (ModelState.IsValid)
            {
                repairOrderVM.RepairOrder.Customer = db.Customers.Find(repairOrderVM.RepairOrder.Customer.ID);
                db.RepairOrders.Add(repairOrderVM.RepairOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(repairOrderVM);
        }

        // GET: RepairOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RepairOrder repairOrder = db.RepairOrders.Include(x => x.Customer)
                                                     .SingleOrDefault(x => x.ID == id);
            if (repairOrder == null)
            {
                return HttpNotFound();
            }

            RepairOrdersViewModel repairOrderVM = new RepairOrdersViewModel
            {
                Customers = db.Customers.ToList(),
                RepairOrder = repairOrder,
            };

            return View(repairOrderVM);
        }

        //[Bind(Include = "ID,StartDate,EndDate,Status")]

        // POST: RepairOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RepairOrdersViewModel repairOrderVM)
        {
            if (ModelState.IsValid)
            {
                RepairOrder repairOrder = db.RepairOrders.Include(p => p.Customer)
                                                         .Single(p => p.ID == repairOrderVM.RepairOrder.ID);

                if (repairOrderVM.RepairOrder.Customer.ID != repairOrder.Customer.ID)
                {
                    db.Customers.Attach(repairOrderVM.RepairOrder.Customer);
                    repairOrder.Customer = repairOrderVM.RepairOrder.Customer;
                }

                db.Entry(repairOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(repairOrderVM);
        }

        // GET: RepairOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RepairOrder repairOrder = db.RepairOrders.Include(x => x.Customer)
                                                     .SingleOrDefault(x => x.ID == id);
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
