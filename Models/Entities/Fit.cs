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
    
    public partial class Fit
    {
        public int FitId { get; set; }
        public Nullable<int> AccessoryId { get; set; }
        public Nullable<int> BoatId { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual Accessory Accessory { get; set; }
        public virtual Boat Boat { get; set; }
    }
}
