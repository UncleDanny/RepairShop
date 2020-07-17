using RepairShop.DAL;
using RepairShop.Models;
using RepairShop.View_Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace RepairShop.Controllers
{
    public class RepairOrdersController : Controller
    {
        private readonly ShopDbContext db = new ShopDbContext();

        // GET: RepairOrders
        public async Task<ActionResult> Index()
        {
            return View(await db.RepairOrders.Include(x => x.Customer)
                                             .Include(x => x.Repairman)
                                             .ToListAsync());
        }

        // GET: RepairOrders/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RepairOrder repairOrder = await db.RepairOrders.Include(x => x.Customer)
                                                           .Include(x => x.Repairman)
                                                           .Include(x => x.Parts)
                                                           .SingleOrDefaultAsync(x => x.ID == id);
            if (repairOrder == null)
            {
                return HttpNotFound();
            }

            return View(repairOrder);
        }

        // GET: RepairOrders/Create
        public async Task<ActionResult> Create()
        {
            RepairOrdersViewModel model = new RepairOrdersViewModel
            {
                Customers = await db.Customers.ToListAsync(),
                Repairmen = await db.Repairmen.ToListAsync(),
            };

            return View(model);
        }

        // POST: RepairOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RepairOrdersViewModel repairOrderVM)
        {
            repairOrderVM.Customers = await db.Customers.ToListAsync();
            repairOrderVM.Repairmen = await db.Repairmen.ToListAsync();
            if (ModelState.IsValid)
            {
                repairOrderVM.RepairOrder.Customer = await db.Customers.FindAsync(repairOrderVM.RepairOrder.Customer.ID);
                repairOrderVM.RepairOrder.Repairman = await db.Repairmen.FindAsync(repairOrderVM.RepairOrder.Repairman.ID);
                db.RepairOrders.Add(repairOrderVM.RepairOrder);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(repairOrderVM);
        }

        // GET: RepairOrders/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RepairOrder repairOrder = await db.RepairOrders.Include(x => x.Customer)
                                                           .Include(x => x.Repairman)
                                                           .SingleOrDefaultAsync(x => x.ID == id);
            if (repairOrder == null)
            {
                return HttpNotFound();
            }

            RepairOrdersViewModel repairOrderVM = new RepairOrdersViewModel
            {
                Customers = await db.Customers.ToListAsync(),
                Repairmen = await db.Repairmen.ToListAsync(),
                RepairOrder = repairOrder,
            };

            return View(repairOrderVM);
        }

        // POST: RepairOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RepairOrdersViewModel repairOrderVM)
        {
            if (ModelState.IsValid)
            {
                RepairOrder repairOrder = await db.RepairOrders.Include(p => p.Customer)
                                                               .Include(p => p.Repairman)
                                                               .SingleOrDefaultAsync(p => p.ID == repairOrderVM.RepairOrder.ID);

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
                repairOrder.Status = repairOrderVM.RepairOrder.Status;
                repairOrder.StartDate = repairOrderVM.RepairOrder.StartDate;
                repairOrder.EndDate = repairOrderVM.RepairOrder.EndDate;

                db.Entry(repairOrder).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(repairOrderVM);
        }

        // GET: RepairOrders/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RepairOrder repairOrder = await db.RepairOrders.Include(x => x.Customer)
                                                           .Include(x => x.Repairman)
                                                           .SingleOrDefaultAsync(x => x.ID == id);
            if (repairOrder == null)
            {
                return HttpNotFound();
            }

            return View(repairOrder);
        }

        // POST: RepairOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            List<AvailablePart> parts = await db.AvailableParts.Include(x => x.RepairOrder)
                                                               .Where(x => x.RepairOrder.ID == id)
                                                               .ToListAsync();
            foreach (AvailablePart part in parts)
            {
                part.RepairOrder = null;
            }

            RepairOrder repairOrder = await db.RepairOrders.FindAsync(id);
            db.RepairOrders.Remove(repairOrder);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: RepairOrders/Parts/5
        public async Task<ActionResult> Parts(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RepairOrder repairOrder = await db.RepairOrders.Include(x => x.Parts)
                                                           .SingleOrDefaultAsync(x => x.ID == id);
            if (repairOrder == null)
            {
                return HttpNotFound();
            }

            RepairOrderPartsViewModel repairOrderPartsVM = new RepairOrderPartsViewModel
            {
                RepairOrder = repairOrder,
            };

            return View(repairOrderPartsVM);
        }

        [HttpGet]
        public async Task<JsonResult> DbParts()
        {
            List<AvailablePart> parts = await db.AvailableParts.Where(x => x.RepairOrder == null)
                                                               .ToListAsync();
            return Json(parts, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> OrderParts(int id)
        {
            List<AvailablePart> parts = await db.AvailableParts.Include(x => x.RepairOrder)
                                                               .Where(y => y.RepairOrder.ID == id)
                                                               .ToListAsync();
            return Json(parts, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> Parts(List<AvailablePart> parts, int id)
        {
            if (parts is null)
            {
                parts = new List<AvailablePart>();
            }

            RepairOrder repairOrder = await db.RepairOrders.Include(x => x.Parts)
                                                           .SingleOrDefaultAsync(x => x.ID == id);
            foreach (AvailablePart part in parts)
            {
                AvailablePart dbPart = await db.AvailableParts.Include(x => x.RepairOrder)
                                                              .SingleOrDefaultAsync(x => x.ID == part.ID);
                dbPart.RepairOrder = repairOrder;
            }

            foreach (AvailablePart part in repairOrder.Parts.ToList())
            {
                if (!(parts.FindIndex(x => x.ID == part.ID) >= 0))
                {
                    repairOrder.Parts.Remove(part);
                }
            }

            return Json(await db.SaveChangesAsync());
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
