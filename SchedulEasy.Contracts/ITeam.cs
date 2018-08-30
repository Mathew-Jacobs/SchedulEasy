using SchedulEasy.Data;
using SchedulEasy.Models;
using SchedulEasy.Models.Team;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulEasy.Contracts
{
    public interface ITeam
    {
        bool CreateTeam(TeamCreate model);
        bool AddToTeam(TeamAddMember model);
        TeamListComplete GetTeams();
        TeamDetail GetTeamByID(int id);
        List<string> GetMembers(int id, ApplicationDbContext ctx);
        bool UpdateTeam(TeamEdit model);
        bool UpdateTeamMember(TeamDataEdit model);
        bool DeleteTeam(int id);
        TeamDataDetail GetMemberByID(string id, int teamID);
        bool RemoveMember(string id, int teamID);
        TeamListComplete ConvertIDToUserName(TeamListComplete listItems);
    }
}
