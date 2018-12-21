using Hospital_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital_Web.Controllers
{
    public class HomeController : Controller
    {
        Hospital_WebEntities db = new Hospital_WebEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult DChannel()
        {
            ViewBag.Message = "Your Docotr Channeling page.";

            ViewBag.spec= db.Specialties.ToList<Specialty>();
            ViewBag.doc = db.Doctors.ToList<Doctor>();
            ViewBag.date1 = db.Date1.ToList<Date1>();

            return View();
        }

        public ActionResult DChannelView(DViewSearch dv)
        {

            int sp_id = int.Parse(dv.Specialty1);
            int doc_id = int.Parse(dv.d_name);
            int date1 = int.Parse(dv.date1);

           

            try
            {
                if (sp_id==0 && doc_id!=0 && date1!=0)
                {
                    List<Doctor> d = new List<Doctor>();
                    List<Work_Date> wd= db.Work_Date.Where(x => x.Date_ID == date1 && x.Doctor_ID==doc_id).Distinct().ToList();
                    foreach (var item in wd)
                    {
                        d.Add(db.Doctors.Where(x => x.Doctor_ID == item.Doctor_ID && x.Status.ToString().Trim()=="1").SingleOrDefault());
                    }
                    int s = int.Parse(dv.skip1);

                    ViewBag.dlist = d.Skip(s).Take(4);

                    return PartialView("DChannelView");
                }

                if (sp_id == 0 && doc_id == 0 && date1 != 0)
                {
                    List<Doctor> d = new List<Doctor>();
                    List<Work_Date> wd = db.Work_Date.Where(x => x.Date_ID == date1).Distinct().ToList();
                    foreach (var item in wd)
                    {
                        d.Add(db.Doctors.Where(x => x.Doctor_ID == item.Doctor_ID && x.Status.ToString().Trim() == "1").SingleOrDefault());
                    }
                    int s = int.Parse(dv.skip1);

                    ViewBag.dlist = d.Skip(s).Take(4);

                    return PartialView("DChannelView");
                }

                if (sp_id == 0 && doc_id == 0 && date1 == 0)
                {
                    List<Doctor> d = db.Doctors.Where(x => x.Status.ToString().Trim() == "1").ToList();

                    int s = int.Parse(dv.skip1);

                    ViewBag.dlist = d.Skip(s).Take(4);

                    return PartialView("DChannelView");
                }

                if (sp_id != 0 && doc_id == 0 && date1 == 0)
                {
                    List<Doctor> d = db.Doctors.Where(x => x.Specialty_ID == sp_id).ToList();

                    int s = int.Parse(dv.skip1);

                    ViewBag.dlist = d.Skip(s).Take(4);

                    return PartialView("DChannelView");
                }

                if (sp_id != 0 && doc_id != 0 && date1 == 0)
                {
                    List<Doctor> d = db.Doctors.Where(x => x.Specialty_ID == sp_id && x.Doctor_ID==doc_id).ToList();

                    int s = int.Parse(dv.skip1);

                    ViewBag.dlist = d.Skip(s).Take(4);

                    return PartialView("DChannelView");
                }


                if (sp_id == 0 && doc_id != 0 && date1 == 0)
                {
                    List<Doctor> d = db.Doctors.Where(x => x.Doctor_ID == doc_id).ToList();

                    int s = int.Parse(dv.skip1);

                    ViewBag.dlist = d.Skip(s).Take(4);

                    return PartialView("DChannelView");
                }

                if (sp_id != 0 && doc_id == 0 && date1 != 0)
                {
                    List<Doctor> d = new List<Doctor>();
                    List<Work_Date> wd = db.Work_Date.Where(x => x.Date_ID == date1).Distinct().ToList();
                    foreach (var item in wd)
                    {
                        d.Add(db.Doctors.Where(x => x.Specialty_ID==sp_id && x.Status.ToString().Trim() == "1").SingleOrDefault());
                    }
                    int s = int.Parse(dv.skip1);

                    ViewBag.dlist = d.Skip(s).Take(4);

                    return PartialView("DChannelView");
                }


                if (sp_id != 0 && doc_id != 0 && date1 != 0)
                {

                   List<Doctor> d1= db.Doctors.SqlQuery("SELECT * FROM Doctor WHERE Doctor_ID in (SELECT distinct Doctor_ID FROM Work_Date WHERE Date_ID='"+date1+"')").ToList();
                   List<Doctor> d2=d1.Where(x=>x.Specialty_ID==sp_id && x.Doctor_ID==doc_id).ToList();
                    int s = int.Parse(dv.skip1);

                    ViewBag.dlist = d2.Skip(s).Take(4);

                    return PartialView("DChannelView");
                }

                return Content("U");
            }
            catch (Exception)
            {
                return Content("U");
            }

            
        }

        public ActionResult AdminPage()
        {

            if (Session["Role_ID"]!=null)
            {
                string s=Session["Role_ID"].ToString();
                bool b = GrantAccess.GetAccess(int.Parse(s), "AdminPage");

                if (b)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Register");
                }
            }
            else
            {
                return RedirectToAction("Index", "Register");
            }



        }

        public ActionResult ChannelingView()
        {

            DateTime d1= DateTime.Now;

            List<Channel> ch= db.Channels.Where(x => x.Status.ToString().Trim() == "1").ToList();

            foreach (var item in ch)
            {
                int i=DateTime.Compare(d1.Date.Date,item.Channel_Date2.Value.Date.Date);
                if (i>=0)
                {
                    item.Status = "0";
                }
                
            }
            db.SaveChanges();

            ViewBag.chan1 = db.Channels.Where(x=>x.Status.ToString().Trim()=="1") .ToList<Channel>();

            return PartialView("ChannelingView");
        }

        public ActionResult SpecialtyView()
        {
            
            ViewBag.spec1 = db.Specialties.ToList<Specialty>(); 

            return PartialView("SpecialtyView");
        }

        public ActionResult SickView()
        {
            ViewBag.Message = "Your Docotr Channeling View page.";

            return PartialView("SickView");
        }

        public ActionResult DoctorView()
        {
            ViewBag.doc1 = db.Doctors.ToList<Doctor>();
            ViewBag.spec1 = db.Specialties.ToList<Specialty>();

            return PartialView("DoctorView");
        }


        public ActionResult WorkDayView()
        {
            ViewBag.doc1 = db.Doctors.ToList<Doctor>();
            ViewBag.wd1 = db.Work_Date.ToList<Work_Date>();
            ViewBag.date1 = db.Date1.ToList<Date1>();

            return PartialView("WorkDayView");
        }


        public JsonResult A_SignUp()
        {
            ViewBag.Message = "Your Docotr Channeling page.";

            return Json("", JsonRequestBehavior.AllowGet);
        }

    }
}