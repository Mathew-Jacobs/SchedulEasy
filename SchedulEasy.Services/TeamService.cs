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

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Teams.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<TeamListItem> GetTeams()
        {
            using (var ctx = new ApplicationDbContext())
            {
                string ownerName = "";
                foreach (ApplicationUser user in ctx.Users)
                {
                    if (user.Id == _userID)
                    {
                        ownerName = user.UserName;
                        break;
                    }
                }
                var query =
                    ctx
                        .Teams
                        .Where(e => e.OwnerID == _userID)
                        .Select(
                        e =>
                            new TeamListItem
                            {
                                TeamID = e.TeamID,
                                Title = e.Title,
                                Description = e.Description,
                                OwnerName = ownerName
                            }
                        );
                return query.ToArray();
            }
        }

        public TeamDetail GetTeamByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Teams
                        .Single(e => e.TeamID == id && e.OwnerID == _userID);

                return
            new TeamDetail
            {
                TeamID = entity.TeamID,
                Title = entity.Title,
                Description = entity.Description
            };
            }
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
    }
}
