//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hospital_Web.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Page1
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Page1()
        {
            this.Role_Page = new HashSet<Role_Page>();
        }
    
        public int Page_ID { get; set; }
        public string Page_Name { get; set; }
        public string Page_Controller { get; set; }
        public string Page_Method { get; set; }
        public string Status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Role_Page> Role_Page { get; set; }
    }
}
