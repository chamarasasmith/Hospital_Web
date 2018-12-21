using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital_Web.Models
{
    
    public class GrantAccess
    {
        static Hospital_WebEntities db = new Hospital_WebEntities();

        public static bool GetAccess(int rid, string pname) {

            bool f = false;
            try
            {
                
                   var data1= db.Role_Page.Where(x => x.Page1.Page_Method == pname && x.Role1.Role_ID==rid && x.Status=="1").ToList<Role_Page>();

                    if (data1!=null && data1.Count>0)
                    {
                        f = true;
                    }

            }
            catch (Exception)
            {
                f = false;
            }
            return f;
        }

    }
}