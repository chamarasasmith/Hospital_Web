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
    
    public partial class Role_Page
    {
        public int Role_Page_ID { get; set; }
        public Nullable<int> Role_ID { get; set; }
        public Nullable<int> Page_ID { get; set; }
        public string Status { get; set; }
    
        public virtual Page1 Page1 { get; set; }
        public virtual Role1 Role1 { get; set; }
    }
}
