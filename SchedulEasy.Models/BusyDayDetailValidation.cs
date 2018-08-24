using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulEasy.Models
{
    public class BusyDayDetailValidation
    {
        public BusyDayDetail busyDayDetail { get; set; }
        public bool Authenticated { get; set; }
        public int ? TeamID { get; set; }
    }
}
