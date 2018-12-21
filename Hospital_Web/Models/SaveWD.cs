using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital_Web.Models
{
    public class SaveWD
    {
        public string Work_Date_ID { get; set; }
        public string Date_ID { get; set; }
        public string Doctor_ID { get; set; }
        public string Status { get; set; }
        public string Start_Time { get; set; }
        public string End_Time { get; set; }
        public string Patient { get; set; }
    }
}