using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectPharmacy.Models;

namespace ProjectPharmacy.Controllers
{
    public class MedicineDeliveriesController : Controller
    {
        private PharmacyEntities db = new PharmacyEntities();

        // GET: MedicineDeliveries
        public async Task<ActionResult> Index()
        {
            var medicineDeliveries = db.MedicineDeliveries.Include(m => m.Delivery).Include(m => m.Medicine);
            return View(await medicineDeliveries.ToListAsync());
        }

        // GET: MedicineDeliveries/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicineDelivery medicineDelivery = await db.MedicineDeliveries.FindAsync(id);
            if (medicineDelivery == null)
            {
                return HttpNotFound();
            }
            return View(medicineDelivery);
        }

        // GET: MedicineDeliveries/Create
        public ActionResult Create()
        {
            ViewBag.DeliveryId = new SelectList(db.Deliveries, "Id", "Date");
            ViewBag.MedicineId = new SelectList(db.Medicines, "Id", "Name");
            return View();
        }

        // POST: MedicineDeliveries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MedicineId,DeliveryId,Quantity")] MedicineDelivery medicineDelivery)
        {
            if (ModelState.IsValid)
            {
                medicineDelivery.Id = Guid.NewGuid();
                db.MedicineDeliveries.Add(medicineDelivery);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.DeliveryId = new SelectList(db.Deliveries, "Id", "Id", medicineDelivery.DeliveryId);
            ViewBag.MedicineId = new SelectList(db.Medicines, "Id", "Name", medicineDelivery.MedicineId);
            return View(medicineDelivery);
        }

        // GET: MedicineDeliveries/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicineDelivery medicineDelivery = await db.MedicineDeliveries.FindAsync(id);
            if (medicineDelivery == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeliveryId = new SelectList(db.Deliveries, "Id", "Date", medicineDelivery.DeliveryId);
            ViewBag.MedicineId = new SelectList(db.Medicines, "Id", "Name", medicineDelivery.MedicineId);
            return View(medicineDelivery);
        }

        // POST: MedicineDeliveries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MedicineId,DeliveryId,Quantity,Id")] MedicineDelivery medicineDelivery)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicineDelivery).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DeliveryId = new SelectList(db.Deliveries, "Id", "Date", medicineDelivery.DeliveryId);
            ViewBag.MedicineId = new SelectList(db.Medicines, "Id", "Name", medicineDelivery.MedicineId);
            return View(medicineDelivery);
        }

        // GET: MedicineDeliveries/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicineDelivery medicineDelivery = await db.MedicineDeliveries.FindAsync(id);
            if (medicineDelivery == null)
            {
                return HttpNotFound();
            }
            return View(medicineDelivery);
        }

        // POST: MedicineDeliveries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            MedicineDelivery medicineDelivery = await db.MedicineDeliveries.FindAsync(id);
            db.MedicineDeliveries.Remove(medicineDelivery);
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
