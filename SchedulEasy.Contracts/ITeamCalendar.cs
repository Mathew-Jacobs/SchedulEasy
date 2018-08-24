using SchedulEasy.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SchedulEasy.Models.CalendarModel;
using static SchedulEasy.Models.TeamCalendarModel;

namespace SchedulEasy.Contracts
{
    public interface ITeamCalendar
    {
        TeamWeekForMonth GetTeamCalendar(int month, int year, int teamID);

        int GetDateInfo(DateTime now);

        List<DateTime> GetBusies(int teamID);

        string GetBusyLevel(DateTime date, int teamID);

        List<Color> GetColorGradient(int stepNumber);

        List<string> GetDescription(DateTime date, int teamID);

        int GetBusyDayID(DateTime date, int teamID);

        bool AuthorizeUser(int teamID);
    }
}
