using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulEasy.Models.Team
{
    public class TeamListComplete
    {
        public IEnumerable<TeamListItem> JoinedTeams { get; set; }
        public IEnumerable<TeamListItem> PendingTeams { get; set; }
    }
}
