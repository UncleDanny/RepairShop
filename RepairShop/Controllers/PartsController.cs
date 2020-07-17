using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RepairShop.DAL;
using RepairShop.Models;
using RepairShop.View_Models;

namespace RepairShop.Controllers
{
    public class PartsController : Controller
    {
        private ShopDbContext db = new ShopDbContext();

        // GET: Parts
        public async Task<ActionResult> Index()
        {
            PartsViewModel partsVM = new PartsViewModel
            {
                Parts = await db.Parts.ToListAsync(),
                AvailableParts = await db.AvailableParts.ToListAsync(),
            };

            return View(partsVM);
        }

        // GET: Parts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Part part = await db.Parts.FindAsync(id);
            if (part == null)
            {
                return HttpNotFound();
            }

            return View(part);
        }

        // GET: Parts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Parts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Brand,Type,Price")] Part part)
        {
            if (ModelState.IsValid)
            {
                List<AvailablePart> availableParts = await db.AvailableParts.Where(x => x.Brand == part.Brand && x.Type == part.Type)
                                                                            .ToListAsync();
                foreach (AvailablePart availablePart in availableParts)
                {
                    availablePart.isActive = true;
                    availablePart.Price = part.Price;
                }

                db.Parts.Add(part);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(part);
        }

        // GET: Parts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Part part = await db.Parts.FindAsync(id);
            if (part == null)
            {
                return HttpNotFound();
            }

            return View(part);
        }

        // POST: Parts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Brand,Type,Price")] Part part)
        {
            if (ModelState.IsValid)
            {
                List<AvailablePart> parts = await db.AvailableParts.Where(x => x.Type == part.Type && x.Brand == part.Brand)
                                                                   .ToListAsync();
                foreach (AvailablePart availablePart in parts)
                {
                    availablePart.Brand = part.Brand;
                    availablePart.Type = part.Type;
                    availablePart.Price = part.Price;
                    db.Entry(availablePart).State = EntityState.Modified;
                }

                db.Entry(part).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(part);
        }

        // GET: Parts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Part part = await db.Parts.FindAsync(id);
            if (part == null)
            {
                return HttpNotFound();
            }

            return View(part);
        }

        // POST: Parts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Part part = await db.Parts.FindAsync(id);
            List<AvailablePart> availableParts = await db.AvailableParts.Where(x => x.Brand == part.Brand && x.Type == part.Type)
                                                                        .ToListAsync();
            foreach (AvailablePart availablePart in availableParts)
            {
                availablePart.isActive = false;
            }

            db.Parts.Remove(part);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> AddAvailablePart(int id)
        {
            Part part = await db.Parts.FindAsync(id);
            AvailablePart availablePart = new AvailablePart()
            {                
                Brand = part.Brand,
                Price = part.Price,
                Type = part.Type,
            };

            db.AvailableParts.Add(availablePart);
            await db.SaveChangesAsync();
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
