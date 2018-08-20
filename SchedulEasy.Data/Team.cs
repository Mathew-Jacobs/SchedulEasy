using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulEasy.Data
{
    public class Team
    {
        [Key]
        public int TeamID { get; set; }
        public Guid OwnerID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }
}
