using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchedulEasy.Models
{
    public class BusyDayCreate
    {
        public string DefaultDay { get; set; }
        public DateTimeOffset Busy { get; set; }
        public DateTimeOffset ? BusyEnd { get; set; }

        [MaxLength(30,ErrorMessage ="Must be shorter than 30 characters")]
        public string Description { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}