//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Webcuoiky.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class billtable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public billtable()
        {
            this.tbl_order = new HashSet<tbl_order>();
        }
    
        public int billid { get; set; }
        public string date { get; set; }
        public int user_id { get; set; }
        public int total_bill { get; set; }
    
        public virtual tbl_user tbl_user { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_order> tbl_order { get; set; }
    }
}
