//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CoachCompany.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Driver
    {
        public System.Guid id { get; set; }
        public string Name { get; set; }
        public string experience { get; set; }
        public Nullable<System.Guid> busId { get; set; }
    
        public virtual Bus Bus { get; set; }
    }
}
