using ProjectPharmacy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectPharmacy.Controllers
{
    public class HomeController : Controller
    {
        private PharmacyEntities db = new PharmacyEntities();
        public ActionResult Index()
        {
            return View();
        }
        

        public ActionResult Contact()
        {
            var sups = db.Suppliers
                .Select(x=>new ContactViewModel { Name=x.Name, Latitude=x.Latitude, Longtitude=x.Longtitude});
            ViewBag.Message = "We are free to contact with you. Here our addresses:";

            return View(sups);
        }
    }
    public class ContactViewModel
    {
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longtitude { get; set; }
    }
}