using SchedulEasy.Data;
using SchedulEasy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulEasy.Services
{
    public class TeamService
    {
        private readonly string _userID;

        public TeamService(string userID)
        {
            _userID = userID;
        }

        public bool CreateTeam(TeamCreate model)
        {
            var entity =
                new Team()
                {
                    OwnerID = _userID,
                    Title = model.Title,
                    Description = model.Description
                };

            var teamData =
                new TeamData()
                {
                    TeamID = entity.TeamID,
                    UserID = entity.OwnerID
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Teams.Add(entity);
                ctx.TeamsData.Add(teamData);

                return ctx.SaveChanges() == 1;
            }
        }

        public bool AddToTeam(TeamAddMember model)
        {
            var entity =
                new TeamData()
                {
                    TeamID = model.TeamID,
                    UserID = model.UserID
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.TeamsData.Add(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<TeamListItem> GetTeams()
        {
            List<int> data = new List<int>();
            using (var ctx = new ApplicationDbContext())
            {
                foreach (var item in ctx.TeamsData)
                {
                    if ( item.UserID == _userID )
                    {
                        data.Add(item.TeamID);
                    }
                }
                var query =
                    ctx
                        .Teams
                        .Where(e => e.OwnerID == _userID || data.Contains(e.TeamID))
                        .Select(
                        e =>
                            new TeamListItem
                            {
                                TeamID = e.TeamID,
                                Title = e.Title,
                                Description = e.Description,
                                OwnerName = e.OwnerID
                            }
                        );
                return query.ToArray();
            }
        }

        public TeamDetail GetTeamByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                string ownerName = "";
                var entity =
                    ctx
                        .Teams
                        .Single(e => e.TeamID == id && e.OwnerID == _userID);
                var members = GetMembers(id, ctx);
                foreach (var item in ctx.Users)
                {
                    if (item.Id == entity.OwnerID)
                    {
                        ownerName = item.UserName;
                    }
                }


                return
                new TeamDetail
                {
                    TeamID = entity.TeamID,
                    Title = entity.Title,
                    Description = entity.Description,
                    OwnerName = ownerName,
                    Members = members
                };
            }
        }



        public List<string> GetMembers(int id, ApplicationDbContext ctx)
        {
            var teams = ctx.TeamsData.ToList();
            List<string> members = new List<string>();
            string member = "";
            foreach (TeamData team in teams)
            {
                if (id == team.TeamID)
                {
                    foreach (ApplicationUser user in ctx.Users)
                    {
                        if (team.UserID == user.Id)
                        {
                            member = user.UserName;
                            members.Add(member);
                        }
                    }
                }
            }
            return members;
        }

        public bool UpdateTeam(TeamEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Teams
                        .Single(e => e.TeamID == model.TeamID && e.OwnerID == _userID);

                entity.Title = model.Title;
                entity.Description = model.Description;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool UpdateTeamMember(TeamDataEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .TeamsData
                        .Single(e => e.TeamDataID == model.TeamDataID && e.UserID == _userID);

                entity.Private = model.Private;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteTeam(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Teams
                        .Single(e => e.TeamID == id && e.OwnerID == _userID);

                ctx.Teams.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public TeamDataDetail GetMemberByID(string id, int teamID)
        {
            var userID = "";
            bool priv = true;

            using (var ctx = new ApplicationDbContext())
            {
                foreach (var item in ctx.Users)
                {
                    if (item.UserName == id)
                    {
                        userID = item.Id;
                    }
                }
                foreach (var item in ctx.TeamsData)
                {
                    if (item.TeamID == teamID && item.UserID == userID)
                    {
                        priv = item.Private;
                    }
                }
                return
                new TeamDataDetail
                {
                    TeamDataID = teamID,
                    UserName = id,
                    UserID = userID,
                    Private = priv
                };

            }
        }

        public bool RemoveMember(string id, int teamID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                foreach (var item in ctx.Users)
                {
                    if (item.UserName == id)
                    {
                        id = item.Id;
                    }
                }
                var entity =
                    ctx
                        .TeamsData
                        .Single(e => e.UserID == id && e.TeamID == teamID);

                ctx.TeamsData.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }



        public IEnumerable<TeamListItem> ConvertIDToUserName(IEnumerable<TeamListItem> listItems)
        {
            using (var ctx = new ApplicationDbContext())
            {
                foreach (var listItem in listItems)
                {
                    foreach (var item in ctx.Users)
                    {
                        if (item.Id == listItem.OwnerName)
                        {
                            listItem.OwnerName = item.UserName;
                        }
                    }
                }
                return listItems;
            }
        }
    }
}
