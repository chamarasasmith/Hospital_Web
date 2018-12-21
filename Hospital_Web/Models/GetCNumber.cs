using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hospital_Web.Models;

namespace Hospital_Web.Models
{
    public class GetCNumber
    {
        static Hospital_WebEntities db = new Hospital_WebEntities();

        public static List<ChannelingDates> getCNo(List<Work_Date> wd) {


            try
            {
                List<ChannelingDates> cd = new List<ChannelingDates>();

                foreach (var item in wd)
                {

                    if (item.Status.ToString().Trim()=="1")
                    {
                        string day = item.Date1.Date_Of_Week.ToString().Trim();

                        DayOfWeek dw = DayOfWeek.Monday;

                        if (day.ToString().Trim() == "Monday")
                        {
                            dw = DayOfWeek.Monday;
                        }
                        if (day.ToString().Trim() == "Tuesday")
                        {
                            dw = DayOfWeek.Tuesday;
                        }
                        if (day.ToString().Trim() == "Wednesday")
                        {
                            dw = DayOfWeek.Wednesday;
                        }
                        if (day.ToString().Trim() == "Thursday")
                        {
                            dw = DayOfWeek.Thursday;
                        }
                        if (day.ToString().Trim() == "Friday")
                        {
                            dw = DayOfWeek.Friday;
                        }
                        if (day.ToString().Trim() == "Saturday")
                        {
                            dw = DayOfWeek.Saturday;
                        }
                        if (day.ToString().Trim() == "Sunday")
                        {
                            dw = DayOfWeek.Sunday;
                        }

                        DateTime nextday = GetNextWeekday(dw);

                        string d1 = nextday.ToString("yyyy:MM:dd");

                        ChannelingDates cd2 = new ChannelingDates();

                        List<Channel> ch1 = db.Channels.ToList();
                        List<Channel> ch2 = ch1.Where(x => x.Channel_Date2.Value.Date == nextday.Date && x.Work_Date_ID==item.Work_Date_ID).ToList();

                        int no = 0;

                        if (ch2.Count>0)
                        {
                            no = int.Parse(ch2.Last().No1);
                        }

                        cd2.count1 = ch2.Count+"";
                        cd2.wd_id = item.Work_Date_ID;
                        cd2.Date1 = d1;
                        cd2.No1 = no + 1+"";
                        cd2.day1 = item.Date1.Date_Of_Week.ToString().Trim();
                        cd2.stime1 = item.Start_Time.Value.ToString("HH:mm").Trim();
                        cd2.etime1 = item.End_Time.Value.ToString("HH:mm").Trim();
                        cd2.max1 = item.Max_Patient.ToString().Trim();
                        cd.Add(cd2);

                    }



                    //return no;
                }

                return cd;

            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public static DateTime GetNextWeekday(DayOfWeek day)
        {
            DateTime result = DateTime.Now.AddDays(1);
            while (result.DayOfWeek != day)
            {
                result = result.AddDays(1);
            }
            return result;
        }

    }
}