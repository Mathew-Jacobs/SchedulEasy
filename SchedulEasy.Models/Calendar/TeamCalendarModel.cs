using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulEasy.Models
{
    public class TeamCalendarModel
    {
        public class TeamWeekForMonth
        {
            public List<TeamDay> Week1 { get; set; }
            public List<TeamDay> Week2 { get; set; }
            public List<TeamDay> Week3 { get; set; }
            public List<TeamDay> Week4 { get; set; }
            public List<TeamDay> Week5 { get; set; }
            public List<TeamDay> Week6 { get; set; }
            public int TeamID { get; set; }

            public int nextMonth { get; set; }
            public int nextYear { get; set; }
            public int prevMonth { get; set; }
            public int prevYear { get; set; }
        }

        public class TeamDay
        {
            public DateTime Date { get; set; }
            public string _Date { get; set; }
            public string dateStr { get; set; }
            public int dtDay { get; set; }
            public int? daycolumn { get; set; }
            public List<DescAndID> BusyDayID { get; set; }
            public string BusyLevel { get; set; }
        }
    }
}
