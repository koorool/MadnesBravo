using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectPharmacy.Models
{
    public class MedicineGroupViewModel
    {
        public List<Medicine> Medicines { get; set; }
        public List<Group> Groups { get; set; }
    }
}