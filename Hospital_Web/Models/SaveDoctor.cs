using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital_Web.Models
{
    public class SaveDoctor
    {
        public string d_id1 { get; set; }
        public string d_name1 { get; set; }
        public string spec2 { get; set; }
        public string hos2 { get; set; }
        public string con1 { get; set; }
        public string con2 { get; set; }
        public string gender { get; set; }
        public HttpPostedFileWrapper ImageFile { get; set; }

    }
}