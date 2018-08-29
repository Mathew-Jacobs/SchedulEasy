using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SchedulEasy.Models.Calendar.CalendarModelSimple;

namespace SchedulEasy.Models.Calendar
{
    public class YearCalendar
    {
        public List<WeekForMonthSimple> weeks { get; set; }
    }
}
