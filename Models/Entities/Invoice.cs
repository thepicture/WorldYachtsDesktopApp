//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WorldYachtsDesktopApp.Models.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Invoice
    {
        public int InvoiceId { get; set; }
        public Nullable<int> ContractId { get; set; }
        public bool Settled { get; set; }
        public decimal Sum { get; set; }
        public decimal SumInclVAT { get; set; }
        public System.DateTime Date { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual Contract Contract { get; set; }
    }
}
