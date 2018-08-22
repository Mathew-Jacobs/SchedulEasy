using SchedulEasy.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SchedulEasy.Models.TeamCalendarModel;

namespace SchedulEasy.Services
{
    public class TeamCalendarService
    {
        private readonly string _userID;

        public TeamCalendarService(string userID)
        {
            _userID = userID;
        }

        public TeamWeekForMonth GetTeamCalendar(int month, int year, int teamID)
        {
            TeamWeekForMonth weeks = new TeamWeekForMonth();
            weeks.Week1 = new List<TeamDay>();
            weeks.Week2 = new List<TeamDay>();
            weeks.Week3 = new List<TeamDay>();
            weeks.Week4 = new List<TeamDay>();
            weeks.Week5 = new List<TeamDay>();
            weeks.Week6 = new List<TeamDay>();
            weeks.TeamID = teamID;

            List<DateTime> dt = new List<DateTime>();
            List<DateTime> bd = GetBusies(teamID);
            dt = GetDates(year, month);


            foreach (DateTime day in dt)
            {
                switch (GetWeekOfMonth(day))
                {
                    case 1:
                        TeamDay dy1 = new TeamDay();
                        dy1.Date = day;
                        dy1._Date = day.ToShortDateString();
                        dy1.dateStr = day.ToString("MM/dd/yyyy");
                        dy1.dtDay = day.Day;
                        dy1.daycolumn = GetDateInfo(dy1.Date);
                        dy1.BusyDayID = GetBusyDayID(day,teamID);
                        dy1.BusyLevel = GetBusyLevel(day,teamID);
                        dy1.Desc = GetDescription(day,teamID);
                        weeks.Week1.Add(dy1);
                        break;
                    case 2:
                        TeamDay dy2 = new TeamDay();
                        dy2.Date = day;
                        dy2._Date = day.ToShortDateString();
                        dy2.dateStr = day.ToString("MM/dd/yyyy");
                        dy2.dtDay = day.Day;
                        dy2.daycolumn = GetDateInfo(dy2.Date);
                        dy2.BusyDayID = GetBusyDayID(day,teamID);
                        dy2.BusyLevel = GetBusyLevel(day,teamID);
                        dy2.Desc = GetDescription(day,teamID);
                        weeks.Week2.Add(dy2);
                        break;
                    case 3:
                        TeamDay dy3 = new TeamDay();
                        dy3.Date = day;
                        dy3._Date = day.ToShortDateString();
                        dy3.dateStr = day.ToString("MM/dd/yyyy");
                        dy3.dtDay = day.Day;
                        dy3.daycolumn = GetDateInfo(dy3.Date);
                        dy3.BusyDayID = GetBusyDayID(day,teamID);
                        dy3.BusyLevel = GetBusyLevel(day,teamID);
                        dy3.Desc = GetDescription(day,teamID);
                        weeks.Week3.Add(dy3);
                        break;
                    case 4:
                        TeamDay dy4 = new TeamDay();
                        dy4.Date = day;
                        dy4._Date = day.ToShortDateString();
                        dy4.dateStr = day.ToString("MM/dd/yyyy");
                        dy4.dtDay = day.Day;
                        dy4.daycolumn = GetDateInfo(dy4.Date);
                        dy4.BusyDayID = GetBusyDayID(day,teamID);
                        dy4.BusyLevel = GetBusyLevel(day,teamID);
                        dy4.Desc = GetDescription(day,teamID);
                        weeks.Week4.Add(dy4);
                        break;
                    case 5:
                        TeamDay dy5 = new TeamDay();
                        dy5.Date = day;
                        dy5._Date = day.ToShortDateString();
                        dy5.dateStr = day.ToString("MM/dd/yyyy");
                        dy5.dtDay = day.Day;
                        dy5.daycolumn = GetDateInfo(dy5.Date);
                        dy5.BusyDayID = GetBusyDayID(day,teamID);
                        dy5.BusyLevel = GetBusyLevel(day,teamID);
                        dy5.Desc = GetDescription(day,teamID);
                        weeks.Week5.Add(dy5);
                        break;
                    case 6:
                        TeamDay dy6 = new TeamDay();
                        dy6.Date = day;
                        dy6._Date = day.ToShortDateString();
                        dy6.dateStr = day.ToString("MM/dd/yyyy");
                        dy6.dtDay = day.Day;
                        dy6.daycolumn = GetDateInfo(dy6.Date);
                        dy6.BusyDayID = GetBusyDayID(day,teamID);
                        dy6.BusyLevel = GetBusyLevel(day,teamID);
                        dy6.Desc = GetDescription(day,teamID);
                        weeks.Week6.Add(dy6);
                        break;
                };
            }

            while (weeks.Week1.Count < 7)
            {
                TeamDay dy = null;
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
                weeks.prevYear = (year - 1);
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

        public List<DateTime> GetBusies(int teamID)
        {
            List<DateTime> dates = new List<DateTime>();
            
            using (var ctx = new ApplicationDbContext())
            {
                List<string> userIDs = GetTeamMembers(teamID);
                foreach (BusyDay busy in ctx.BusyDays.ToList())
                {
                    if (userIDs.Contains(busy.UserID))
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

        private List<string> GetTeamMembers(int teamID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                List<string> userIDs = new List<string>();
                foreach (var item in ctx.TeamsData)
                {
                    if (item.TeamID == teamID && item.Private == false)
                    {
                        userIDs.Add(item.UserID);
                    }
                }

                return userIDs;
            }
        }

        public string GetBusyLevel(DateTime date, int teamID)
        {
            List<string> userIDs = GetTeamMembers(teamID);
            var dates = GetBusies(teamID);
            var colors = GetColorGradient(Convert.ToInt32(Math.Floor(255m / userIDs.Count)));
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
                busyLevel = colors.Count - 1;
            }

            return ColorTranslator.ToHtml(colors[busyLevel]);
        }

        public List<Color> GetColorGradient(int stepSize)
        {
            int redStart = 148; int redStep1 = stepSize * 4;
            int greenStart = 255; int greenStep1 = stepSize * 0;
            int blueStart = 127; int blueStep1 = stepSize * -1;

            int redMid = 255; int redStep2 = stepSize * -2;
            int greenMid = 255; int greenStep2 = stepSize * -5;
            int blueMid = 100; int blueStep2 = stepSize * -1;

            int redEnd = 178;
            int greenEnd = 57;
            int blueEnd = 59;

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

        public List<string> GetDescription(DateTime date, int teamID)
        {
            List<string> list = new List<string>();
            List<DateTime> busyDays = GetBusies(teamID);
            using (var ctx = new ApplicationDbContext())
            {
                foreach (BusyDay day in ctx.BusyDays.ToList())
                {
                    if (day.Busy.DateTime == date && busyDays.Contains(day.Busy.DateTime))
                    {
                        list.Add(day.Description);
                    }
                }

                return list;
            }
        }

        public int GetBusyDayID(DateTime date, int teamID)
        {
            List<DateTime> busyDays = GetBusies(teamID);
            using (var ctx = new ApplicationDbContext())
            {
                foreach (BusyDay day in ctx.BusyDays.ToList())
                {
                    if (day.Busy.DateTime == date && busyDays.Contains(day.Busy.DateTime))
                    {
                        return day.BusyDayID;
                    }
                }

                return 0;
            }
        }

        public bool AuthorizeUser(int teamID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                foreach (var item in ctx.TeamsData)
                {
                    if (item.TeamID == teamID && item.UserID == _userID)
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
