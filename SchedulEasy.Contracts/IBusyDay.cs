using SchedulEasy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulEasy.Contracts
{
    public interface IBusyDay
    {
        BusyDayCreate GetCreateByDate(DateTime date);
        bool CreateBusyDay(BusyDayCreate model);
        IEnumerable<BusyDayListItem> GetBusyDays();
        BusyDayDetailValidation GetBusyDayByIDAndTeam(int id, int? teamID);
        bool UpdateBusyDay(BusyDayEdit model);
        bool DeleteDay(int id);
    }
}
