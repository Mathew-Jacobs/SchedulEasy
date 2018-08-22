using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulEasy.Data
{
    public class BusyDay
    {
        [Key]
        public int BusyDayID { get; set; }
        public string UserID { get; set; }

        [Display(Name = "Short Descripton")]
        [MaxLength(30,ErrorMessage="Must be shorter than 30 characters")]
        public string Description { get; set; }

        public DateTimeOffset Busy { get; set; }
        public DateTimeOffset BusyEnd { get; set; }
        
        [ForeignKey("UserID")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
