using SchedulEasy.Data;
using SchedulEasy.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SchedulEasy.Models.CalendarModel;

namespace SchedulEasy.Services
{
    public class CalendarService
    {

        private readonly string _userID;

        public CalendarService(string userID)
        {
            _userID = userID;
        }

        

        public WeekForMonth GetCalendar(int month, int year)
        {
            WeekForMonth weeks = new WeekForMonth();
            weeks.Week1 = new List<Day>();
            weeks.Week2 = new List<Day>();
            weeks.Week3 = new List<Day>();
            weeks.Week4 = new List<Day>();
            weeks.Week5 = new List<Day>();
            weeks.Week6 = new List<Day>();

            List<DateTime> dt = new List<DateTime>();
            List<DateTime> bd = GetBusies();
            dt = GetDates(year, month);
            

            foreach (DateTime day in dt)
            {
                switch (GetWeekOfMonth(day))
                {
                    case 1:
                        Day dy1 = new Day();
                        dy1.Date = day;
                        dy1._Date = day.ToShortDateString();
                        dy1.dateStr = day.ToString("MM/dd/yyyy");
                        dy1.dtDay = day.Day;
                        dy1.daycolumn = GetDateInfo(dy1.Date);
                        dy1.BusyDayID = GetBusyDayID(day);
                        dy1.BusyLevel = GetBusyLevel(day);
                        dy1.Desc = GetDescription(day);
                        weeks.Week1.Add(dy1);
                        break;
                    case 2:
                        Day dy2 = new Day();
                        dy2.Date = day;
                        dy2._Date = day.ToShortDateString();
                        dy2.dateStr = day.ToString("MM/dd/yyyy");
                        dy2.dtDay = day.Day;
                        dy2.daycolumn = GetDateInfo(dy2.Date);
                        dy2.BusyDayID = GetBusyDayID(day);
                        dy2.BusyLevel = GetBusyLevel(day);
                        dy2.Desc = GetDescription(day);
                        weeks.Week2.Add(dy2);
                        break;
                    case 3:
                        Day dy3 = new Day();
                        dy3.Date = day;
                        dy3._Date = day.ToShortDateString();
                        dy3.dateStr = day.ToString("MM/dd/yyyy");
                        dy3.dtDay = day.Day;
                        dy3.daycolumn = GetDateInfo(dy3.Date);
                        dy3.BusyDayID = GetBusyDayID(day);
                        dy3.BusyLevel = GetBusyLevel(day);
                        dy3.Desc = GetDescription(day);
                        weeks.Week3.Add(dy3);
                        break;
                    case 4:
                        Day dy4 = new Day();
                        dy4.Date = day;
                        dy4._Date = day.ToShortDateString();
                        dy4.dateStr = day.ToString("MM/dd/yyyy");
                        dy4.dtDay = day.Day;
                        dy4.daycolumn = GetDateInfo(dy4.Date);
                        dy4.BusyDayID = GetBusyDayID(day);
                        dy4.BusyLevel = GetBusyLevel(day);
                        dy4.Desc = GetDescription(day);
                        weeks.Week4.Add(dy4);
                        break;
                    case 5:
                        Day dy5 = new Day();
                        dy5.Date = day;
                        dy5._Date = day.ToShortDateString();
                        dy5.dateStr = day.ToString("MM/dd/yyyy");
                        dy5.dtDay = day.Day;
                        dy5.daycolumn = GetDateInfo(dy5.Date);
                        dy5.BusyDayID = GetBusyDayID(day);
                        dy5.BusyLevel = GetBusyLevel(day);
                        dy5.Desc = GetDescription(day);
                        weeks.Week5.Add(dy5);
                        break;
                    case 6:
                        Day dy6 = new Day();
                        dy6.Date = day;
                        dy6._Date = day.ToShortDateString();
                        dy6.dateStr = day.ToString("MM/dd/yyyy");
                        dy6.dtDay = day.Day;
                        dy6.daycolumn = GetDateInfo(dy6.Date);
                        dy6.BusyDayID = GetBusyDayID(day);
                        dy6.BusyLevel = GetBusyLevel(day);
                        dy6.Desc = GetDescription(day);
                        weeks.Week6.Add(dy6);
                        break;
                };
            }

            while (weeks.Week1.Count < 7)
            {
                Day dy = null;
                weeks.Week1.Insert(0, dy);
            }

            if (month == 12)
            {
                weeks.nextMonth = 1;
                weeks.nextYear = (year + 1);
                weeks.prevMonth = (month - 1);
                weeks.prevYear = year;
            }
            else if (month == 1)
            {
                weeks.nextMonth = month + 1;
                weeks.nextYear = (year);
                weeks.prevMonth = 12;
                weeks.prevYear = (year-1);
            }
            else
            {
                weeks.nextMonth = month + 1;
                weeks.nextYear = year;
                weeks.prevMonth = (month - 1);
                weeks.prevYear = year;
            }

            return weeks;
        }

        public static List<DateTime> GetDates(int year, int month)
        {
            return Enumerable.Range(1, DateTime.DaysInMonth(year, month))
                .Select(day => new DateTime(year, month, day))
                .ToList();
        }

        public static int GetWeekOfMonth(DateTime date)
        {
            DateTime beginningOfMonth = new DateTime(date.Year, date.Month, 1);
            while (date.Date.AddDays(1).DayOfWeek != DayOfWeek.Sunday)
            {
                date = date.AddDays(1);
            }
            return (int)Math.Truncate(date.Subtract(beginningOfMonth).TotalDays / 7f) + 1;
        }

        public int GetDateInfo(DateTime now)
        {
            int dayNumber = 0;
            DateTime dt = now.Date;
            string dayStr = Convert.ToString(dt.DayOfWeek);


            if (dayStr.ToLower() == "sunday")
            {
                dayNumber = 0;
            }
            else if (dayStr.ToLower() == "monday")
            {
                dayNumber = 1;
            }
            else if (dayStr.ToLower() == "tuesday")
            {
                dayNumber = 2;
            }
            else if (dayStr.ToLower() == "wednesday")
            {
                dayNumber = 3;
            }
            else if (dayStr.ToLower() == "thursday")
            {
                dayNumber = 4;
            }
            else if (dayStr.ToLower() == "friday")
            {
                dayNumber = 5;
            }
            else if (dayStr.ToLower() == "saturday")
            {
                dayNumber = 6;
            }

            return dayNumber;
        }

        public List<DateTime> GetBusies()
        {
            List<DateTime> dates = new List<DateTime>();
            using (var ctx = new ApplicationDbContext())
            {
                foreach (BusyDay busy in ctx.BusyDays.ToList())
                {
                    if (busy.UserID == _userID)
                    {
                        var busyLength = (busy.BusyEnd - busy.Busy).Days + 1;
                        var busyDay = busy.Busy.DateTime;
                        if (busyLength > 0)
                        {
                            for (int i = 0; i < busyLength; i++)
                            {
                                dates.Add(busyDay.AddDays(i));
                            }
                        }
                        else
                        {
                        dates.Add(busy.Busy.DateTime);
                        }
                    }
                }
                return dates;
            }
        }

        public string GetBusyLevel(DateTime date)
        {
            var dates = GetBusies();
            int red = 0;
            int green = 175;
            int blue = 5;
            int stepsize = 175;
            List<Color> colors = new List<Color>();
            Color color = Color.FromArgb(1, red, green, blue);
            colors.Add(color);
            while (red < 175)
            {
                red += stepsize;
                if (red > 175) { red = 175; }
                colors.Add(Color.FromArgb(1, red, green, blue));
            }
            while (green > 0)
            {
                green -= stepsize;
                if (green < 0) { green = 0; }
                colors.Add(Color.FromArgb(1, red, green, blue));
            }
            int busyLevel = 0;
            foreach (DateTime day in dates)
            {
                if (day == date)
                {
                    busyLevel++;
                }
            }
            if (busyLevel >= colors.Count)
            {
                busyLevel = colors.Count -1;
            }

            return ColorTranslator.ToHtml(colors[busyLevel]);
        }

        public List<string> GetDescription(DateTime date)
        {
            List<string> list = new List<string>();
            using (var ctx = new ApplicationDbContext())
            {
                foreach (BusyDay day in ctx.BusyDays.ToList())
                {
                    if (day.Busy.DateTime == date)
                    {
                        list.Add(day.Description);
                    }
                }

                return list;
            }
        }
        public int GetBusyDayID(DateTime date)
        {
            using (var ctx = new ApplicationDbContext())
            {
                foreach (BusyDay day in ctx.BusyDays.ToList())
                {
                    if (day.Busy.DateTime == date)
                    {
                        return day.BusyDayID;
                    }
                }

                return 0;
            }
        }
    }
}
