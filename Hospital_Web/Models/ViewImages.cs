using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital_Web.Models
{
    public class ViewImages
    {

        public static string View1(byte[] img,string gen) {
            string s = "";
            if (img == null)
            {
                if (gen.Trim().ToString()=="Male")
                {
                    s = "/Img/mdoctor.png";
                }
                else
                {
                    s = "/Img/fdoctor.png";
                }
            }
            else {
                s= "data:image/jpg;base64,"+Convert.ToBase64String(img, 0, img.Length);
            }
            return s;
        }

    }
}