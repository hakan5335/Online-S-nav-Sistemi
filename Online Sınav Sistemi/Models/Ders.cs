//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Online_Sınav_Sistemi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ders
    {
        public Ders()
        {
            this.Konu = new HashSet<Konu>();
            this.Sınav = new HashSet<Sınav>();
        }
    
        public int DersID { get; set; }
        public string DersAdi { get; set; }
        public Nullable<int> DonemID { get; set; }
    
        public virtual Donem Donem { get; set; }
        public virtual ICollection<Konu> Konu { get; set; }
        public virtual ICollection<Sınav> Sınav { get; set; }
    }
}
