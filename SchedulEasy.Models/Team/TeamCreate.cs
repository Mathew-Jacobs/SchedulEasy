using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulEasy.Models
{
    public class TeamCreate
    {   
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        [Display(Name = "Name")]
        public string Title { get; set; }

        [MaxLength(500)]
        [Display(Name = "Info")]
        public string Description { get; set; }
    }
}
