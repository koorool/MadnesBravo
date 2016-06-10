using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CoachCompany.Models;

namespace CoachCompany.Controllers
{
    public class RoutesController : Controller
    {
        private CoachEntities db = new CoachEntities();

        // GET: Routes
        public ActionResult Index()
        {
            var routes = db.Routes.Include(r => r.Company);
            return View(routes.ToList());
        }

        // GET: Routes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var route = db.Routes.Find(id);
            RouteViewModel routeModel = new RouteViewModel {
                Number = route.number,
                Company = route.Company.Name,
                Stops = route.Stops.Select(x => new StopsViewModel {
                    Name = x.Name,
                    Latitude = x.Latitude,
                    Longtitude = x.Longtitude }).ToList()
            };
            if (route == null)
            {
                return HttpNotFound();
            }
            ViewBag.routeModel = routeModel;
            return View(route);
        }

        public class RouteViewModel
        {
            public int? Number { get; set; }
            public string Company { get; set; }
            public List<StopsViewModel> Stops { get; set; }
        }

        public class StopsViewModel
        {
            public string Name { get; set; }
            public string Latitude { get; set; }
            public string Longtitude { get; set; }
        }
        // GET: Routes/Create
        public ActionResult Create()
        {
            ViewBag.companyId = new SelectList(db.Companies, "id", "Name");
            return View();
        }

        // POST: Routes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,number,companyId")] Route route)
        {
            if (ModelState.IsValid)
            {
                route.id = Guid.NewGuid();
                db.Routes.Add(route);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.companyId = new SelectList(db.Companies, "id", "Name", route.companyId);
            return View(route);
        }

        // GET: Routes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Route route = db.Routes.Find(id);
            if (route == null)
            {
                return HttpNotFound();
            }
            ViewBag.companyId = new SelectList(db.Companies, "id", "Name", route.companyId);
            return View(route);
        }

        // POST: Routes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,number,companyId")] Route route)
        {
            if (ModelState.IsValid)
            {
                db.Entry(route).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.companyId = new SelectList(db.Companies, "id", "Name", route.companyId);
            return View(route);
        }

        // GET: Routes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Route route = db.Routes.Find(id);
            if (route == null)
            {
                return HttpNotFound();
            }
            return View(route);
        }

        // POST: Routes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Route route = db.Routes.Find(id);
            db.Routes.Remove(route);
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
