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
    public class MedicinesController : Controller
    {
        private PharmacyEntities db = new PharmacyEntities();

        // GET: Medicines
        public async Task<ActionResult> Index()
        {
            var medicines = db.Medicines.Include(m => m.Group).Include(m => m.Supplier).Include(m => m.Warehouse);
            return View(await medicines.ToListAsync());
        }

        // GET: Medicines/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicine medicine = await db.Medicines.FindAsync(id);
            if (medicine == null)
            {
                return HttpNotFound();
            }
            return View(medicine);
        }

        // GET: Medicines/Create
        public ActionResult Create()
        {
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "Name");
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name");
            ViewBag.Id = new SelectList(db.Warehouses, "MedicineId", "MedicineId");
            return View();
        }

        // POST: Medicines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,GroupId,SupplierId,Price")] Medicine medicine)
        {
            if (ModelState.IsValid)
            {
                medicine.Id = Guid.NewGuid();
                db.Medicines.Add(medicine);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.GroupId = new SelectList(db.Groups, "Id", "Name", medicine.GroupId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", medicine.SupplierId);
            ViewBag.Id = new SelectList(db.Warehouses, "MedicineId", "MedicineId", medicine.Id);
            return View(medicine);
        }

        // GET: Medicines/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicine medicine = await db.Medicines.FindAsync(id);
            if (medicine == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "Name", medicine.GroupId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", medicine.SupplierId);
            ViewBag.Id = new SelectList(db.Warehouses, "MedicineId", "MedicineId", medicine.Id);
            return View(medicine);
        }

        // POST: Medicines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,GroupId,SupplierId,Price")] Medicine medicine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicine).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "Name", medicine.GroupId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", medicine.SupplierId);
            ViewBag.Id = new SelectList(db.Warehouses, "MedicineId", "MedicineId", medicine.Id);
            return View(medicine);
        }

        // GET: Medicines/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicine medicine = await db.Medicines.FindAsync(id);
            if (medicine == null)
            {
                return HttpNotFound();
            }
            return View(medicine);
        }

        // POST: Medicines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Medicine medicine = await db.Medicines.FindAsync(id);
            db.Medicines.Remove(medicine);
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
