using SchedulEasy.Data;
using SchedulEasy.Models;
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
        IEnumerable<TeamListItem> GetTeams();
        TeamDetail GetTeamByID(int id);
        List<string> GetMembers(int id, ApplicationDbContext ctx);
        bool UpdateTeam(TeamEdit model);
        bool UpdateTeamMember(TeamDataEdit model);
        bool DeleteTeam(int id);
        TeamDataDetail GetMemberByID(string id, int teamID);
        bool RemoveMember(string id, int teamID);
        IEnumerable<TeamListItem> ConvertIDToUserName(IEnumerable<TeamListItem> listItems);
    }
}
