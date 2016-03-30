using ProjectPharmacy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProjectPharmacy.Controllers
{
    public class MedicinesListController : Controller
    {
        private PharmacyEntities _context;

        public MedicinesListController()
        {
            _context = new PharmacyEntities();
        }

        // GET: MedicinesList
        public async Task<ActionResult> Index(Guid? groupId)
        {
            if (groupId!=null && !(_context.Groups.Any(t => t.Id == groupId))) return new HttpNotFoundResult();
            ViewBag.SelectedGroup = groupId;
            var medicines = groupId == null ? await _context.Medicines
                .OrderBy(t => t.Name)
                .ToListAsync() : null;
            var lst = await _context.Groups
                .OrderBy(t => t.Name)
                .ToListAsync();
            return View(new MedicineGroupViewModel { Medicines = medicines, Groups = lst});
        }

        public async Task<ActionResult> MedicinesList(Group group)
        {
            var lst = await _context.Medicines
                .Where(t => t.GroupId == group.Id)
                .Include("Supplier")
                .OrderBy(t => t.Name)
                .ToListAsync();
            return PartialView(lst);
        }
    }
}