using RepairShop.DAL;
using RepairShop.Models;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RepairShop.Controllers
{
    public class RepairmenController : Controller
    {
        private ShopDbContext db = new ShopDbContext();

        // GET: Repairmen
        public async Task<ActionResult> Index()
        {
            return View(await db.Repairmen.ToListAsync());
        }

        // GET: Repairmen/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Repairman repairman = await db.Repairmen.FindAsync(id);
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
        public async Task<ActionResult> Create([Bind(Include = "ID,Wage,FirstName,LastName")] Repairman repairman)
        {
            if (ModelState.IsValid)
            {
                db.Repairmen.Add(repairman);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(repairman);
        }

        // GET: Repairmen/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Repairman repairman = await db.Repairmen.FindAsync(id);
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
        public async Task<ActionResult> Edit([Bind(Include = "ID,Wage,FirstName,LastName")] Repairman repairman)
        {
            if (ModelState.IsValid)
            {
                db.Entry(repairman).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(repairman);
        }

        // GET: Repairmen/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Repairman repairman = await db.Repairmen.FindAsync(id);
            if (repairman == null)
            {
                return HttpNotFound();
            }

            return View(repairman);
        }

        // POST: Repairmen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Repairman repairman = await db.Repairmen.FindAsync(id);
            db.Repairmen.Remove(repairman);
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
