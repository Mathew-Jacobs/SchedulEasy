using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulEasy.Models
{
    public class BusyDayEdit
    {
        public int BusyDayID { get; set; }
        [Display(Name = "Start")]
        public DateTimeOffset Busy { get; set; }
        [Display(Name = "End")]
        public DateTimeOffset BusyEnd { get; set; }

        [MaxLength(30,ErrorMessage = "Must be shorter than 30 characters.")]
        [Display(Name = "Title")]
        public string Description { get; set; } 
    }
}
