//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LTW_FFOOD.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Bill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bill()
        {
            this.Bill_info = new HashSet<Bill_info>();
        }
    
        public int bill_id { get; set; }
        public System.DateTime ngaydat { get; set; }
        public System.DateTime ngaygiao { get; set; }
        public int id_account { get; set; }
        public string status { get; set; }
        public string address { get; set; }
    
        public virtual Account Account { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bill_info> Bill_info { get; set; }
    }
}