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
    
    public partial class producttable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public producttable()
        {
            this.tbl_order = new HashSet<tbl_order>();
        }
    
        public int productid { get; set; }
        public string productname { get; set; }
        public int productquantity { get; set; }
        public int productprice { get; set; }
        public string productcategory { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_order> tbl_order { get; set; }
    }
}
