using SchedulEasy.Models;
using SchedulEasy.Models.Calendar;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SchedulEasy.Models.CalendarModel;

namespace SchedulEasy.Contracts
{
    public interface ICalendar
    {
        WeekForMonth GetCalendar(int month, int year);

        int GetDateInfo(DateTime now);

        List<BusyData> GetBusies();

        string GetBusyLevel(DateTime date, List<BusyData> busies);

        List<Color> GetColorGradient(int stepNumber);

        List<DescAndID> GetDescription(DateTime date, List<BusyData> busies);

        List<int> GetBusyDayID(DateTime date, List<BusyData> busies);
    }
}
