using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulEasy.Data
{
    public class TeamData
    {
        [Key]
        public int TeamDataID { get; set; }
        public int TeamID { get; set; }
        public string UserID { get; set; }
        public bool Private { get; set; }

        public virtual Team Team { get; set; }

        [ForeignKey("UserID")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
