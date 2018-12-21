using Hospital_Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital_Web.Controllers
{
    public class AdminController : Controller
    {
        Hospital_WebEntities db = new Hospital_WebEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public JsonResult SetWDStatus(string id1, string st) {
            try
            {
                int i1=int.Parse(id1);
                Work_Date wd= db.Work_Date.Where(x=>x.Work_Date_ID==i1).SingleOrDefault();
                wd.Status = st;
                db.SaveChanges();

                return Json("S",JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("U", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult RemoveWD(string id1)
        {
            try
            {
                int wd_id =int.Parse(id1.Trim());
                
                Work_Date wd= db.Work_Date.Where(x => x.Work_Date_ID == wd_id).SingleOrDefault();
                db.Work_Date.Remove(wd);
                db.SaveChanges();
                return Json("S", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("U", JsonRequestBehavior.AllowGet);
            }
            
        }

            public JsonResult SaveWD(SaveWD sw)
        {
            var date_id = int.Parse(sw.Date_ID.ToString().Trim());
            var doc_id = int.Parse(sw.Doctor_ID.ToString().Trim());
            var patient = int.Parse(sw.Patient.ToString().Trim());
            var s_time = DateTime.Parse(sw.Start_Time.ToString().Trim());
            var e_time = DateTime.Parse(sw.End_Time.ToString().Trim());

            try
            {
               Work_Date wd= new Work_Date();
                wd.Doctor_ID = doc_id;
                wd.Date_ID = date_id;
                wd.Start_Time = s_time;
                wd.End_Time = e_time;
                wd.Status = "1";
                wd.Max_Patient = patient;
                db.Work_Date.Add(wd);
                db.SaveChanges();
                return Json("S",JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return Json("U", JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetWorkDates(string text1) {
            try
            {
                List<Work_Date> wd=db.Work_Date.ToList<Work_Date>();
                return Json(wd, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("U", JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult AddWorkDateView()
        {
            List <Doctor> doc = db.Doctors.ToList();
            List<Date1> date1 = db.Date1.ToList();
            ViewBag.doc = doc;
            ViewBag.date1 = date1;
            return PartialView("AddWorkDateView");
        }

        public ActionResult SpecialtyUpdate(string s_id,string s_name)
        {
            var sid = s_id;
            var sname = s_name;

            if (sid!=null)
            {
                if (sid.Contains(":"))
                {
                    if (!string.IsNullOrEmpty(sname) && !string.IsNullOrWhiteSpace(sname))
                    {
                        var id1 = int.Parse(sid.Split(':')[0].Trim());
                        var s = db.Specialties.Where(x => x.Specialty_ID == id1).SingleOrDefault<Specialty>();
                        s.Specialty1 = sname.Trim();
                        db.SaveChanges();
                        return Content("Updated");
                    }
                    else {

                        return Content("U");
                    }
                    
                }
                else
                {
                    return Content("Select Specialty");
                }
            }
            else
            {
                return Content("U");
            }
            
        }

            public JsonResult SelectSpecialty(String doc)
        {
            if (doc != null && doc.Contains(":"))
            {
                int i = int.Parse(doc.Split(':')[0].ToString().Trim());
                var d = db.Specialties.Where(x => x.Specialty_ID == i).SingleOrDefault<Specialty>();


                string spec = "";
                

                
                if (d.Specialty1 != null)
                {
                    spec = d.Specialty1.ToString().Trim();
                }
                

                var a = new
                {
                    sname1 = spec
                    
                };
                return Json(a, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json("U", JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult SetSStatus0(string spec)
        {
            if (spec != null)
            {
                if (spec.Contains(":"))
                {
                    int i = int.Parse(spec.Split(':')[0].ToString().Trim());
                    var d = db.Specialties.Where(x => x.Specialty_ID == i).SingleOrDefault<Specialty>();
                    d.Status="0";
                    db.SaveChanges();

                    return Content(d.Specialty1.Trim() + " Deactivate");
                }
                else
                {
                    return Content("Select Specialty");
                }

            }
            else
            {
                return Content("U");
            }
        }


        public ActionResult SetSStatus1(string spec)
        {
            if (spec != null)
            {
                if (spec.Contains(":"))
                {
                    int i = int.Parse(spec.Split(':')[0].ToString().Trim());
                    var d = db.Specialties.Where(x => x.Specialty_ID == i).SingleOrDefault<Specialty>();
                    d.Status = "1";
                    db.SaveChanges();

                    return Content(d.Specialty1.Trim() + " Activate");
                }
                else
                {
                    return Content("Select Specialty");
                }

            }
            else
            {
                return Content("U");
            }
        }

        public JsonResult SelectDoctor1(String doc)
        {
            if (doc!=null && doc.Contains(":"))
            {
                    int i = int.Parse(doc.Split(':')[0].ToString().Trim());
                    var d = db.Doctors.Where(x => x.Doctor_ID == i).SingleOrDefault<Doctor>();

               
                string dname = "";
                string spec_id = "";
                string spec_name = "";
                string gen = "";
                string hos = "";
                string con1 = "";
                string con2 = "";

                if (d.Doctor_Name!=null)
                {
                    dname = d.Doctor_Name.ToString().Trim();
                }
                if (d.Specialty != null)
                {
                    spec_id = d.Specialty.Specialty_ID.ToString().Trim();
                }
                if (d.Specialty != null)
                {
                    spec_name = d.Specialty.Specialty1.ToString().Trim();
                }
                if (d.Gender != null)
                {
                    gen = d.Gender.ToString().Trim();
                }
                if (d.Hostpital != null)
                {
                    hos = d.Hostpital.ToString().Trim();
                }
                if (d.Contact1 != null)
                {
                    con1 = d.Contact1.ToString().Trim();
                }
                if (d.Contact2 != null)
                {
                    con2 = d.Contact2.ToString().Trim();
                }

                var a = new
                    {
                        dname1 = dname,
                        spec1 = spec_id+":"+spec_name,
                        gen1 = gen,
                        hos1 = hos,
                        con_1 = con1,
                        con_2 = con2
                    };
                    return Json(a, JsonRequestBehavior.AllowGet);
                
            }
            else
            {
                return Json("U", JsonRequestBehavior.AllowGet);
            }
            
        }

        public ActionResult SetDStatus1(string doc)
        {
            if (doc != null)
            {
                if (doc.Contains(":"))
                {
                    int i = int.Parse(doc.Split(':')[0].ToString().Trim());
                    var d = db.Doctors.Where(x => x.Doctor_ID == i).SingleOrDefault<Doctor>();
                    d.Status="1";
                    db.SaveChanges();

                    return Content("Dr " + d.Doctor_Name.Trim() + " Activate");
                }
                else
                {
                    return Content("Select Doctor");
                }
                
            }
            else {
                return Content("U");
            }
            }


        public ActionResult SetDStatus0(string doc)
        {
            if (doc != null)
            {
                if (doc.Contains(":"))
                {
                    int i = int.Parse(doc.Split(':')[0].ToString().Trim());
                    var d = db.Doctors.Where(x => x.Doctor_ID == i).SingleOrDefault<Doctor>();
                    d.Status = "0";
                    db.SaveChanges();

                    return Content("Dr " + d.Doctor_Name.Trim() + " Deactivate");
                }
                else
                {
                    return Content("Select Doctor");
                }

            }
            else
            {
                return Content("U");
            }
        }

        public ActionResult SaveDoctor(SaveDoctor sd)
        {

            var file1 = sd.ImageFile;
            var dname = sd.d_name1;
            var spec = sd.spec2;
            var hos = sd.hos2;
            var con1 = sd.con1;
            var con2 = sd.con2;
            var gen2 = sd.gender;

            byte[] imagebyte = null;

            if (file1!=null && dname!=null && spec != null && hos != null && con1!= null && gen2!=null)
            {
                if (sd.spec2.Contains(":"))
                {
                    if (gen2.Trim().ToString()=="Male" || gen2.Trim().ToString() == "Female")
                    {
                        BinaryReader br = new BinaryReader(file1.InputStream);
                        imagebyte = br.ReadBytes(file1.ContentLength);
                        Doctor d = new Doctor();
                        d.Doctor_Name = dname;
                        d.Hostpital = hos;
                        d.Contact1 = con1;
                        d.Contact2 = con2;
                        d.Specialty_ID = int.Parse(spec.Split(':')[0]);
                        d.Status = "1";
                        d.Gender = gen2;
                        d.Img = imagebyte;
                        db.Doctors.Add(d);
                        db.SaveChanges();
                        return Content("Dr " + dname.Trim() + " Inserted");
                    }
                    else
                    {
                        return Content("Select Gender");
                    }
                    
                }
                else
                {
                    return Content("Select Specialty");
                }
            }
            else
            {
                return Content("Some Feild Empty or Image File Empty");
            }

            
            
        }



        public ActionResult UpdateDoctor(SaveDoctor sd)
        {

            var file1 = sd.ImageFile;
            var d_id = sd.d_id1;
            var dname = sd.d_name1;
            var spec = sd.spec2;
            var hos = sd.hos2;
            var con1 = sd.con1;
            var con2 = sd.con2;
            var gen2 = sd.gender;

            byte[] imagebyte = null;

            if (file1 != null)
            {

                if (dname != null && spec != null && hos != null && con1 != null && gen2 != null && d_id.Contains(":"))
                {
                    if (spec.Contains(":"))
                    {
                        if (gen2.Trim().ToString() == "Male" || gen2.Trim().ToString() == "Female")
                        {
                            int i = int.Parse(d_id.Split(':')[0]);
                            BinaryReader br = new BinaryReader(file1.InputStream);
                            imagebyte = br.ReadBytes(file1.ContentLength);
                            Doctor d = db.Doctors.Where(x => x.Doctor_ID == i).SingleOrDefault<Doctor>();
                            d.Doctor_Name = dname;
                            d.Hostpital = hos;
                            d.Contact1 = con1;
                            d.Contact2 = con2;
                            d.Specialty_ID = int.Parse(spec.Split(':')[0]);
                            d.Status = "1";
                            d.Gender = gen2;
                            d.Img = imagebyte;
                            db.SaveChanges();
                            return Content("Dr " + dname.Trim() + " Updated");
                        }
                        else
                        {
                            return Content("Select Gender");
                        }

                    }
                    else
                    {
                        return Content("Select Specialty");
                    }
                }
                else
                {
                    return Content("Some Feild Empty");
                }
            }
            else {
                if (dname != null && spec != null && hos != null && con1 != null && gen2 != null && d_id.Contains(":"))
                {
                    if (spec.Contains(":"))
                    {
                        if (gen2.Trim().ToString() == "Male" || gen2.Trim().ToString() == "Female")
                        {
                            int i = int.Parse(d_id.Split(':')[0]);
                            Doctor d = db.Doctors.Where(x => x.Doctor_ID == i).SingleOrDefault<Doctor>();
                            d.Doctor_Name = dname;
                            d.Hostpital = hos;
                            d.Contact1 = con1;
                            d.Contact2 = con2;
                            d.Specialty_ID = int.Parse(spec.Split(':')[0]);
                            d.Status = "1";
                            d.Gender = gen2;
                            db.SaveChanges();
                            return Content("Dr " + dname.Trim() + " Updated");
                        }
                        else
                        {
                            return Content("Select Gender");
                        }

                    }
                    else
                    {
                        return Content("Select Specialty");
                    }
                }
                else
                {
                    return Content("Some Feild Empty");
                }
            }


            



        }




    }
}