using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulEasy.Models
{
    public class BusyDayDetail
    {
        public int BusyDayID { get; set; }
        public DateTimeOffset Busy { get; set; }
        public DateTimeOffset BusyEnd { get; set; }
        public string Description { get; set; }
    }
}
