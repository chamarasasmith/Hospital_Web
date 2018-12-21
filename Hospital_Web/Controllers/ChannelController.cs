using Hospital_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital_Web.Controllers
{
    public class ChannelController : Controller
    {
        public Hospital_WebEntities db = new Hospital_WebEntities();
        // GET: Channel
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult GetViewDetails1(int id=0)
        {
            if (id == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Doctor data1 = db.Doctors.Where(x => x.Doctor_ID == id).SingleOrDefault();
                ViewBag.doc = data1;
                return PartialView("GetViewDetails1");
            }
        }

        public JsonResult ChannelDoctor(string nic1, string fn2, string ln2, string con2, string wd_id,string doc_id1,string cdate,string cno1)
        {
            try
            {
                int doc_id = int.Parse(doc_id1);
                int wd_id1 = int.Parse(wd_id);
                DateTime d1 = DateTime.Now;
                DateTime d2=DateTime.Now;
                if (cdate.Contains(":"))
                {
                    int year1= int.Parse(cdate.Split(':')[0]);
                    int month1 = int.Parse(cdate.Split(':')[1]);
                    int day1 = int.Parse(cdate.Split(':')[2]);
                    d2=new DateTime(year1,month1,day1);
                }

                //DateTime d2 = DateTime.Parse(cdate);
                List<Channel> ch1 = db.Channels.ToList();
                List<Channel> ch2 = ch1.Where(x => x.Channel_Date2.Value.Date == d2.Date && x.Work_Date_ID == wd_id1).ToList();

                List<User_Details> ud = db.User_Details.Where(x => x.NICOrPassport.ToString().Trim() == nic1.ToString().Trim()).ToList();

                int no = 0;

                if (ch2.Count > 0)
                {
                    no = int.Parse(ch2.Last().No1);
                }

                if (ud.Count > 0)
                {
                    User_Details ud1 = ud.First();

                    Channel c = new Channel();
                    c.Doctor_ID = doc_id;
                    c.Contact1 = ud1.Contact1.ToString().Trim();
                    c.Status = "1";
                    c.Channel_Date1 = d1;
                    c.Channel_Date2 = d2;
                    c.User_ID = ud1.User_ID;
                    c.Work_Date_ID = wd_id1;
                    c.No1 = no+1+"";
                    db.Channels.Add(c);
                    db.SaveChanges();
                    return Json(no+1+"", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    User_Details ud2 = new User_Details();
                    ud2.First_Name = fn2;
                    ud2.Last_Name = ln2;
                    ud2.NICOrPassport = nic1;
                    ud2.Contact1 = con2;
                    db.User_Details.Add(ud2);

                    Channel c = new Channel();
                    c.Doctor_ID = doc_id;
                    c.Contact1 = con2;
                    c.Status = "1";
                    c.Channel_Date1 = d1;
                    c.Channel_Date2 = d2;
                    c.User_ID = ud2.User_ID;
                    c.Work_Date_ID = wd_id1;
                    c.No1 = no+1+"";
                    db.Channels.Add(c);
                    db.SaveChanges();
                    return Json(no + 1 + "", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {
                return Json("U",JsonRequestBehavior.AllowGet);
            }
        }

            public JsonResult CheckNICAndPassport(string nic1) {
            try
            {
                List<User_Details>ud= db.User_Details.Where(x => x.NICOrPassport.ToString().Trim() == nic1.ToString().Trim()).ToList();
                if (ud.Count>0)
                {
                    return Json("S", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("U", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json("U", JsonRequestBehavior.AllowGet);
            }
            
        }

        
    }
}