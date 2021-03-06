﻿using SchedulEasy.Contracts;
using SchedulEasy.Data;
using SchedulEasy.Models;
using SchedulEasy.Models.Team;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulEasy.Services
{
    public class TeamService : ITeam
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
                    UserID = entity.OwnerID,
                    Private = false
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
                    UserID = model.UserID,
                    Private = model.Private
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.TeamsData.Add(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public TeamListComplete GetTeams()
        {
            List<int> dataJoined = new List<int>();
            List<int> dataPending = new List<int>();
            using (var ctx = new ApplicationDbContext())
            {
                foreach (var item in ctx.TeamsData)
                {
                    if ( item.UserID == _userID)
                    {
                        if (item.Private == false)
                        {
                            dataJoined.Add(item.TeamID);
                        }
                        else
                        {
                            dataPending.Add(item.TeamID);
                        }

                    }
                }
                var joinedquery =
                    ctx
                        .Teams
                        .Where(e => e.OwnerID == _userID || dataJoined.Contains(e.TeamID))
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
                var pendingquery =
                    ctx
                        .Teams
                        .Where(e => dataPending.Contains(e.TeamID))
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
                var Joined = joinedquery.ToArray();
                var Pending = pendingquery.ToArray();
                var ret = new TeamListComplete
                {
                    JoinedTeams = Joined,
                    PendingTeams = Pending
                };

                return ret;
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
                        .Single(e => e.TeamID == id);
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
            int dataID = 0;

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
                        dataID = item.TeamDataID;
                    }
                }
                return
                new TeamDataDetail
                {
                    TeamDataID = dataID,
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

        public TeamListComplete ConvertIDToUserName(TeamListComplete listItems)
        {
            using (var ctx = new ApplicationDbContext())
            {
                foreach (var listItem in listItems.JoinedTeams)
                {
                    foreach (var item in ctx.Users)
                    {
                        if (item.Id == listItem.OwnerName)
                        {
                            listItem.OwnerName = item.UserName;
                        }
                    }
                }
                foreach (var listItem in listItems.PendingTeams)
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
