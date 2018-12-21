using Hospital_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Hospital_Web.Controllers
{
    public class RegisterController : Controller
    {
        public Hospital_WebEntities db = new Hospital_WebEntities();
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult CheckUsername(string s)
        {
            Thread.Sleep(200);
            var data1 = db.Logins.Where(x=>x.Username==s).SingleOrDefault();
            if (data1 != null)
            {
                return Json(1);
            }
            else {
                return Json(0);
            }
        }

        public JsonResult Login1(Login s)
        {
            string txt = "U";
            try
            {
                
                var data1 = db.Logins.Where(x => x.Username == s.Username && x.Password == s.Password).SingleOrDefault();

                if (data1 != null)
                {
                    if (Session["ltype"] != null)
                    {
                        Session["ltype"] = null;
                    }
                    Session["Role_ID"] = data1.Role1.Role_ID.ToString();
                    Session["ltype"] = data1.Role1.Role_Name.ToString();
                    txt = "S";
                }
                else
                {
                    txt = "U";
                }
                return Json(txt, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //return Json("U", JsonRequestBehavior.AllowGet);
                throw;
            }
            

        }

            public JsonResult SaveData(SignUp1 s) {

            try
            {
                User_Details user = new User_Details();
                user.City = s.city1;
                user.Contact1 = s.con1;
                user.Contact2 = s.con2;
                user.Email1 = s.email2;
                user.Fax1 = s.fax2;
                user.First_Name = s.fn2;
                user.Last_Name = s.ln2;
                user.Street1 = s.str2;

                Login login = new Login();
                login.Username = s.un2;
                login.Password = s.pwd2;
                login.Status = "1";
                login.Role_ID =2;
                login.User_ID=user.User_ID;

                db.User_Details.Add(user);
                db.Logins.Add(login);
                db.SaveChanges();

                return Json("S", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("U", JsonRequestBehavior.AllowGet);
            }
            
        }


        public ActionResult LogOut1()
        {
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Index");
        }


        
    }
}