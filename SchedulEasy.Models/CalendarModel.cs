using SchedulEasy.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulEasy.Models
{
    public class CalendarModel
    {
        public class WeekForMonth
        {
            public List<Day> Week1 { get; set; }
            public List<Day> Week2 { get; set; }
            public List<Day> Week3 { get; set; }
            public List<Day> Week4 { get; set; }
            public List<Day> Week5 { get; set; }
            public List<Day> Week6 { get; set; }

            public int nextMonth { get; set; }
            public int nextYear { get; set; }
            public int prevMonth { get; set; }
            public int prevYear { get; set; }
        }

        public class Day
        {
            public DateTime Date { get; set; }
            public string _Date { get; set; }
            public string dateStr { get; set; }
            public int dtDay { get; set; }
            public int? daycolumn { get; set; }
            public int BusyDayID { get; set; }
            public decimal BusyLevel { get; set; }
            public List<string> Desc { get; set; }
        }
    }
}
