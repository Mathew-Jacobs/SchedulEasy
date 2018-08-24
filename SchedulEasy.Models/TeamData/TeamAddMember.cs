using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulEasy.Models
{
    public class TeamAddMember
    {
        public int TeamID { get; set; }
        public string UserID { get; set; }
        public bool Private { get; set; }
    }
}
