using Microsoft.AspNet.Identity;
using SchedulEasy.Data;
using SchedulEasy.Models;
using SchedulEasy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchedulEasy.WebMVC.Controllers
{
    public class TeamDataController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TeamData
        public ActionResult Index(int id)
        {
            var svc = CreateTeamService();
            var detail = svc.GetTeamByID(id);
            var model = detail.Members;
            return View(model);
        }

        public ActionResult Create(int id)
        {
            var svc = CreateTeamService();
            var detail = svc.GetTeamByID(id);
            List<ApplicationUser> users = db.Users.ToList();
            List<ApplicationUser> nullUsers = new List<ApplicationUser>();
            foreach (ApplicationUser user in users)
            {
                if (detail.Members.Contains(user.UserName))
                {
                    nullUsers.Add(user);
                }
            }
            foreach (ApplicationUser user in nullUsers)
            {
                users.Remove(user);
            }
            ViewBag.UserID = new SelectList(users, "Id", "UserName");
            var model =
                new TeamAddMember
                {
                    TeamID = detail.TeamID,
                    UserID = Convert.ToString(ViewBag.UserID),
                    Private = true
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TeamAddMember model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = CreateTeamService();
            service.AddToTeam(model);

            return RedirectToAction("Index", "Team");
        }

        public ActionResult Delete(string id, int teamID)
        {
            var svc = CreateTeamService();
            var model = svc.GetMemberByID(id, teamID);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Remove(string id, int teamID)
        {
            var svc = CreateTeamService();
            svc.RemoveMember(id, teamID);

            return RedirectToAction("Index", "Team");
        }

        public ActionResult Edit(string id, int teamID)
        {
            var service = CreateTeamService();
            var detail = service.GetMemberByID(id, teamID);
            var model =
                new TeamDataEdit
                {
                    TeamDataID = detail.TeamDataID,
                    Private = detail.Private
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TeamDataEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateTeamService();

            if (service.UpdateTeamMember(model))
            {
                return RedirectToAction("Index", "Team");
            }

            return RedirectToAction("Index", "Team");
        }



        private TeamService CreateTeamService()
        {
            var userID = User.Identity.GetUserId();
            var service = new TeamService(userID);
            return service;
        }
    }
}