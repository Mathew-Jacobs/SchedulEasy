using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchedulEasy.Models
{
    public class BusyDayListItem
    {
        public int BusyDayID { get; set; }
        public DateTimeOffset Busy { get; set; }
        public DateTimeOffset BusyEnd { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}