using SchedulEasy.Contracts;
using SchedulEasy.Data;
using SchedulEasy.Models;
using SchedulEasy.Models.Calendar;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SchedulEasy.Models.CalendarModel;

namespace SchedulEasy.Services
{
    public class CalendarService : ICalendar
    {

        private readonly string _userID;

        public CalendarService(string userID)
        {
            _userID = userID;
        }

        

        public WeekForMonth GetCalendar(int month, int year)
        {
            WeekForMonth weeks = new WeekForMonth
            {
                Week1 = new List<Day>(),
                Week2 = new List<Day>(),
                Week3 = new List<Day>(),
                Week4 = new List<Day>(),
                Week5 = new List<Day>(),
                Week6 = new List<Day>()
            };

            List<DateTime> dt = new List<DateTime>();
            //List<DateTime> bd = GetBusies();
            dt = GetDates(year, month);
            

            foreach (DateTime day in dt)
            {
                switch (GetWeekOfMonth(day))
                {
                    case 1:
                        Day dy1 = new Day
                        {
                            Date = day,
                            _Date = day.ToShortDateString(),
                            dateStr = day.ToString("MM/dd/yyyy"),
                            dtDay = day.Day
                        };
                        dy1.daycolumn = GetDateInfo(dy1.Date);
                        dy1.BusyLevel = GetBusyLevel(day);
                        dy1.BusyDayID = GetDescription(day);
                        weeks.Week1.Add(dy1);
                        break;
                    case 2:
                        Day dy2 = new Day
                        {
                            Date = day,
                            _Date = day.ToShortDateString(),
                            dateStr = day.ToString("MM/dd/yyyy"),
                            dtDay = day.Day
                        };
                        dy2.daycolumn = GetDateInfo(dy2.Date);
                        dy2.BusyLevel = GetBusyLevel(day);
                        dy2.BusyDayID = GetDescription(day);
                        weeks.Week2.Add(dy2);
                        break;
                    case 3:
                        Day dy3 = new Day
                        {
                            Date = day,
                            _Date = day.ToShortDateString(),
                            dateStr = day.ToString("MM/dd/yyyy"),
                            dtDay = day.Day
                        };
                        dy3.daycolumn = GetDateInfo(dy3.Date);
                        dy3.BusyLevel = GetBusyLevel(day);
                        dy3.BusyDayID = GetDescription(day);
                        weeks.Week3.Add(dy3);
                        break;
                    case 4:
                        Day dy4 = new Day
                        {
                            Date = day,
                            _Date = day.ToShortDateString(),
                            dateStr = day.ToString("MM/dd/yyyy"),
                            dtDay = day.Day
                        };
                        dy4.daycolumn = GetDateInfo(dy4.Date);
                        dy4.BusyLevel = GetBusyLevel(day);
                        dy4.BusyDayID = GetDescription(day);
                        weeks.Week4.Add(dy4);
                        break;
                    case 5:
                        Day dy5 = new Day
                        {
                            Date = day,
                            _Date = day.ToShortDateString(),
                            dateStr = day.ToString("MM/dd/yyyy"),
                            dtDay = day.Day
                        };
                        dy5.daycolumn = GetDateInfo(dy5.Date);
                        dy5.BusyLevel = GetBusyLevel(day);
                        dy5.BusyDayID = GetDescription(day);
                        weeks.Week5.Add(dy5);
                        break;
                    case 6:
                        Day dy6 = new Day
                        {
                            Date = day,
                            _Date = day.ToShortDateString(),
                            dateStr = day.ToString("MM/dd/yyyy"),
                            dtDay = day.Day
                        };
                        dy6.daycolumn = GetDateInfo(dy6.Date);
                        dy6.BusyLevel = GetBusyLevel(day);
                        dy6.BusyDayID = GetDescription(day);
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

        public List<BusyData> GetBusies()
        {
            List<BusyData> dateData = new List<BusyData>();
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
                                BusyData data = new BusyData
                                {
                                    Day = busyDay.AddDays(i),
                                    Description = busy.Description,
                                    ID = busy.BusyDayID
                                };
                                dateData.Add(data);
                            }
                        }
                        else
                        {
                            BusyData data = new BusyData
                            {
                                Day = busy.Busy.DateTime,
                                Description = busy.Description,
                                ID = busy.BusyDayID
                            };
                            dateData.Add(data);
                        }
                    }
                }
                return dateData;
            }
        }

        public string GetBusyLevel(DateTime date)
        {
            var dates = GetBusies();
            var colors = GetColorGradient(1);
            int busyLevel = 0;
            foreach (var day in dates)
            {
                if (day.Day == date)
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


        public List<Color> GetColorGradient(int stepNumber)
        {
            int redStart = 148;
            int greenStart = 255;
            int blueStart = 127;

            int redMid = 255;
            int greenMid = 255;
            int blueMid = 100;

            int redEnd = 178;
            int greenEnd = 57;
            int blueEnd = 59;

            int redStep1 = Convert.ToInt32(Math.Ceiling((redMid - redStart) / (stepNumber + 1m)));
            int greenStep1 = Convert.ToInt32(Math.Ceiling((greenMid - greenStart) / (stepNumber + 1m)));
            int blueStep1 = Convert.ToInt32(Math.Ceiling((blueMid - blueStart) / (stepNumber + 1m)));

            int redStep2 = Convert.ToInt32(Math.Ceiling((redEnd - redMid) / (stepNumber + 1m)));
            int greenStep2 = Convert.ToInt32(Math.Ceiling((greenEnd - greenMid) / (stepNumber + 1m)));
            int blueStep2 = Convert.ToInt32(Math.Ceiling((blueEnd - blueMid) / (stepNumber + 1m)));

            int red = redStart;
            int green = greenStart;
            int blue = blueStart;

            List<Color> colors = new List<Color>();
            Color color = Color.FromArgb(1, redStart, greenStart, blueStart);
            colors.Add(color);

            while (red < redMid)
            {
                red += redStep1;
                green += greenStep1;
                blue += blueStep1;

                if (red > redMid) { red = redMid; }
                if (green > greenMid) { green = greenMid; }
                if (blue < blueMid) { blue = blueMid; }

                colors.Add(Color.FromArgb(1, red, green, blue));
            }
            while (green > greenEnd)
            {
                red += redStep2;
                green += greenStep2;
                blue += blueStep2;

                if (red < redEnd) { red = redEnd; }
                if (green < greenEnd) { green = greenEnd; }
                if (blue < blueEnd) { blue = blueEnd; }

                colors.Add(Color.FromArgb(1, red, green, blue));
            }

            return colors;
        }

        public List<DescAndID> GetDescription(DateTime date)
        {
            List<DescAndID> list = new List<DescAndID>();
            
            var dateData = GetBusies();
            var dates = new List<DateTime>();

            foreach (var item in dateData)
            {
                dates.Add(item.Day);
            }

            using (var ctx = new ApplicationDbContext())
            {
                foreach (BusyDay day in ctx.BusyDays.ToList())
                {
                    if (date >= day.Busy && date <= day.BusyEnd && day.UserID == _userID)
                    {
                        foreach (var data in dateData)
                        {
                            if (date == data.Day && !list.Contains(new DescAndID { Description = data.Description, ID = day.BusyDayID }))
                            {
                                DescAndID item = new DescAndID
                                {
                                    Description = data.Description,
                                    ID = data.ID
                                };
                                list.Add(item);
                            }
                        }
                        break;
                    }
                }

                return list;
            }
        }

        public List<int> GetBusyDayID(DateTime date)
        {
            var dateData = GetBusies();
            var dates = new List<DateTime>();

            foreach (var item in dateData)
            {
                dates.Add(item.Day);
            }

            List<int> ids = new List<int>();
            using (var ctx = new ApplicationDbContext())
            {
                foreach (BusyDay day in ctx.BusyDays.ToList())
                {
                    if (day.Busy.DateTime == date && dates.Contains(day.Busy.DateTime))
                    {
                        ids.Add(day.BusyDayID);
                    }
                }

                return ids;
            }
        }
    }
}
