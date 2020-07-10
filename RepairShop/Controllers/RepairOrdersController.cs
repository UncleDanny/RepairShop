using RepairShop.DAL;
using RepairShop.Models;
using RepairShop.View_Models;
using System.Collections.Generic;
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
            return View(db.RepairOrders.Include(x => x.Customer).Include(x => x.Repairman).ToList());
        }

        // GET: RepairOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RepairOrder repairOrder = db.RepairOrders.Include(x => x.Customer)
                                                     .Include(x => x.Repairman)
                                                     .Include(x => x.Parts)
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
                Customers = db.Customers.ToList(),
                Repairmen = db.Repairmen.ToList(),
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
                repairOrderVM.RepairOrder.Repairman = db.Repairmen.Find(repairOrderVM.RepairOrder.Repairman.ID);
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
                                                     .Include(x => x.Repairman)
                                                     .SingleOrDefault(x => x.ID == id);
            if (repairOrder == null)
            {
                return HttpNotFound();
            }

            RepairOrdersViewModel repairOrderVM = new RepairOrdersViewModel
            {
                Customers = db.Customers.ToList(),
                Repairmen = db.Repairmen.ToList(),
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
                                                         .Include(p => p.Repairman)
                                                         .Single(p => p.ID == repairOrderVM.RepairOrder.ID);

                if (repairOrderVM.RepairOrder.Customer.ID != repairOrder.Customer.ID)
                {
                    db.Customers.Attach(repairOrderVM.RepairOrder.Customer);
                    repairOrder.Customer = repairOrderVM.RepairOrder.Customer;
                }

                if (repairOrderVM.RepairOrder.Repairman.ID != repairOrder.Repairman.ID)
                {
                    db.Repairmen.Attach(repairOrderVM.RepairOrder.Repairman);
                    repairOrder.Repairman = repairOrderVM.RepairOrder.Repairman;
                }

                repairOrder.RepairDescription = repairOrderVM.RepairOrder.RepairDescription;

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
                                                     .Include(x => x.Repairman)
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


        // GET: RepairOrders/Parts/5
        public ActionResult Parts(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RepairOrder repairOrder = db.RepairOrders.Include(x => x.Parts)
                                                     .SingleOrDefault(x => x.ID == id);
            if (repairOrder == null)
            {
                return HttpNotFound();
            }

            RepairOrderPartsViewModel repairOrderPartsVM = new RepairOrderPartsViewModel
            {
                Parts = db.Parts.ToList(),
                RepairOrder = repairOrder,
            };

            return View(repairOrderPartsVM);
        }

        [HttpPost]
        public ActionResult Parts(RepairOrderPartsViewModel repairOrderPartsVM, int id, int selectedID, bool add)
        {
            RepairOrder repairOrder = db.RepairOrders.Include(x => x.Parts)
                                         .SingleOrDefault(x => x.ID == id);
            repairOrderPartsVM.RepairOrder = repairOrder;
            repairOrderPartsVM.Parts = db.Parts.ToList();

            if (add)
            {
                if (!repairOrderPartsVM.RepairOrder.Parts.Contains(db.Parts.Single(x => x.ID == selectedID)))
                {
                    repairOrderPartsVM.RepairOrder.Parts.Add(db.Parts.Single(x => x.ID == selectedID));
                }
                else
                {
                    repairOrderPartsVM.RepairOrder.Parts.Single(x => x.ID == selectedID).Count++;
                }
            }
            else
            {
                if (repairOrderPartsVM.RepairOrder.Parts.Single(x => x.ID == selectedID).Count == 1)
                {
                    repairOrderPartsVM.RepairOrder.Parts.Remove(db.Parts.Single(x => x.ID == selectedID));
                }
                else
                {
                    repairOrderPartsVM.RepairOrder.Parts.Single(x => x.ID == selectedID).Count--;
                }
            }

            repairOrder.Parts = repairOrderPartsVM.RepairOrder.Parts;

            db.Entry(repairOrder).State = EntityState.Modified;
            db.SaveChanges();

            return View(repairOrderPartsVM);
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
