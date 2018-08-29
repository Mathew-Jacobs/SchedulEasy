using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulEasy.Models.Calendar
{
    public class CalendarModelSimple
    {
        public class WeekForMonthSimple
        {
            public DateTime MonthInfo { get; set; }
            public List<DaySimple> Week1 { get; set; }
            public List<DaySimple> Week2 { get; set; }
            public List<DaySimple> Week3 { get; set; }
            public List<DaySimple> Week4 { get; set; }
            public List<DaySimple> Week5 { get; set; }
            public List<DaySimple> Week6 { get; set; }
        }

        public class DaySimple
        {
            public DateTime Date { get; set; }
            public string dateStr { get; set; }
            public int? daycolumn { get; set; }
            public string BusyLevel { get; set; }
        }
    }
}
